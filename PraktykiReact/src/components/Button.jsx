import "../styles/Button.css"

export default function Button(props){
    return(
        <button className={props.class} onClick={props.onClick} disabled={props.disabled} type={props.type}>{props.label}</button>
    )
}