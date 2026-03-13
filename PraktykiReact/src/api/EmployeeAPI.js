const API_URL = "https://localhost:7164/api"

export const getEmployeeData = async (emplID) => {
    const response = await fetch(`${API_URL}/employees/employee/${emplID}`);
    if(!response.ok){
        throw new Error("Failed to fetch employee data.");
    }
    
    return response.json();
}