import React , { useEffect, useState } from "react";
import Navbar from "./components/Shared/Navbar";
import Address from "./components/Shared/Address";
import "./custom.css";
import { Switch, Route } from "react-router-dom";
import PhoneList from "./components/Shared/PhoneList";
import Home from "./components/Shared/Home";
import PhoneDetails from "./components/Shared/PhoneDetails";
import AddPhone from "./components/Admin/AddPhone";
import Register from "./components/Authentication/Register";
import SuccesfullRegistration from "./components/Authentication/SuccesfulRegistration";
import Login from "./components/Authentication/Login";
import SuccesfullLogin from "./components/Authentication/SuccesfullLogin";
import Logout from "./components/Authentication/Logout";
import UpdatePhone from "./components/Admin/UpdatePhone";
import Orders from "./components/Customer/Orders";
import PhonesInOrder from "./components/Customer/PhonesInOrder";
import Unauthorized from "./components/Shared/Unauthorized";

function App() {
  const [authorizationStatus, setAuthorizationStatus] = useState("Unauthorized");
  const [username, setUsername] = useState("");
  
  useEffect(()=> {  
    fetch("api/Authorize")
    .then(response => response.json())
    .then(data => {
      setAuthorizationStatus(data.role)
      setUsername(data.name)
    });
  },[]);

  return (
    <div className="main">
      <Navbar authorizationStatus={authorizationStatus} username={username} />
      <Switch>
        <Route exact path="/">
          <Home />
        </Route>
        <Route exact path="/phonelist">
          <PhoneList />
        </Route>
        <Route exact path="/phonedetails/:id">
          <PhoneDetails authorizationStatus={authorizationStatus} username={username} />
        </Route>
        <Route exact path="/addphone">
          <AddPhone />
        </Route>
        <Route exact path="/orders">
          <Orders username={username} />
        </Route>
        <Route exact path="/phonesInOrder/:id">
          <PhonesInOrder />
        </Route>
        <Route exact path="/updatephone/:id">
          <UpdatePhone />
        </Route>
        <Route exact path="/unauthorized">
          <Unauthorized />
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
          <SuccesfullLogin setAuthorizationStatus={setAuthorizationStatus} setUsername={setUsername} />
        </Route>
        <Route exact path="/logout">
          <Logout setAuthorizationStatus={setAuthorizationStatus} setUsername={setUsername} />
        </Route>
      </Switch>
      <Address />
    </div>
  );
}

export default App;
