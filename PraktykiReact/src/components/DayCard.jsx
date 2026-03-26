import "../styles/DayCard.css";

export default function DayCard(props){
    const hasDaysOff = props.employees.length > 0;
    const dayCardClass = `day-card ${props.isSelected ? "selected" : ""} ${props.isToday && !props.isSelected ? "today" : ""}`;
    return(
        <div className = {dayCardClass} style={props.style} onClick={props.onClick}>
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