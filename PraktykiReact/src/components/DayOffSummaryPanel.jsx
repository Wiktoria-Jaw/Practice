import { useEffect, useState } from "react"
import { GetDayOffSummary } from "../api/DaysOffAPI";
import "../styles/DayOffSummaryPanel.css";

export default function DayOffSummaryPanel(props){
    const [dayOffSummary, setDayOffSummary] = useState([])
    const [message, setMessage] = useState("");
    
    useEffect(() => {
            if(!props.date){
                return;
            }
            setDayOffSummary([]);
            setMessage("");
    
            const fetchDayOffSummary = async() => {
                try{
                    const data = await GetDayOffSummary(props.date);
                    console.log(data);
                    setDayOffSummary(data);
                }catch(error){
                    setMessage(error.message);
                }
            }
            fetchDayOffSummary();
        }, [props.date]);

    const formatMonth = (date) =>{
        if(!date){
            return "";
        }else{
            const [year, month] = date.split("-");
            const monthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            const monthIndex = parseInt(month, 10)-1;

            return`${monthNames[monthIndex]} ${year}`;
        }
    }

    return (
        <main className="dayoff-panel">
            <h2>Month: {formatMonth(props.date)}</h2>
            <ul className="dayoff-summary">
                {dayOffSummary.map(day =>(
                    <li key={day.id}>{day.firstName} {day.middleName} {day.lastName} : {day.daysOffCount} days</li>
                ))}
            </ul>
            {!!message && (<p className="info">{message}</p>)}
        </main>
    )
}