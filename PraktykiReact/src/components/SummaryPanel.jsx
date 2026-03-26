import { useEffect, useState } from "react"
import { GetSummary } from "../api/DaysOffAPI";
import "../styles/SummaryPanel.css";

export default function SummaryPanel(props){
    const [summary, setSummary] = useState({});
    const [message, setMessage] = useState("");

    useEffect(() => {
        if(!props.id || !props.date){
            return;
        }
        setSummary({});
        setMessage("");

        const fetchSummary = async() => {
            try{
                const data = await GetSummary(props.id, props.date);
                setSummary(data);
            }catch(error){
                setMessage(error.message);
            }
        }
        fetchSummary();
    }, [props.id, props.date]);

    const formatTime = (timeString) => {
        if(!timeString){
            return "";
        }else{
            return timeString.split(".")[0];
        }
    }

    return (
        <main className="summary-panel">
            <h2>Summary for {props.firstName} {props.middleName} {props.lastName}</h2>
            {!!summary.status && (
                <>
                    {summary.status == "dayOff" && (
                        <p className="summary">{summary.message}</p>
                    )}
                    {summary.status == "workday" && (
                        <div className="workday-summary">
                            <p className="summary">Total workday time: {formatTime(summary.wdLength)}</p>
                            <p className="summary">Amount of breaks taken: {summary.allBreaks}</p>
                            <p className="summary">Total breaks time: {formatTime(summary.allBLength)}</p>
                            <p className="summary">Real workday time: {formatTime(summary.finalWDLength)}</p>
                        </div>
                    )}
                </>
            )}
            {!!message && (<p className="info">{message}</p>)}
        </main>
    )
}