import Header from "./components/Header.jsx"
import MainContent from "./components/MainContent.jsx"
import Calendar from "./pages/Calendar.jsx"
import LoginPage from "./pages/LoginPage.jsx"
import ManageWorkrules from "./pages/ManageWorkrules.jsx"
import ManageCalendar from "./pages/ManageCalendar.jsx"
import { useEffect, useState } from "react"
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom"

export default function App(){
  const [user, setUser] = useState(null);
  const today = new Date();

  useEffect(() =>{
    const storedUser = localStorage.getItem("user");

    if(storedUser){
      setUser(JSON.parse(storedUser));
    }
  }, [])

    return (
        <BrowserRouter>
        {user &&(<Header FirstName={user.firstName} MiddleName={user.middleName} LastName={user.lastName} Permission={user.permission} setUser={setUser}/>)}
          <Routes>
            {!user ? (
              <Route path="*" element={<LoginPage setUser={setUser}/>}/>
            ) : (
              <>
                <Route path="/" element={<MainContent emplID={user.id}/>}/>
                <Route path="/calendar" element={<Calendar year={today.getFullYear()} month={today.getMonth() +1} emplID={user.id}/>}/>
                <Route path="*" element={<Navigate to="/"/>}/>
                <Route path="/manage-workrules" element={<ManageWorkrules/>}/>
                <Route path="/manage-calendar" element={<ManageCalendar/>}/>
              </>
            )}
          </Routes>
        </BrowserRouter>
    )
}