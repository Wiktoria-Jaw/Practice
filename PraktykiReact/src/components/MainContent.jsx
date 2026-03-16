import Button from "./Button.jsx"
import Timelaps from "./Timelaps.jsx"
import { startWorkday, endWorkday, statusWorkday } from "../api/WorkDayAPI"
import { useState, useEffect} from "react"
import { endBreak, startBreak, statusBreak } from "../api/BreakAPI.js"
import "../styles/MainContent.css"

export default function MainContent(props){
    const emplID = props.emplID;
    const [workState, setWorkState] = useState(null);
    const [workTime, setWorkTime] = useState({start: null, end: null});
    const [breakState, setBreakState] = useState(null);
    const [breakTime, setBreakTime] = useState({start: null, end: null});
    const [message, setMessage] = useState("");

    useEffect(() => {
        const fetchStatus = async () => {
            try{
                const workStatus = await statusWorkday(emplID);
                console.log("Work: ", workStatus);
                const breakStatus = await statusBreak(emplID);
                console.log("Break: ", breakStatus);

                if(workStatus.status === "notStarted" || workStatus === null){
                    setWorkState("notStarted");
                    setWorkTime({start: null, end: null});
                }else if(workStatus.status === "working"){
                    setWorkState("working");
                    setWorkTime({start: workStatus.startTime, end: workStatus.endTime});
                }else if(workStatus.status === "ended"){
                    setWorkState("ended");
                    setWorkTime({start: workStatus.startTime, end: workStatus.endTime});
                }
                
                if(breakStatus === null || breakStatus.stauts === "notStarted"){
                    setBreakState("notStarted");
                    setBreakTime({start: null, end:null});
                }else if(breakStatus.statis == "onBreak"){
                    setBreakState("onBreak");
                    setBreakTime({start: breakStatus.startTime, end: breakStatus.endTime});
                }else if(breakStatus.status === "ended"){
                    etBreakState("ended");
                    setBreakTime({start: breakStatus.startTime, end: breakStatus.endTime});
                }
            }catch(error){
                setMessage(error.message);
            }
        }
        fetchStatus();
    }, [emplID]);

    const handleWorkAction = async (apiFunc) => {
        try{
            await apiFunc(emplID);
            await fetchStatus();
        }catch (error){
            setMessage(error.message);
        }
    };

    const handleBreakAction = async (apiFunc) => {
        try{
            await apiFunc(emplID);
            await fetchStatus();
        }catch (error){
            setMessage(error.message);
        }
    };

    let workButton, breakButton;
    if (workState === "notStarted") {
        workButton = <Button label="Start Workday" onClick={() => handleWorkAction(startWorkday)} />
    } else if (workState === "working" || workState === "onBreak") {
        workButton = <Button label="End Workday" onClick={() => handleWorkAction(endWorkday)} />
    } else {
        workButton = <Button label="End Workday" disabled={true} />;
    };

    if (breakState === "notStarted" && workState === "working") {
        breakButton = <Button label="Start Break" onClick={() => handleBreakAction(startBreak)} />
    } else if (breakState === "onBreak") {
        breakButton = <Button label="End Break" onClick={() => handleBreakAction(endBreak)} />
    } else {
        breakButton = <Button label="Start Break" disabled={true} />;
    };

    return(
        <main>
            <div className="buttonNInfo">
                {workButton}
                <Timelaps mode="work" start={workTime.start} end={workTime.end}/>
            </div>
            <div className="buttonNInfo">
                {breakButton}
                <Timelaps mode="break" start={workTime.start} end={workTime.end}/>
            </div>
        </main>
    )
}