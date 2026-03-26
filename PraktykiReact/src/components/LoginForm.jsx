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
                <p className="loginText">Login</p>
                <input id="login" name="login" type="text" placeholder="Login" value={login} onChange={e=> setLogin(e.target.value)}/>
                <p className="loginText">Password</p>
                <input id="password" name="password" type="password" value={password} onChange={e=> setPassword(e.target.value)} placeholder="Password"/>
                <Button type="submit" label="Log In" disabled={!login || !password}/>
            </form>
            {!!message && (<h1 className="information">{message}</h1>)}
        </>
        
    )
}