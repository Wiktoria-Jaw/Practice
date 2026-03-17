const API_URL = "https://localhost:7164/api/day_off"

export const getDaysOff = async (emplID) => {
    const response = await fetch(`${API_URL}/daysoff`,{
        method: "GET"
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    
    const data = await response.json();
    return data;
}