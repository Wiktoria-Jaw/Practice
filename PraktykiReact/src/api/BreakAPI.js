const API_URL = "https://localhost:7164/api/breaks"

export const startBreak = async (emplID) =>{
    console.log("test wysywlania",emplID)
    const response = await fetch(`${API_URL}/emplPanel/workday/break/start/${emplID}`,{
        method: "POST"
    });
    if(!response.ok){
        const error = await response.text();
        throw new Error(error);
    }
    return response.text()
}

export const endBreak = async (emplID)=>{
    const response = await fetch(`${API_URL}/emplPanel/workday/break/end/${emplID}`,{
        method: "PUT"
    });
    if(!response.ok){
        const error = await response.text();
        console.log(error);
        throw new Error(error);
    }
    return response.text();
}

export const statusBreak = async (emplID)=>{
    const response = await fetch(`${API_URL}/emplPanel/workday/break/status/${emplID}`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.text();
        alert(error);
    }
    return response.text();
}