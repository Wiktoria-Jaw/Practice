import LoginForm from "../components/LoginForm";
import "../styles/LoginPage.css";

export default function LoginPage(props){
    return (
        <div className="login-page">
            <h1 className="login">Login</h1>
            <LoginForm setUser={props.setUser}/>
        </div>
    )
}