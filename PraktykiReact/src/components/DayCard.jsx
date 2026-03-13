import "../styles/DayCard.css"

export default function DayCard(props){
    return(
        <div className = "day-card">
            <h1>{props.Num}</h1>
            <span className="whoHasDayOff">Days off:</span>
            <div className="employees">
                {props.employees.map((name, surname, index) =>(
                <div key={index} className="employeeNames">
                    {name} {surname}
                </div>
                ))}
            </div>
        </div>
    )
}