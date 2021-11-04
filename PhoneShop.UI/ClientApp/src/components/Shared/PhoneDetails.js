import React, {useEffect, useState} from 'react'
import Typography from '@material-ui/core/Typography'
import Button from '@material-ui/core/Button'
import Grid from '@material-ui/core/Grid'
import { useParams, useHistory } from 'react-router-dom'
import useFetchGet from '../../customHooks/useFetchGet'
import { useSelector } from 'react-redux'
import { ContactSupportOutlined } from '@material-ui/icons'

const phoneColor = ["White","Black","Red","Blue","Pink"]

function PhoneDetails() {
    const logging = useSelector(state => state.logging);
    const isAdmin = logging == "LOGGED_AS_ADMIN" ? true : false;
    const [refreshButton, setRefreshButton] = useState(0);
    const [isInShoppingCart,setIsInShoppingCart] = useState(false);
    const [customerId, setCustomerId] = useState(0);
    const history = useHistory();
    const { id } = useParams();
    const username = useSelector(state => state.username);
    const {data: phone, isPending, error} = useFetchGet("api/GetPhoneById/"+id);
    const token = useSelector(state => state.token);
    
    useEffect(() => {
        if(!isAdmin){
            fetch('api/GetCustomerIdByUsername/'+username)
            .then(response => response.text())
            .then(customerId => {
                setCustomerId(customerId);
                fetch(`api/IsPhoneInShoppingCart/?customerId=${customerId}&phoneId=${id}`, {
                    headers: {
                        "Authorization": "bearer "+token
                      }
                })
                .then(resp => resp.json())
                .then(data => {
                    console.log(data);
                    setIsInShoppingCart(data);
                });
            })
        }
    },[refreshButton])
    const deleteClick = () => {
        fetch('api/DeletePhoneById/'+id, 
        {
            method: 'DELETE',
            headers: {
                "Authorization": "bearer "+token
              }
        })
        .then(resp => {
            console.log("resp:"+resp);
            if (!resp.ok){
                throw new Error('Failed deleting.');
            }
            else {
                history.push('/phonelist');
            }
        })
        .catch(err => console.log("err:"+err.message))
    }

    const updateClick = () => {
        history.push('/updatephone/'+id);
    }

    const addToShoppingCartClick = () => {
        fetch('api/AddPhoneToShoppingCart',{
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
                "Authorization": "bearer "+token
            },
            body : JSON.stringify({customerId: parseInt(customerId), phoneId: parseInt(id)})
        })
        .then(response => {
            if (response.ok){
                console.log('everything ok');
                setRefreshButton((state) => ++state);
            }
            else{
                console.log('something went wrong')
            }
        });
    }
    const removeFromShoppingCartClick = () => {
        fetch('api/RemovePhoneFromShoppingCart',{
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
                "Authorization": "bearer "+token
            },
            body : JSON.stringify({customerId: parseInt(customerId), phoneId: parseInt(id)})
        })
        .then(response => {
            if (response.ok){
                console.log('everything ok');
                setRefreshButton((state) => ++state);
            }
            else{
                console.log('something went wrong')
            }
        });
    }

    return (
        <div >
        {phone &&
        <div className="phone-details">
            <Typography variant="h6">{phone.brand} {phone.model}</Typography>
            <img className="phone-details-img" src={phone.imageUrl}  />
            <div className="dl">
                <dl>
                    <div className="definition-list-item">
                        <dt> RAM [GB]</dt>                  
                        <dd>{phone.ram}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Camera [Mpx]</dt>
                        <dd>{phone.camera}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Memory [GB]</dt>
                        <dd>{phone.memory}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>OS</dt>
                        <dd>{phone.os}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Color</dt>
                        <dd>{phoneColor[phone.color]}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Price [$]</dt>
                        <dd>{phone.price}</dd>
                    </div>
                </dl>
                {/* Admin */}
                {isAdmin && 
                <div>
                    <Button onClick={updateClick} variant="contained" color="primary">Update</Button>
                    <Button onClick={deleteClick} variant="contained" color="secondary">Delete</Button>
                </div>}
                {/* Customer */}
                {!isAdmin && !isInShoppingCart && 
                <div>
                    <Button onClick={addToShoppingCartClick} variant="contained" color="primary">Add to shopping cart</Button>
                </div>}       
                {!isAdmin && isInShoppingCart && 
                <div>
                    <Button onClick={removeFromShoppingCartClick} variant="contained" color="secondary">Remove from shopping cart</Button>
                </div>}            
                
            </div>
        </div>}
        </div>
    )
}

export default PhoneDetails
