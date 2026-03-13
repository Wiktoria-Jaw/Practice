import {Link} from 'react-router-dom';
import "../styles/Header.css"

export default function Header(props){
    return(
        <header>
            <h1>Hello <span>{props.Name} {props.Surname}</span></h1>
            <nav>
                <Link to ="/">Home</Link>
                <Link to ="/calendar">Calendar</Link>
                {/* <Link to ="/log-out">Log out</Link> */}
            </nav>
        </header>
    )
}