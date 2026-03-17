import "../styles/DayCard.css";

export default function DayCard(props){
    return(
        <div className = "day-card" style={props.style}>
            <h1>{props.Num}</h1>
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