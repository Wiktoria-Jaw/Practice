export default function Header(){
    return(
        <header className="main-header">
            <img src=".\src\assets\react_logo.png" className="logo" alt="React logo"/>
            <nav>
                <ul className = "nav-list">
                    <li className="nav-list-item">Pricing</li>
                    <li className="nav-list-item">About</li>
                    <li className="nav-list-item">Contact</li>
                </ul>
            </nav>
        </header>
    )
}