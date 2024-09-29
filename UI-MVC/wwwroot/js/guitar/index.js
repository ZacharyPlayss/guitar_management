import {retrieveData, putData} from "../shared/CustomFunctions.js";
import {getCurrentPort, getCurrentProtocol} from "../shared/CustomFunctions.js";

const protocol = getCurrentProtocol();
const port = getCurrentPort();

//COOKIE WAARDE UITLEZEN DIE IN DE API CONTROLLER GEZET WORDT.
let currentIdentityCookieValue = identityCookieValue;
//ELEMENTS
const priceElement = document.querySelector("#priceElement");
const nameElement = document.querySelector("#nameElement");
const amountOfStringElement = document.querySelector("#stringAmountElement");

const updateButton = document.querySelector(".updateButton");
const guitarDetails = document.querySelector(".guitarDetails");
const updateMessageElement = document.querySelector("#updateMessage");
const guitarId = guitarDetails.dataset.id;
const getUrl = `${protocol}//localhost:${port}/api/guitars`;
const url = `${protocol}//localhost:${port}/api/guitars/${guitarId}`

if(updateButton){
updateButton.addEventListener("click",async function () {
    const amountOfString = parseInt(amountOfStringElement.innerHTML);
    const headers = {
        "Content-Type": "application/json",
        Accept: "application/json",
        Cookie: `.AspNetCore.Identity.Application=${currentIdentityCookieValue}`
    };
    const data = {
        "price": priceElement.value,
    };
    updateMessageElement.style.color = "black";
    const response = await putData(url, headers,data);
    if(response.status == 200){
        updateMessageElement.innerHTML = "Update succesful."
        updateMessageElement.style.color = "green";
        
    }else {
        updateMessageElement.style.color = "red";
        updateMessageElement.innerHTML = "Update failed :'( "
    }
})}