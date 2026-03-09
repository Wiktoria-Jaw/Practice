import Button from "./Button.jsx"
import { startWorkday, endWorkday } from "../api/WorkDayAPI"
import { useState, useEffect} from "react"
import { endBreak, startBreak } from "../api/BreakAPI.js"

export default function MainContet(){

    const [workState, setWorkState] = useState("notStarted")
    const [message, setMessage] = useState("");


    const handleAction = async (apiFunc, nextState) => {
        try{
            const result = await apiFunc(emplID);
            setMessage(result);
            if (nextState){
                setWorkState(nextState);
            } 
        }catch (error){
            setMessage(error.message);
        }
    }

    let workButton;
    let breakButton;
    let workButtonInfo = "Start your workday";
    let breakButtonInfo = "You cant start your break without starting workday.";

    if(workState==="notStarted"){
        workButton = <Button label="Start Workday" onClick={() => handleAction(startWorkday, "working")}/>
        workButtonInfo="Workday started. You can take break or end your workday.";
    }else{
        workButton = <Button label="End Workday" onClick={() => handleAction(endWorkday,"workdayEnded")}/>
    }   workButtonInfo="Workday ended.";

    if(workState==="working"){
        breakButton = <Button label="Start Break" onClick={()=> handleAction(startBreak, "onBreak")}/>
        breakButtonInfo="Break started. You can stop it anytime";
    }else if(workState==="onBreak"){
        breakButton = <Button label="End Break" onClick={()=> handleAction(endBreak, "working")}/>
        breakButtonInfo="Break ended. You can start a new one.";
    }else if(workState==="notStarted"){
        breakButton = <Button label="Start Break" onClick={()=> handleAction(startBreak, "onBreak")}/>
        breakButtonInfo="You cant start your break without starting workday.";
    }else{
        breakButton = <Button label="Start Break" onClick={()=> handleAction(startBreak, "onBreak")}/>
        breakButtonInfo="You can start your break.";
    }

    return(
        <main>
            <div className="buttonNInfo">
                {workButton}
                <span className="infoSpan">{workButtonInfo}</span>
            </div>
            <div className="buttonNInfo">
                {breakButton}
                <span className="infoSpan">{breakButtonInfo}</span>
            </div>
        </main>
    )
}