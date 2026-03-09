<<<<<<< HEAD
export default function Header(props){
    return(
        <header>
            <h1>Hello {props.Name} {props.Surname}</h1>
=======
export default function Header(){
    return(
        <header>
            <h1>Hello [Imie pracownika]</h1>
>>>>>>> d32fa4a11364822f7ee53419ad19ebd09de9d703
            <div className="menu">
                <a href="#">Home</a>
                <a href="#">Calendar</a>
                <a href="#">Log out</a>
            </div>
        </header>
    )
}