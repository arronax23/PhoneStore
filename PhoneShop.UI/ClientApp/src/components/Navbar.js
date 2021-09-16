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
    return (
        <div>
        <BottomNavigation showLabels>
            <BottomNavigationAction label="Home" value="some1" onClick={homeClicked} />
            <BottomNavigationAction label="Phones' list" value="some2" onClick={phoneListClicked}/>
            <BottomNavigationAction label="Add phone" value="some3"/>
        </BottomNavigation>
        </div>
    )
}

export default Navbar
