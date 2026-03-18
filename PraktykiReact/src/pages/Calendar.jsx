import DayCard from "../components/DayCard";
import Button from "../components/Button";
import {useEffect, useState } from "react";
import { getDaysOff } from "../api/DaysOffAPI";
import "../styles/Calendar.css";

export default function Calendar(props){
    const [month, setMonth] = useState(props.month);
    const [year, setYear] = useState(props.year);
    const [days, setDays] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchDaysOff = async() => {
            try{
                console.log("test pobierania dni wolnych");
                const daysOffData = await getDaysOff();
                console.log(daysOffData);
                
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

    if(loading){
        return (
            <div>Loading calender...</div>
        )
    }

    let firstDayOfMonth = new Date(year, month -1,1).getDay();
    firstDayOfMonth = firstDayOfMonth === 0 ? 7 : firstDayOfMonth;

    const weekDays = ["Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"];
    const months = ["January","February","March","April","May","June","July","August","September","October","November","December"];
    const monthName = months[month-1];

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
                        />
                    );
                })}
            </div>
        </main>
    );
}