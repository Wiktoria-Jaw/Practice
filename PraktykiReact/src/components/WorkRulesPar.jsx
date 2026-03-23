import "../styles/WorkRulesPar.css";

export default function WorkRulesPar(props){
    return(
        <p><span>{props.label}: {props.currValue}</span><input type="number" id={props.name} name={props.name} value={props.value} onChange={(e) => props.setter(e.target.value)}></input></p>
    )
}