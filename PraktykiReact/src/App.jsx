import Header from "./components/Header.jsx"
import MainContent from "./components/MainContent.jsx"
import Calendar from "./pages/Calendar.jsx"
import { getEmployeeData } from "./api/EmployeeAPI.js"
import { useState, useEffect } from "react"
import { BrowserRouter, Routes, Route } from "react-router-dom"

export default function App(){
  const [employee, setEmployee] = useState({Name: "", Surname: ""});
  const employeeId = 1;

  useEffect(() => {
    const fetchData = async()=>{
      try {
        const data = await getEmployeeData(employeeId);
        setEmployee({Name: data.name, Surname: data.surname})
      }catch(error){
        console.error(error);
      }
    };
    fetchData();
    }, []);
  

    return (
        <BrowserRouter>
          <Header Name={employee.Name} Surname={employee.Surname}/>
          <Routes>
             <Route path="/" element={<MainContent emplID={employeeId}/>}/>
             <Route path="/calendar" element={<Calendar year={Date.now.year} month={Date.now.month}/>}/>
             {/* <Route path="/log-out" element={" "}/> */}
          </Routes>
        </BrowserRouter>
    )
}