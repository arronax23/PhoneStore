import React from 'react'
import { useState } from 'react';
import { useEffect } from 'react';
import { Route, Redirect } from "react-router-dom";
import WrongRole from './WrongRole';
import Login from './../Authentication/Login';

function ProtectedRoute({ path, renderComponent, authorizationStatus, allowAdmin, allowCustomer}) {
    if (authorizationStatus === "Admin" && allowAdmin) {
        return (
            <Route path={path}>
               {renderComponent()}
            </Route>
        )
    }
    else if (authorizationStatus === "Customer" && allowCustomer){
        return (
            <Route path={path}>
            {renderComponent()}
        </Route>
        )
    }
    else if (authorizationStatus !== "Unauthorized"){
        console.log(authorizationStatus);
        return (
            <Route path={path}>
                <WrongRole path={path} />
            </Route>
        )
    }
    else {
        return (
            <Route path={path}>
                <Login />
            </Route>
        )
    }
}

export default ProtectedRoute