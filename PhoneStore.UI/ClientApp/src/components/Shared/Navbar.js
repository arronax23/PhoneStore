import React from 'react'
import BottomNavigation from '@material-ui/core/BottomNavigation'
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction'
import { useHistory } from 'react-router';
import { useState } from 'react';

function Navbar({ authorizationStatus, username }) {
    const history = useHistory();
    const [currentTab, setcurrentTab] = useState(0)

    const homeClicked = () => {
        history.push("/");
    }

    const phoneListClicked = () => {
        history.push("/phonelist");
    }    

    const addPhoneClicked = () => {
        history.push("/addphone");
    }    

    const register = () => {
        history.push("/register");
    }  

    const login = () => {
        history.push("/login");
    }  
    const logout = () => {
        history.push("/logout");
    }  

    const orders = () => {
        history.push("/orders");
    } 
    const swagger = () => {
        window.location.href= '/swagger'
    }

    return (
        <nav>
        <BottomNavigation 
            className="bottomNavigation"
            showLabels
            value={currentTab}
            onChange={(e, newTab)=> {setcurrentTab(newTab)}}
        >
            <BottomNavigationAction label="Home" onClick={homeClicked} />
            {authorizationStatus != 'Unauthorized' ?
            <BottomNavigationAction label="Phones" onClick={phoneListClicked}/>
            : null}     
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction label="Add phone" onClick={addPhoneClicked}/>
            : null}    
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction  label="Swagger" onClick={swagger}/>
            : null}  
            {authorizationStatus == 'Customer' ?
            <BottomNavigationAction  label="Orders" onClick={orders}/>
            : null} 
            {authorizationStatus == 'Unauthorized' ?
            <BottomNavigationAction className="login-register" label="Login" onClick={login}/>
            : null}    
            {authorizationStatus == 'Unauthorized' ?
            <BottomNavigationAction className="login-register" label="Register" onClick={register}/>
            : null}     
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction className="role" label="Role: ADMIN"  disabled/>
            : null}    
            {authorizationStatus == 'Customer' ?
            <BottomNavigationAction className="role" label="Role: CUSTOMER"  disabled/>
            : null}       
            {username != '' ?
            <BottomNavigationAction className="username" label={`User: ${username}`}  disabled/>
            : null}      
            {authorizationStatus != 'Unauthorized' ?
            <BottomNavigationAction className="login-register" label="Logout" onClick={logout} />
            : null}                    
        </BottomNavigation>
        </nav>
    )
}

export default Navbar
