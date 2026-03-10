import Button from "./Button.jsx"
import { startWorkday, endWorkday, statusWorkday } from "../api/WorkDayAPI"
import { useState, useEffect} from "react"
import { endBreak, startBreak, statusBreak } from "../api/BreakAPI.js"

export default function MainContent(props){
    const emplID = props.emplID;
    const [workState, setWorkState] = useState(null)
    const [message, setMessage] = useState("");

    useEffect(() => {
        const fetchStatus = async () => {
            try{
                console.log("test pobierania statusu dnia");
                const breakStatus = await statusBreak(emplID);
                const workStatus = await statusWorkday(emplID);
                console.log(breakStatus, workStatus)
                let status;
                if(workStatus === "notStarted" || workStatus === null){
                    status = "notStarted"
                }else if(workStatus === "ended"){
                    status = "ended";
                }else if(breakStatus === "onBreak"){
                    status = "onBreak";
                }else{
                    status = "working";
                }
                console.log(status)

                setWorkState(status);
            }catch(error){
                setMessage(error.message);
            }
        }
        fetchStatus();
    }, [emplID]);

    const handleAction = async (apiFunc) => {
        try{
            console.log("emplID:",emplID);
            const result = await apiFunc(emplID);
            console.log("Api wynik:", result);
            setMessage(result);
            
            const breakStatus = await statusBreak(emplID);
            const workStatus = await statusWorkday(emplID);
            console.log(breakStatus, workStatus)
            let status;
            if(workStatus === "notStarted" || workStatus === null){
                status = "notStarted";
            }else if(workStatus === "ended"){
                status = "ended";
            }else if(breakStatus === "onBreak"){
                status = "onBreak";
            }else{
                status = "working";
            }
            console.log(status);
            setWorkState(status);
            
        }catch (error){
            setMessage(error.message);
        }
    }

    const handleEndWorkday = async () =>{
        try{
            if(workState === "onBreak"){
                await endBreak(emplID);
            }

            const result = await endWorkday(emplID);
            setMessage(result);

            const workStatus = await statusWorkday(emplID);
            const breakStatus = await statusBreak(emplID);

            let status;
            if(workStatus === "notStarted" || workStatus === null){
                status = "notStarted";
            } else if(workStatus === "ended"){
                status = "ended";
            } else if(breakStatus === "onBreak"){
                status = "onBreak";
            } else {
                status = "working";
            }

            setWorkState(status);
        }catch(error){
            setMessage(error.message);
        }
    }

    let workButton, breakButton, workButtonInfo, breakButtonInfo;

    if(workState === null){
        workButtonInfo = "Waiting for database...";
        breakButtonInfo = "Waiting for database...";
        workButton = <Button label="Start Workday" disabled={true}/>
        breakButton= <Button label="Start Break" disabled={true}/>
    }

    if(workState==="notStarted"){
        workButton = <Button label="Start Workday" onClick={() => handleAction(startWorkday, "working")}/>
        workButtonInfo="Click to start your workday.";
    }else if(workState === "working" || workState === "onBreak"){
        workButton = <Button label="End Workday" onClick={() => handleEndWorkday()}/>
        workButtonInfo="Click to end your workday.";
    }else{
        workButton = <Button label="End Workday" disabled={true}/>
        workButtonInfo="Workday ended.";
    }   

    if(workState==="working"){
        breakButton = <Button label="Start Break" onClick={()=> handleAction(startBreak)}/>
        breakButtonInfo="You can start your break.";
    }else if(workState==="onBreak"){
        breakButton = <Button label="End Break" onClick={()=> handleAction(endBreak)}/>
        breakButtonInfo="Currenly on break. Click to end your break.";
    }else if(workState==="notStarted"){
        breakButton = <Button label="Start Break" disabled={true}/>
        breakButtonInfo="You need to start your workday first.";
    }else{
        breakButton = <Button label="Start Break" disabled={true}/>
        breakButtonInfo="Workday ended, you can't start your break.";
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