export default function Header(props){
    return(
        <header>
            <h1>Hello <span>{props.Name} {props.Surname}</span></h1>
            <div className="menu">
                <a href="#">Home</a>
                <a href="#">Calendar</a>
                <a href="#">Log out</a>
            </div>
        </header>
    )
}