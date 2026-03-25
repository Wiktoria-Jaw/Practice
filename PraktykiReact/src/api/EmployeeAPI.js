const API_URL = "https://localhost:7164/api/employees"

export const getEmployeeData = async (emplID) => {
    const response = await fetch(`${API_URL}/employee/${emplID}`,{
        method: "GET"
    });
    if(!response.ok){
        throw new Error("Failed to fetch employee data.");
    }
    return response.json();
}

export const getAllEmployeeData = async () => {
    const response = await fetch(`${API_URL}/employee/allemployee`,{
        method: "GET"
    });
    if(!response.ok){
        throw new Error("Failed to fetch employee data.");
    }
    return response.json();
}