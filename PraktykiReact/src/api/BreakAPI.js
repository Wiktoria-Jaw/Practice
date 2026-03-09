const API_URL = "https://localhost:7164/api"

export const startBreak = async (emlID) =>{
    const response = await fetch(`${API_URL}/breaks/emplPanel/workday/break/end/${emplID}`,{
        method: "POST"
    });
    if(!response.ok){
        const error = await response.text();
        alert(error);
    }
    return response.text()
}

export const endBreak = async (emplID)=>{
    const response = await fetch(`${API_URL}/breaks/emplPanel/workday/break/end/${emplID}`,{
        method: "PUT"
    });
    if(!response.ok){
        const error = await response.text();
        alert(error);
    }
    return response.text();
}