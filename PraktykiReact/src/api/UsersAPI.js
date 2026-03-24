const API_URL = "https://localhost:7164/api/users"

export const loginUser = async ({login, password}) => {
    const response = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({login, password})
    });

    if(!response.ok){
        const error = await response.json();
        throw new Error(error.message);
    }
    const data = await response.json();
    return data;
}