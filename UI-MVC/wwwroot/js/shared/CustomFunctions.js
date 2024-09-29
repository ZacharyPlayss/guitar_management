
export function print(data){
    console.log(data);
}
export function getCurrentPort(){
    const port = window.location.port;
    return port;
}
export function getCurrentProtocol(){
    const protocol = window.location.protocol;
    return protocol;
}
export async function retrieveData(url) {
    const response = await fetch(url, {
        headers: {
            Accept: "application/json"
        }
    });

    if (response.status === 200) {
        return response.json();
    } else {
        throw new Error(`Failed to retrieve data from ${url}`);
    }
}

export async function postData(url, data){
    const response = await fetch(url, {
        method: "POST",
        headers:{
            "Content-Type":"application/json",
            Accept:"application/json"
        },
        body:JSON.stringify(data)
    });
    
    if(response.status === 201 || response.status === 200){
        return response.json();
    }
    else{
        throw new Error(`Failed to post data to ${url}`)
    }
}
export async function putData(url, headers,data){
    const response = await fetch(url, {
        method: "PUT",
        headers: headers,
        body:JSON.stringify(data)
    });
    if(response.status === 201 || response.status === 200){
        return response;
    }
    else{
        throw new Error(`Failed to put data to ${url}`)
    }
}
