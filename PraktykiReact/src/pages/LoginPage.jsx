import LoginForm from "../components/LoginForm";
import "../styles/LoginPage.css";

export default function LoginPage(props){
    return (
        <div className="login-page">
            <h1 className="login">Welcome back</h1>
            <h2>Please log in...</h2>
            <LoginForm setUser={props.setUser}/>
        </div>
    )
}