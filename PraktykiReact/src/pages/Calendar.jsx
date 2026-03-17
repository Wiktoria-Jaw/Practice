import DayCard from "../components/DayCard";
import {useEffect, useState } from "react";
import { getDaysOff } from "../api/DaysOffAPI";
import "../styles/Calendar.css";

export default function Calendar(props){
    const [days, setDays] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchDaysOff = async() => {
            try{
                console.log("test pobierania dni wolnych");
                const daysOffData = await getDaysOff();
                console.log(daysOffData);
                
                const daysInMonth = new Date(props.year, props.month, 0).getDate();
                const calenderDay = [];

                for(let i = 1; i<= daysInMonth; i++){ 
                    const currDate = new Date(props.year, props.month -1, i);
                    
                    const employeesForDay = daysOffData.filter( d=> {
                        const start = new Date(d.StartDate);
                        const end = new Date(d.EndDate);
                        return currDate >= start && currDate <= end;
                    }).map(d=> ({FirstName: d.FirstName, MiddleName: d.MiddleName, LastName: d.LastName}));

                    console.log(i, currDate.toDateString(), employeesForDay);

                    calenderDay.push({ day: i, employees: employeesForDay });
                }
                setDays(calenderDay);
                setLoading(false);
            }
            catch(error){
                console.error(error);
                setLoading(false);
            }
        };
        fetchDaysOff();
    }, [props.month, props.year]);

    if(loading){
        return (
            <div>Loading calender...</div>
        )
    }

    let firstDayOfMonth = new Date(props.year, props.month -1,1).getDay();
    firstDayOfMonth = firstDayOfMonth === 0 ? 7 : firstDayOfMonth;

    const weekDays = ["Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"];

    return(
        <main>
            <div className="calendar-header">
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
                        />
                    );
                })}
            </div>
        </main>
    );
}