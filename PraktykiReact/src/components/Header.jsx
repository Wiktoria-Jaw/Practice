export default function Header(props){
    return(
        <header>
            <h1>Hello {props.Name} {props.Surname}</h1>
            <div className="menu">
                <a href="#">Home</a>
                <a href="#">Calendar</a>
                <a href="#">Log out</a>
            </div>
        </header>
    )
}