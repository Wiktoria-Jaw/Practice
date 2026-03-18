import "../styles/DayCard.css";

export default function DayCard(props){
    const hasDaysOff = props.employees.length > 0;
    return(
        <div className = "day-card" style={props.style}>
            <h1>{props.Num}</h1>
            {hasDaysOff && <span className="whoHasDaysOff">Days off:</span>}
            <div className="employees">
                {props.employees.map((empl, index) =>(
                <div key={index} className="employeeNames">
                    {empl.FirstName} {empl.MiddleName} {empl.LastName}
                </div>
                ))}
            </div>
        </div>
    )
}