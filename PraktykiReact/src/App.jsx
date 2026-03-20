import Header from "./components/Header.jsx"
import MainContent from "./components/MainContent.jsx"
import Calendar from "./pages/Calendar.jsx"
import LoginPage from "./pages/LoginPage.jsx"
import ManageWorkrules from "./pages/ManageWorkrules.jsx"
import { useState } from "react"
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom"

export default function App(){
  const [user, setUser] = useState(null);
  const today = new Date();

    return (
        <BrowserRouter>
        {user &&(<Header FirstName={user.firstName} MiddleName={user.middleName} LastName={user.lastName} Permission={user.permission}/>)}
          <Routes>
            {!user ? (
              <Route path="*" element={<LoginPage setUser={setUser}/>}/>
            ) : (
              <>
                <Route path="/" element={<MainContent emplID={user.id}/>}/>
                <Route path="/calendar" element={<Calendar year={today.getFullYear()} month={today.getMonth() +1} emplID={user.id}/>}/>
                <Route path="*" element={<Navigate to="/"/>}/>
                <Route path="/manage-workrules" element={<ManageWorkrules/>}/>
              </>
            )}
          </Routes>
        </BrowserRouter>
    )
}