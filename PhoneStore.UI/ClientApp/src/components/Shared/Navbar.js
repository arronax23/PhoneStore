import React, { useRef, useEffect } from 'react'
import BottomNavigation from '@material-ui/core/BottomNavigation'
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction'
import { useHistory } from 'react-router';
import { useState } from 'react';

function Navbar({ authorizationStatus, username }) {
    const history = useHistory();
    const bar = useRef();
    const [currentTab, setcurrentTab] = useState(0)

    useEffect(() => {
        bar.current.innerHTML = "<i class=\"fa-sharp fa-solid fa-bars\"></i>"  
    });
    

    const homeClicked = () => {
        history.push("/");
    }

    const onBarClick = () => {
        document.querySelectorAll(".btn-nav").forEach((btn)=>{
            btn.classList.toggle("mobile-hide");
        })
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
            <BottomNavigationAction className="bar" ref={bar} onClick={onBarClick} />
            <BottomNavigationAction className="btn-nav mobile-hide" label="Home" onClick={homeClicked} />
            {authorizationStatus != 'Unauthorized' ?
            <BottomNavigationAction className="btn-nav mobile-hide" label="Phones" onClick={phoneListClicked}/>
            : null}     
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction className="btn-nav mobile-hide" label="Add phone" onClick={addPhoneClicked}/>
            : null}    
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction className="btn-nav mobile-hide" label="Swagger" onClick={swagger}/>
            : null}  
            {authorizationStatus == 'Customer' ?
            <BottomNavigationAction className="btn-nav mobile-hide" label="Orders" onClick={orders}/>
            : null} 
            {authorizationStatus == 'Unauthorized' ?
            <BottomNavigationAction className="login-register btn-nav mobile-hide" label="Login" onClick={login}/>
            : null}    
            {authorizationStatus == 'Unauthorized' ?
            <BottomNavigationAction className="login-register btn-nav mobile-hide" label="Register" onClick={register}/>
            : null}     
            {authorizationStatus == 'Admin' ?
            <BottomNavigationAction className="role btn-nav mobile-hide" label="Role: ADMIN"  disabled/>
            : null}    
            {authorizationStatus == 'Customer' ?
            <BottomNavigationAction className="role btn-nav mobile-hide" label="Role: CUSTOMER"  disabled/>
            : null}       
            {username != '' ?
            <BottomNavigationAction className="username btn-nav mobile-hide" label={`User: ${username}`}  disabled/>
            : null}      
            {authorizationStatus != 'Unauthorized' ?
            <BottomNavigationAction className="login-register btn-nav mobile-hide" label="Logout" onClick={logout} />
            : null}                    
        </BottomNavigation>
        </nav>
    )
}

export default Navbar
