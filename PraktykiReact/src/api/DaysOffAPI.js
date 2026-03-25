const API_URL = "https://localhost:7164/api/day_off"

export const getAcceptedDaysOff = async () => {
    const response = await fetch(`${API_URL}/emplPanel/daysoff/accepted`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const declareDayOff = async (emplID, startDate, endDate) => {
    const response = await fetch(`${API_URL}/emplPanel/daysoff/declareDaysOff/${emplID}`,{
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            startDate: startDate,
            endDate: endDate
        })
    });
    if(!response.ok){
      const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const adminGetAcceptedDaysOff = async () => {
    const response = await fetch(`${API_URL}/adminPanel/daysoff/accepted`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const adminGetPendingDaysOff = async () => {
    const response = await fetch(`${API_URL}/adminPanel/daysoff/pending`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const adminGetRejectedDaysOff = async () => {
    const response = await fetch(`${API_URL}/adminPanel/daysoff/rejected`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const DecideDayOff = async ({DayoffID, Status}) =>{
    const response = await fetch(`${API_URL}/adminPanel/daysoff/decide`,{
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({DayoffID, Status})
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const GetSummary = async (emplID,date) => {
    const response = await fetch(`${API_URL}/adminPanel/summary/${emplID}/${date}`,{
        method: "GET",
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}