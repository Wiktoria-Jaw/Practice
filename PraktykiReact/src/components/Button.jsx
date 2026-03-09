<<<<<<< HEAD
export default function Button({label, onClick}){
    return(
        <>
            <button className="ButtonState" onClick={onClick}>{label}</button>
=======
export default function Button(){
    //const [workState, setWorkState] = React.use()

    // function State(){
    //     if()
    //     setWorkState()
    // }

    return(
        <>
            {/* <button className="ButtonState" onClick="State">{workState}</button> */}
            <button className="ButtonState" onClick="State">Zacznij prace</button>
>>>>>>> d32fa4a11364822f7ee53419ad19ebd09de9d703
        </>
    )
}