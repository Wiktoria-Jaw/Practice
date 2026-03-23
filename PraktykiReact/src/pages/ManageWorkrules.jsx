import "../styles/ManageWorkrules.css"
import { useState, useEffect } from "react";
import Button from "../components/Button";
import WorkRulesPar from "../components/WorkRulesPar.jsx";
import {GetWorkSettings, UpdateWorkSettings} from "../api/WorkrulesAPI.js";

export default function ManageWorkrules(){
    const [workrules, setWorkrules] = useState({ MinWorkdayLength: 0, AutoEndWorkday: 0, MinBreakBetweenWorkdays: 0, MinWorkdayLengthForBreak: 0, MinBreakLength: 0});
    const [message, setMessage] = useState("");
    const [MinWorkdayLength, setMinWorkdayLength] = useState("")
    const [AutoEndWorkday, setAutoEndWorkday] = useState("")
    const [MinBreakBetweenWorkdays, setMinBreakBetweenWorkdays] = useState("")
    const [MinWorkdayLengthForBreak, setMinWorkdayLengthForBreak] = useState("")
    const [MinBreakLength, setMinBreakLength] = useState("")

    const fetchWorkRules = async() => {
        try{
            const data = await GetWorkSettings();
            setWorkrules(data);
        }catch(error){
            setMessage(error.message);
        }
    }

    useEffect(()=>{
        fetchWorkRules();
    },[]);

    const handleChange = async(e) => {
        e.preventDefault();
        try{
            const result = await UpdateWorkSettings({MinWorkdayLength, AutoEndWorkday, MinBreakBetweenWorkdays, MinWorkdayLengthForBreak, MinBreakLength})
            setMessage(result.message);
        }catch(error){
            setMessage(error.message);
        }
    }

    return(
        <main className="manage-workrules">
            <h2>Manage Work Rules</h2>
            <p className="info">Leave inputs blank, if you want to keep current value.</p>
            <p className="info">Put values in minutes, please.</p>
            <div className="workrules">
                <WorkRulesPar label="Minimum Workday" name="MinWorkdayLength" currValue={workrules.MinWorkdayLength} value={MinWorkdayLength} setter={setMinWorkdayLength}/>
                <WorkRulesPar label="Auto End Time"  name="AutoEndWorkday" currValue={workrules.AutoEndWorkday} value={AutoEndWorkday} setter={setAutoEndWorkday}/>
                <WorkRulesPar label="Min Break Between Workdays" name="MinBreakBetweenWorkdays" currValue={workrules.MinBreakBetweenWorkdays} value={MinBreakBetweenWorkdays} setter={setMinBreakBetweenWorkdays}/>
                <WorkRulesPar label="Min Workday for Brea" name="MinWorkdayLengthForBreak" currValue={workrules.MinWorkdayLengthForBreak} value={MinWorkdayLengthForBreak} setter={setMinWorkdayLengthForBreak}/>
                <WorkRulesPar label="Minimum Break" name="MinBreakLength" currValue={workrules.MinBreakLength} value={MinBreakLength} setter={setMinBreakLength}/>
            </div>
            <Button onClick={() => handleChange} label="Update Work Rules"/>
            {!!message && (<p className="info">{message}.</p>)}
        </main>
    )
}