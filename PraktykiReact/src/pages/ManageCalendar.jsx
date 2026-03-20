import { adminGetAcceptedDaysOff, adminGetPendingDaysOff, adminGetRejectedDaysOff, DecideDayOff} from "../api/DaysOffAPI";
import "../styles/ManageCalendar.css";
import { useState, useEffect } from "react";
import Button from "../components/Button";

export default function ManageCalendar(){
    const [pendingDaysOff, setPendingDaysOff] = useState([]);
    const [message, setMessage] = useState("");

    const fetchPendingDaysOff = async() =>{
        try{
            const data = await adminGetPendingDaysOff();
            setPendingDaysOff(data);
        }catch(error){
            setMessage(error.message);
        }
    }

    useEffect(() => {
        fetchPendingDaysOff();
    }, []);

    const handleDecision = async (dayoffID, status) => {
        try{
            const result = await DecideDayOff({DayoffID: dayoffID, Status: status});
            setMessage(result.message);
            fetchPendingDaysOff()
        }catch (error){
            setMessage(error.message);
        }
    }

    return(
        <main className="manage-calendar">
            <h2>Pending Day Off Requests</h2>
            {pendingDaysOff.length === 0 && (<p className="information">No pending requests</p>)}

            {pendingDaysOff.length > 0 && (
                <table className="pending-table">
                    <thead>
                        <tr>
                            <th>Employee</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {pendingDaysOff.map((req) => (
                            <tr key={req.id}>
                                <td>{req.firstName} {req.middleName} {req.lastName}</td>
                                <td>{req.startDate}</td>
                                <td>{req.endDate}</td>
                                <td className="actions"> 
                                    <Button class="actionButton" label="Accept" onClick={() => handleDecision(req.id, "accepted")}/>
                                    <Button class="actionButton" label="Reject" onClick={() => handleDecision(req.id, "rejected")}/>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
            {message && <p className="information">{message}</p>}
        </main>
    )
}