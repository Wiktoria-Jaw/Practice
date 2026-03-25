import "../styles/Summary.css";
import { useEffect, useState } from "react";
import Button from "../components/Button.jsx";
import { getAllEmployeeData } from "../api/EmployeeAPI.js";

export default function Summary(){
    const [search, setSearch] = useState("");
    const [employees, setEmployees] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState(null);
    const [summary, setSummary] = useState(null);
    const [message, setMessage] = useState("");

    const fetchEmployeesData = async() => {
        try{
            const data = await getAllEmployeeData();
            setEmployees(data);
        }catch(error){
            setMessage(error.message);
        }
    }

    useEffect(() => {
        fetchEmployeesData();
    }, []);

    const filteredEmployee = employees.filter(emp =>
        `${emp.firstName} ${emp.middleName} ${emp.lastName}`.toLowerCase().includes(search.toLowerCase())
    )

    return(
        <>
            <main className="search-panel">
                <div className="left-panel">
                    <h2>Search Employee</h2>
                    <span className="search-span"> <label htmlFor="searchEmployee">Employee:</label>
                    <input type="text" id="searchEmployee" placeholder="Type name, middle name or surname..." value={search} onChange={(e) => setSearch(e.target.value)}/> </span>
                    <ul className="search-result">
                        {search && filteredEmployee.map(emp => (
                            <li key={emp.id} onClick={() => setSelectedEmployee(emp)}>
                                <span className="result-span">
                                    {emp.firstName} {emp.middleName} {emp.lastName}</span>
                                <span className="result-span">
                                    ID: {emp.id}
                                </span>
                            </li>
                        ))}
                    </ul>
                </div>
                {/* <div className="right-panel">
                    <h2>Choice date</h2>
                    <span className="choice-span"> <label htmlFor="choiceDate">Employee:</label>
                    <input type="date" id="choiceDate" value={search} onChange={(e) => setSearch(e.target.value)}/> </span>
                </div> */}
                
                {!!message && (<p className="info">{message}</p>)}
            </main>
            
            {!!selectedEmployee && (
                <main className="summery-panel">
                    <h2>Summery for: {selectedEmployee.FirstName} {selectedEmployee.MiddleName} {selectedEmployee.LastName}</h2>
                </main>
            )}

        </>
    )
}