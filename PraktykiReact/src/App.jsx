import Header from "./components/Header.jsx"
import MainContent from "./components/MainContent.jsx"
import Calendar from "./pages/Calendar.jsx"
import { getEmployeeData } from "./api/EmployeeAPI.js"
import { useState, useEffect } from "react"
import { BrowserRouter, Routes, Route } from "react-router-dom"

export default function App(){
  const [employee, setEmployee] = useState({FirstName: "", MiddleName: "", LastName: ""});
  const employeeId = 1;

  useEffect(() => {
    const fetchData = async()=>{
      try {
        const data = await getEmployeeData(employeeId);
        setEmployee({FirstName: data.firstName, MiddleName: data.middleName, LastName: data.lastName});
      }catch(error){
        console.error(error);
      }
    };
    fetchData();
    }, []);

    return (
        <BrowserRouter>
          <Header FirstName={employee.FirstName} MiddleName={employee.MiddleName} LastName={employee.LastName}/>
          <Routes>
             <Route path="/" element={<MainContent emplID={employeeId}/>}/>
             <Route path="/calendar" element={<Calendar year={Date.now.year} month={Date.now.month}/>}/>
             {/* <Route path="/log-out" element={" "}/> */}
          </Routes>
        </BrowserRouter>
    )
}