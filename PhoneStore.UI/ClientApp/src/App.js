import React , { useEffect, useState } from "react";
import Navbar from "./components/Shared/Navbar";
import Address from "./components/Shared/Address";
import "./custom.css";
import { Switch, Route, Redirect } from "react-router-dom";
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
import WrongRole from "./components/Authorization/WrongRole";
import UnprotectedRoute from "./components/Authorization/UnprotectedRoute";
import ProtectedRoute from "./components/Authorization/ProtectedRoute";

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
        <ProtectedRoute 
          path="/phonelist"
          authorizationStatus={authorizationStatus} 
          allowAdmin={true} 
          allowCustomer={true}
          renderComponent={()=> <PhoneList />} 
        />
        <ProtectedRoute 
          path="/phonedetails/:id"
          authorizationStatus={authorizationStatus} 
          allowAdmin={true} 
          allowCustomer={true}
          renderComponent={()=> <PhoneDetails authorizationStatus={authorizationStatus} username={username} />} 
        />
        <ProtectedRoute 
          path="/addphone" 
          authorizationStatus={authorizationStatus} 
          allowAdmin={true} 
          renderComponent={()=> <AddPhone />} 
        />  
        <ProtectedRoute 
          path="/orders"
          authorizationStatus={authorizationStatus}
          allowCustomer={true} 
          renderComponent={()=> <Orders username={username} />} 
        />
        <ProtectedRoute 
          path="/phonesInOrder/:id"
          authorizationStatus={authorizationStatus}
          allowCustomer={true} 
          renderComponent={()=> <PhonesInOrder />} 
        />
        <ProtectedRoute 
          path="/updatephone/:id"
          authorizationStatus={authorizationStatus}
          allowAdmin={true} 
          renderComponent={()=> <UpdatePhone />} 
        />
        <UnprotectedRoute 
          path="/register"
          renderComponent={() => <Register />}
        />
        <UnprotectedRoute 
          path="/succesfullregistration/:user"
          renderComponent={() => <SuccesfullRegistration />}
        />
        <UnprotectedRoute 
          path="/login"
          renderComponent={()=> <Login />}
        />
        <UnprotectedRoute 
          path="/succesfulllogin/:user"
          renderComponent={() => <SuccesfullLogin setAuthorizationStatus={setAuthorizationStatus} setUsername={setUsername} />}
        />
        <UnprotectedRoute 
          path="/logout"
          renderComponent={() => <Logout setAuthorizationStatus={setAuthorizationStatus} setUsername={setUsername} />}
        />
        <UnprotectedRoute 
          path="/"
          renderComponent={() => <Home />}
        />
      </Switch>
      <Address />
    </div>
  );
}

export default App;
