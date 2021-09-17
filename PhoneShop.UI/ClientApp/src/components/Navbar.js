import React from 'react'
import BottomNavigation from '@material-ui/core/BottomNavigation'
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction'
import { useHistory } from 'react-router';

function Navbar() {
    const history = useHistory();

    const homeClicked = () => {
        history.push("/");
    }

    const phoneListClicked = () => {
        history.push("/phonelist");
    }    

    const addPhoneClicked = () => {
        history.push("/addphone");
    }    

    return (
        <div>
        <BottomNavigation showLabels>
            <BottomNavigationAction label="Home" onClick={homeClicked} />
            <BottomNavigationAction label="Phones' list" onClick={phoneListClicked}/>
            <BottomNavigationAction label="Add phone" onClick={addPhoneClicked}/>
            <BottomNavigationAction className="login" label="Login" onClick={addPhoneClicked}/>
        </BottomNavigation>
        </div>
    )
}

export default Navbar
