import {retrieveData,postData} from "../shared/CustomFunctions.js";
import {getCurrentPort, getCurrentProtocol} from "../shared/CustomFunctions.js";

const protocol = getCurrentProtocol();
const port = getCurrentPort();

const storeDetails = document.querySelector(".storeDetails");
const storeid = storeDetails.dataset.id;

const form =  document.querySelector(".addGuitarButtonForm");
const tableBody = document.querySelector(".table-body");
const guitarModelSelect = document.querySelector("#guitarModelSelect");
const amountInput = document.querySelector("#amountInput");
async function retrieveStore() {
    try{
        tableBody.innerHTML =  ``;
        guitarModelSelect.innerHTML = ``;
        //DATA RETRIEVES FROM API POINT
        const stores = await retrieveData(`${protocol}//localhost:${port}/api/Stores/store/${storeid}`);
        const modelsNotInStore = await retrieveData(`${protocol}//localhost:${port}/api/Stores/guitarmodel/${storeid}`);

        //USE DATA TO FILL PAGE

        const stockElements = stores.stock;
        stockElements.forEach(function(element){
            const guitarModel = element.guitarModel;
            const amountInStock = element.amount;
            tableBody.innerHTML += `
                 <tr>
                    <td>${amountInStock}</td>
                    <td>${guitarModel.name}</td>
                    <td>${guitarModel.amountOfStrings}</td>
                    <td>€ ${guitarModel.price}</td>
                 </tr>
                `
        })
        modelsNotInStore.forEach(function(element){
            guitarModelSelect.innerHTML += `
                <option value="${element.id}">${element.name}</option>
            `
        })
        
    } catch (error) {
        console.error(error.message);
    }
}
retrieveStore();
form.addEventListener("submit", async function (e) {
    e.preventDefault();
    let amountValue;
    if (amountInput.value === "") {
        amountValue = null;
    } else {
        amountValue = amountInput.value;
    }
    const data = {
        "StoreId": storeid,
        "GuitarModelId": guitarModelSelect.value,
        "Amount": amountValue
    }
    await postData(`${protocol}//localhost:${port}/api/Stores`, data);
    await retrieveStore();
})