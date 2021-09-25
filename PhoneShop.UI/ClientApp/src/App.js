import React from 'react'
import Navbar from './components/Shared/Navbar';
import Footer from './components/Shared/Footer';
import './custom.css'
import { Switch, Route } from 'react-router-dom'
import PhoneList from './components/Shared/PhoneList'
import Home from './components/Shared/Home'
import PhoneDetails from './components/Shared/PhoneDetails';
import AddPhone from './components/Admin/AddPhone';
import Register from './components/Authentication/Register';
import SuccesfullRegistration from './components/Authentication/SuccesfulRegistration';
import Login from './components/Authentication/Login';
import SuccesfullLogin from './components/Authentication/SuccesfullLogin';
import Logout from './components/Authentication/Logout';
import UpdatePhone from './components/Admin/UpdatePhone'
import { useSelector } from 'react-redux'

function App() {
    const logging = useSelector(state => state.logging);
    const isAdmin = logging == "LOGGED_AS_ADMIN" ? true : false;

      return (      
        <div>
        <Navbar />
        <Switch>
            <Route exact path="/">
                <Home />
            </Route>
            <Route exact path="/phonelist">
                <PhoneList />
            </Route>
            <Route exact path="/phonedetails/:id">
                <PhoneDetails isAdmin={isAdmin} />
            </Route>
            <Route exact path="/addphone">
                <AddPhone />
            </Route>
            <Route exact path="/updatephone/:id">
                <UpdatePhone />
            </Route>            
            <Route exact path="/register">
                <Register />
            </Route>  
            <Route exact path="/succesfullregistration/:user">
                <SuccesfullRegistration />
            </Route>     
            <Route exact path="/login">
                <Login />
            </Route>   
            <Route exact path="/succesfulllogin/:user">
                <SuccesfullLogin />
            </Route>    
            <Route exact path="/logout">
                <Logout />
            </Route>                          
        </Switch>
        <Footer />
        </div>
      );
}

export default App