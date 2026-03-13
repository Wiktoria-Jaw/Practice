import DayCard from "../components/DayCard"
import {useEffect, useState } from "react";

export default function Calendar(props){
    const [days, setDays] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchDaysOff = async() => {
            try{
                console.log("test pobierania dni wolnych");
                const daysOffData = await getDaysOff();
                
                const daysInMonth = new Date(props.year, props.month, 0).getDate();
                const calenderDay = [];

                for(let i = 1; i<= daysInMonth; i++){ 
                    const currDate = new Date(props.year, props.month -1, i);
                    
                    const employeesForDay = daysOffData.filter( d=> {
                        const start = new Date(d.Start_Date);
                        const end = new Date(d.End_Date);
                        return currDate >= start && currDate <= end;
                    }).map(d=> ({name: d.Name, surname: d.Surname}));

                    calenderDay.push({
                        day: i,
                        employees: employeesForDay
                    });
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
            <div>Loading calender</div>
        )
    }

    return(
        <main>
            <div className="calendar-grid">
                {days.map((day) => (
                    <DayCard
                    key = {day.day}
                    Num = {day.day}
                    employees = {day.employees}
                />
                ))}
            </div>
        </main>
    )
}