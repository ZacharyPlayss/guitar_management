import {retrieveData} from "../shared/CustomFunctions.js";
const tableBody = document.getElementById("manufacturerTableBody");
const refreshButton = document.getElementById("refreshButton");
async function retrieveManufacturers() {
    try{
    const manufacturers = await retrieveData("/api/Manufacturers");
    
    let bodyHtml = ''
    for(const manufacturer of manufacturers){
        const dateObject = new Date(manufacturer.dateFounded);
        //FORMATTING FOR DATE STRING SO THAT IT SHOWS CLEAN IN THE DIAGRAM
        const dateString = ("0"+dateObject.getDate()).slice(-2) + "/" + ("0" + dateObject.getMonth() +1).slice(-2) + "/" + dateObject.getFullYear();
        bodyHtml += `
        <tr>
        <td>${manufacturer.name}</td>
        <td>${manufacturer.location}</td>
        <td>${dateString}</td>
        </tr>
        `
    }
    tableBody.innerHTML = bodyHtml
    console.log("Loaded after things.");
    } catch (error) {
        console.error(error.message);
    }
}

retrieveManufacturers()
refreshButton.addEventListener('click', retrieveManufacturers)
