const API_URL = "https://localhost:7164/api/worksettings"

export const GetWorkSettings = async () => {
    const response = await fetch(`${API_URL}/adminPanel/workrules`,{
        method: "GET",
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}

export const UpdateWorkSettings = async ({MinWorkdayLength, AutoEndWorkday, MinBreakBetweenWorkdays, MinWorkdayLengthForBreak, MinBreakLength}) => {
    const response = await fetch(`${API_URL}/adminPanel/workrules/update`,{
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({MinWorkdayLength, AutoEndWorkday, MinBreakBetweenWorkdays, MinWorkdayLengthForBreak, MinBreakLength})
    });
    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}