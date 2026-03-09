import Header from "./components/Header.jsx"
import MainContet from "./components/MainContent.jsx"
<<<<<<< HEAD
import { getEmployeeData } from "./api/EmployeeAPI.js"
import { useState, useEffect } from "react"

export default function App(){
  const [employee, setEmployee] = useState({Name: "", Surname: ""});
  const employeeId = 2;

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
=======

export default function App(){
    return (
      <>
        <Header />
>>>>>>> d32fa4a11364822f7ee53419ad19ebd09de9d703
        <MainContet />
      </>
    )
}