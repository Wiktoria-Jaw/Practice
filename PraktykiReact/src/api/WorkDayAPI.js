const API_URL = "https://localhost:7164/api/workdays"

export const startWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/start/${emplID}`,{
        method: "POST"
    });
    if(!response.ok){
        const error = await response.text();
        throw new Error(error);
    }
    
    return response.text();
}

export const endWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/end/${emplID}`,{
        method: "PUT"
    });

    if(!response.ok){
        const error = await response.text();
        throw new Error(error);
    }

    return response.text();
}

export const statusWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/status/${emplID}`,{
        method: "GET"
    });

    if(!response.ok){
        alert("Failed to fetch work status");
    }
    return response.text();
}