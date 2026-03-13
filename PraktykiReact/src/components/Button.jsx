import "../styles/Button.css"

export default function Button({label, onClick}){
    return(
        <>
            <button className="ButtonState" onClick={onClick}>{label}</button>
        </>
    )
}