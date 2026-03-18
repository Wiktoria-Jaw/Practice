const API_URL = "https://localhost:7164/api/day_off"

export const getAcceptedDaysOff = async (emplID) => {
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

// export const getAllDaysOff = async (emplID) => {
//     const response = await fetch(`${API_URL}/adminPanel/daysoff`,{
//         method: "GET"
//     });
//     if(!response.ok){
//         const error = await response.json();
//         throw new Error(error.message);
//     }
//     const data = await response.json();
//     return data;
// }