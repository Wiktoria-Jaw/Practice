import {Link} from 'react-router-dom';
import "../styles/Header.css"

export default function Header(props){
    const handleLogout = () => {
        localStorage.removeItem("user");
        props.setUser(null);
    }

    return(
        <header>
            <h1>Hello <span className="helloSpan">{props.FirstName} {props.MiddleName} {props.LastName}</span></h1>
            <nav>
                <Link to ="/">Home</Link>
                <Link to ="/calendar">Calendar</Link>
                {props.Permission === "admin" && (<Link to="/manage-calendar">Manage Calendar</Link>)}
                {props.Permission === "admin" && (<Link to="/manage-workrules">Manage Work Rules</Link>)}
                {props.Permission === "admin" && (<Link to="/check-summary">Summary</Link>)}
                <Link to ="*" onClick={handleLogout}>Log out</Link>
            </nav>
        </header>
    )
}