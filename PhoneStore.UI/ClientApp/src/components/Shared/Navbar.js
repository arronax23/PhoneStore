import React from 'react'
import BottomNavigation from '@material-ui/core/BottomNavigation'
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction'
import { useHistory } from 'react-router';
import { useSelector } from 'react-redux'
import { useState } from 'react';

function Navbar() {
    const history = useHistory();
    const logging = useSelector(state => state.logging);
    const username = useSelector(state => state.username)
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
            {logging != 'NOT_LOGGED' ?
            <BottomNavigationAction label="Phones" onClick={phoneListClicked}/>
            : null}     
            {logging == 'LOGGED_AS_ADMIN' ?
            <BottomNavigationAction label="Add phone" onClick={addPhoneClicked}/>
            : null}    
            {logging == 'LOGGED_AS_ADMIN' ?
            <BottomNavigationAction  label="Swagger" onClick={swagger}/>
            : null}  
            {logging == 'LOGGED_AS_CUSTOMER' ?
            <BottomNavigationAction  label="Orders" onClick={orders}/>
            : null} 
            {logging == 'NOT_LOGGED' ?
            <BottomNavigationAction className="login-register" label="Login" onClick={login}/>
            : null}    
            {logging == 'NOT_LOGGED' ?
            <BottomNavigationAction className="login-register" label="Register" onClick={register}/>
            : null}     
            {logging == 'LOGGED_AS_ADMIN' ?
            <BottomNavigationAction className="role" label="Role: ADMIN"  disabled/>
            : null}    
            {logging == 'LOGGED_AS_CUSTOMER' ?
            <BottomNavigationAction className="role" label="Role: CUSTOMER"  disabled/>
            : null}       
            {username != '' ?
            <BottomNavigationAction className="username" label={`User: ${username}`}  disabled/>
            : null}      
            {logging != 'NOT_LOGGED' ?
            <BottomNavigationAction className="login-register" label="Logout" onClick={logout} />
            : null}                    
        </BottomNavigation>
        </nav>
    )
}

export default Navbar
