const API_URL = "https://localhost:7164/api/workdays"

export const startWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/start/${emplID}`,{
        method: "POST"
    });
    if(!response.ok){
        const error = await response.json()
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const endWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/end/${emplID}`,{
        method: "PUT"
    });

    if(!response.ok){
        const error = await response.json()
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const statusWorkday = async (emplID) => {
    const response = await fetch(`${API_URL}/emplPanel/workday/status/${emplID}`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json()
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}