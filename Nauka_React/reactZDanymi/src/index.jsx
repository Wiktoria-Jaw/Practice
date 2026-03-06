import Contact from "./components/contact.jsx"
import { createRoot } from 'react-dom/client'

const root = createRoot(document.getElementById("root"))

root.render(
    <Index/>
)

function Index(){
    return(
        <div className="contacts">
            <Contact img="./images/mr-whiskerson.png" name="Mr. Whiskerson" phone="(212) 555-1234" mail="mr.whiskaz@catnap.meow"/>
            <Contact img="./images/fluffykins.png" name="Fluffykins" phone="(212) 555-2345" mail="fluff@me.com"/>
            <Contact img="./images/felix.png" name="Felix" phone="(212) 555-4567" mail="thecat@hotmail.com"/>
            <Contact img="./images/pumpkin.png" name="Pumpkin" phone="(0800) CAT KING" mail="pumpkin@scrimba.com"/>
        </div>
    )
}