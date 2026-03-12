import Button from "./Button"
import Timelap from "./Timelaps"
import { startWorkday } from "../api/WorkDayAPI"
import { useState, useEffect} from "react"

export default function Section(){
    const [workState, setWorkState] = useState("notStarted")
    const [message, setMessage] = useState("");


    const handleAction = async (apiFunc, nextState) => {
        try{
            const result = await apiFunc(emplID);
            setMessage(result);
            if (nextState) setWorkState(nextState);
        }catch (error){
            setMessage(error.message);
        }
    }

    return(
        <div className="section">
            <Button label="Start Workday" onClick={handleStartWork}/>
            <span className="timelap"></span>
        </div>
    )
}