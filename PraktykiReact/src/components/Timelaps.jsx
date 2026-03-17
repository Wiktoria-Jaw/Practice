import "../styles/Timelaps.css"
import {useState, useEffect} from "react";

export default function Timelaps(props){
    const [elapsed, setElapsed] = useState("00:00:00");

    const formatTime = (seconds) => {
        const h = String(Math.floor(seconds/3600)).padStart(2,"0");
        const m = String(Math.floor((seconds%3600)/60)).padStart(2,"0");
        const s = String(seconds % 60).padStart(2,"0");
        return `${h}:${m}:${s}`;
    }

    useEffect(() => {
        if (!props.start) {
            setElapsed("00:00:00");
            return;
        }
        const interval = setInterval(() => {
            const startDate = new Date(props.start);
            const now = props.end ? new Date(props.end) : new Date();
            const diffSeconds = Math.floor((now - startDate) / 1000);
            setElapsed(formatTime(diffSeconds));
        }, 1000);
        return () => clearInterval(interval);
        }, [props.start, props.end]);

    const formatDateTime = (datetime) => {
        if (!datetime) return "--:--:--";
        return new Date(datetime).toLocaleTimeString();
    };

    return(
        <div className="timelaps">
            {props.mode === "work" ? (
                <>
                    <p>Work started: {formatDateTime(props.start)}</p>
                    <p>Elapsed: {elapsed}</p>
                    <p>Work ended: {formatDateTime(props.end)}</p>
                </>
            ) : (
                <>
                    <p>Break started: {formatDateTime(props.start)}</p>
                    <p>Elapsed break: {elapsed}</p>
                    <p>Break ended: {formatDateTime(props.end)}</p>
                </>
            )}
        </div>
    )
}