import DayCard from "../components/DayCard";
import Button from "../components/Button";
import {useEffect, useState } from "react";
import { getAcceptedDaysOff, declareDayOff } from "../api/DaysOffAPI";
import "../styles/Calendar.css";

export default function Calendar(props){
    const [month, setMonth] = useState(props.month);
    const [year, setYear] = useState(props.year);
    const [days, setDays] = useState([]);
    const [loading, setLoading] = useState(true);
    const [declarationMode, setDeclarationMode] = useState(false);
    const [selectedDays, setSelectedDays] = useState([]);
    const [message, setMessage] = useState("");

    useEffect(() => {
        const fetchDaysOff = async() => {
            try{
                const daysOffData = await getAcceptedDaysOff();
                const daysInMonth = new Date(year, month, 0).getDate();
                const calenderDay = [];

                for(let i = 1; i<= daysInMonth; i++){ 
                    const currDate = new Date(year, month -1, i);
                    
                    const employeesForDay = daysOffData.filter( d=> {
                        const start = new Date(d.startDate);
                        const end = new Date(d.endDate);
                        
                        start.setHours(0,0,0,0);
                        end.setHours(0,0,0,0);
                        currDate.setHours(0,0,0,0);

                        return currDate >= start && currDate <= end;
                    }).map(d=> ({FirstName: d.firstName, MiddleName: d.middleName, LastName: d.lastName}));

                    calenderDay.push({ day: i, employees: employeesForDay });
                }
                setDays(calenderDay);
                setLoading(false);
            }
            catch(error){
                console.error(error);
                setMessage(String(error));
                setLoading(false);
            }
        };
        fetchDaysOff();
    }, [month, year]);

    const nextMonth = () => {
        if(month === 12){
            setMonth(1);
            setYear(year+1);
        }else{
            setMonth(month + 1)
        }
    };

    const previousMonth = () => {
        if(month === 1){
            setMonth(12);
            setYear(year-1);
        }else{
            setMonth(month - 1)
        }
    }

    const handleDayClick = (day) => {
        if(!declarationMode){
            return;
        };
        if(selectedDays.includes(day)){
            setSelectedDays(selectedDays.filter((d) => d !== day));
        }else{
            setSelectedDays([...selectedDays, day]);
        }
    };

    const declareSelectedDays = async () => {
        if(selectedDays.length === 0){
            return;
        }

        const sortedDays = [...selectedDays].sort((a,b)=>a-b);
        const startDate = new Date(year, month-1,sortedDays[0]);
        const endDate = new Date(year, month-1, sortedDays[sortedDays.length-1]);

        try {
            var result = await declareDayOff(props.emplID, startDate.toISOString().split("T")[0], endDate.toISOString().split("T")[0]);
            setMessage(result.message);
        }catch(error){
            console.error(error);
            setMessage(result.message);
        }
        setSelectedDays([]);
        setDeclarationMode(false);
    }

    if(loading){
        return (
            <div className="loading">Loading calender...</div>
        )
    }

    let firstDayOfMonth = new Date(year, month -1,1).getDay();
    firstDayOfMonth = firstDayOfMonth === 0 ? 7 : firstDayOfMonth;

    const weekDays = ["Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"];
    const months = ["January","February","March","April","May","June","July","August","September","October","November","December"];
    const monthName = months[month-1];
    const Info = message !== "";

    return(
        <main className="calendar">
            <div className="calendar-header">
                <Button label="<-" onClick={() => previousMonth()}/>
                <span>{monthName} {year}</span>
                <Button label="->" onClick={() => nextMonth()}/>
            </div>
            <div className="calendar-dayheader">
                {weekDays.map((d, idx) => (
                    <div key={idx} className = "day-header">{d}</div>
                ))}
            </div>
            <div className="calendar-grid">
                {days.map((day, index) => {
                    const style = index === 0 ? {gridColumnStart: firstDayOfMonth} : {};
                    return (
                        <DayCard
                            key = {day.day}
                            Num = {day.day}
                            employees = {day.employees}
                            style={style}
                            isSelected = {selectedDays.includes(day.day)}
                            onClick={()=>handleDayClick(day.day)}
                        />
                    );
                })}
            </div>
            <div className="action-buttons">
                <Button label={declarationMode ? "Cancel Declaration" : "Declare Day Off"} onClick={() => {
                    if(declarationMode) setSelectedDays([]);
                    setDeclarationMode(!declarationMode)}}/>
                {declarationMode && (<Button label={"Accept Declaration"} onClick={declareSelectedDays}/>)}
            </div>
            {Info && <span className="message">{message}</span>}
        </main>
    );
}