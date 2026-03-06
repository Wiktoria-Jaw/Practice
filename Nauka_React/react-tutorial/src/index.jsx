import { createRoot } from "react-dom/client"
import Header from "./components/Header"
import MainContent from "./components/MainContent"

const root = createRoot(document.getElementById("root"))

root.render(
    <Page/>
)

function Page(){
    return(
        <>
            <Header />
            <MainContent />
        </>
    )
}