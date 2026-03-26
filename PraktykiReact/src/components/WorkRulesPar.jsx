import "../styles/WorkRulesPar.css";

export default function WorkRulesPar(props){
    return(
        <p className="workrulesPara"><span>{props.label}: {props.currValue}</span><input type="number" id={props.name} name={props.name} value={props.value ?? ""} onChange={(e) => props.setter(Number(e.target.value))}></input></p>
    )
}