import "../styles/Summary.css";
import { useEffect, useState } from "react";
import SummaryPanel from "../components/SummaryPanel.jsx";
import { getAllEmployeeData } from "../api/EmployeeAPI.js";

export default function Summary(){
    const [search, setSearch] = useState("");
    const [selectedDate, setSelectedDate] = useState("");
    const [employees, setEmployees] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState("");
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
    );

    return(
        <>
            <main className="search-panel">
                <div className="search-panel-flex">
                    <div className="left-panel">
                        <h2>Search Employee</h2>
                        <span className="span-search-panel"> 
                            <label htmlFor="searchEmployee">Employee:</label>
                            <input type="text" id="searchEmployee" placeholder="Type name, middle name or surname..." value={search} onChange={(e) => setSearch(e.target.value)}/> 
                        </span>
                        <ul className="search-result">
                            {search && filteredEmployee.map(emp => (
                                <li key={emp.id} onClick={() => {setSelectedEmployee(emp); setSearch(emp.firstName)}}>
                                    <span className="result-span">
                                        {emp.firstName} {emp.middleName} {emp.lastName}</span>
                                    <span className="result-span">
                                        ID: {emp.id}
                                    </span>
                                </li>
                            ))}
                        </ul>
                    </div>
                    <div className="right-panel">
                        <h2>Choice date</h2>
                        <span className="span-search-panel"> 
                            <label htmlFor="choiceDate">Date:</label>
                            <input type="date" id="choiceDate" value={selectedDate} onChange={(e) => setSelectedDate(e.target.value)}/> 
                        </span>
                    </div>
                </div>
                {!!message && (<p className="info">{message}</p>)}
            </main>
            
            {!!selectedEmployee && !!selectedDate && (
                <SummaryPanel date = {selectedDate} id = {selectedEmployee.id} firstName = {selectedEmployee.firstName} middleName = {selectedEmployee.middleName} lastName = {selectedEmployee.lastName}/>
            )}

        </>
    )
}