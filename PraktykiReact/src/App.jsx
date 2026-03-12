import Header from "./components/Header.jsx"
import MainContent from "./components/MainContent.jsx"
import { getEmployeeData } from "./api/EmployeeAPI.js"
import { useState, useEffect } from "react"

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
      <>
        <Header Name={employee.Name} Surname={employee.Surname}/>
        <MainContent emplID={employeeId}/>
      </>
    )
}