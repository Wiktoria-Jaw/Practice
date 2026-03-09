const API_URL = "https://localhost:7164/api"

export const startWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/workdays/emplPanel/workday/start/${emplID}`,{
        method: "POST"
    });
    if(!response.ok){
        const error = await response.text();
        alert(error);
    }
    
    return response.text();
}

export const endWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/workdays/emplPanel/workday/end/${emplID}`,{
        method: "POST"
    });

    if(!response.ok){
        const error = await response.text();
        alert(error);
    }

    return response.text();
}