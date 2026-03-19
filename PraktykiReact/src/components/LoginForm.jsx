import { useState } from "react";
import { loginUser } from "../api/UsersAPI";
import Button from "./Button";
import "../styles/LoginForm.css";

export default function LoginForm(props){
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        try{
            const data = await loginUser({login, password});
            localStorage.setItem("user", JSON.stringify(data));
            props.setUser(data);
        }catch (error){
            setMessage(error.message);
        }
    }

    return (
        <>
            <form onSubmit={handleSubmit} className="log-form">
                <input id="login" name="login" placeholder="Login" value={login} onChange={e=> setLogin(e.target.value)}/>
                <input id="password" name="password" type="password" value={password} onChange={e=> setPassword(e.target.value)} placeholder="Password"/>
                <Button type="submit" label="Log In" disabled={!login || !password}/>
        </form>
        {!!message && (<h1>{message}</h1>)}
        </>
        
    )
}