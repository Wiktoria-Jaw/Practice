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

    const fetchStatus = async () => {
            try{
                const workStatus = await statusWorkday(emplID);
                const breakStatus = await statusBreak(emplID);
                console.log("Work status: ",workStatus.status);
                console.log("Break status: ", breakStatus.status);
                if(workStatus.status === "working"){
                    setWorkState("working");
                    setWorkTime({start: workStatus.startTime, end: workStatus.endTime});
                }else if(workStatus.status === "finished"){
                    setWorkState("finished");
                    setWorkTime({start: workStatus.startTime, end: workStatus.endTime});
                }else{
                    setWorkState("notStarted");
                    setWorkTime({start: null, end: null});
                }
                
                if(breakStatus === null || breakStatus.status === "notStarted"){
                    setBreakState("notStarted");
                    setBreakTime({start: null, end:null});
                }else if(breakStatus.status == "onBreak"){
                    setBreakState("onBreak");
                    setBreakTime({start: breakStatus.startTime, end: breakStatus.endTime});
                }else if (breakStatus.status === "finished"){
                    setBreakState("finished");
                    setBreakTime({start: breakStatus.startTime, end: breakStatus.endTime});
                }
            }catch(error){
                setMessage(error.message);
            }
        }

    useEffect(() => {
        fetchStatus();
    }, [emplID]);

    const handleWorkAction = async (apiFunc) => {
        try{
            var result = await apiFunc(emplID);
            await fetchStatus();
            setMessage(result.message);
        }catch (error){
            setMessage(error.message);
        }
    };

    const handleBreakAction = async (apiFunc) => {
        try{
            var result = await apiFunc(emplID);
            await fetchStatus();
            setMessage(result.message);
        }catch (error){
            setMessage(error.message);
        }
    };

    let workButton, breakButton;
    if (workState === "notStarted") {
        workButton = <Button label="Start Workday" onClick={() => handleWorkAction(startWorkday)} />
    } else if (workState === "working") {
        workButton = <Button label="End Workday" onClick={() => handleWorkAction(endWorkday)} />
    } else {
        workButton = <Button label="End Workday" disabled={true} />;
    };

    if (breakState === "notStarted" && workState === "working") {
        breakButton = <Button label="Start Break" onClick={() => handleBreakAction(startBreak)} />
    } else if (breakState === "onBreak" && workState === "working") {
        breakButton = <Button label="End Break" onClick={() => handleBreakAction(endBreak)} />
    } else if (breakState === "finished" && workState === "working" ) {
        breakButton = <Button label="Start Break" onClick={() => handleBreakAction(startBreak)} />
    } else {
        breakButton = <Button label="Start Break" disabled={true} />;
    };

    return(
        <>
            <main>
                <div className="buttonNInfo">
                    {workButton}
                    <Timelaps mode="work" start={workTime.start} end={workTime.end}/>
                </div>
                <div className="buttonNInfo">
                    {breakButton}
                    <Timelaps mode="break" start={breakTime.start} end={breakTime.end}/>
                </div>
            </main>
            {!!message && (<p className="errorParagraf">Information: {message}</p>)}
        </>
    )
}