import React from 'react'
import { Route } from "react-router-dom";

function UnprotectedRoute({ path, renderComponent }) {
    return (
        <Route exact path={path}>
            {renderComponent()}
        </Route>
    )
}

export default UnprotectedRoute