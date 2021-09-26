import React from 'react'
import Typography from '@material-ui/core/Typography'
import Button from '@material-ui/core/Button'
import Grid from '@material-ui/core/Grid'
import { useParams, useHistory } from 'react-router-dom'
import useFetchGet from '../../customHooks/useFetchGet'
import { useSelector } from 'react-redux'
import { ContactSupportOutlined } from '@material-ui/icons'

const phoneColor = ["White","Black","Red","Blue","Pink"]

function PhoneDetails({ isAdmin }) {
    const history = useHistory();
    const { id } = useParams();
    const username = useSelector(state => state.username);
    const {data: phone, isPending, error} = useFetchGet("api/GetPhoneById/"+id);

    const deleteClick = () => {
        fetch('api/DeletePhoneById/'+id, 
        {
            method: 'DELETE'
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

    const addToShoppingCardClick = () => {
        fetch('api/GetCustomerIdByUsername/'+username)
        .then(response => response.text())
        .then(customerId => {
            fetch('api/AddPhoneToShoppingCard',{
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                },
                body : JSON.stringify({customerId: parseInt(customerId), phoneId: parseInt(id)})
            })
            .then(response => {
                if (response.ok){
                    console.log('everything ok');
                }
                else{
                    console.log('something went wrong')
                }
            })
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
                        <dt> RAM[GB]</dt>                  
                        <dd>{phone.ram}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Camera [Mpx]</dt>
                        <dd>{phone.camera}</dd>
                    </div>
                    <div className="definition-list-item">
                        <dt>Memory[GB]</dt>
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
                        <dt>Price</dt>
                        <dd>{phone.price}&nbsp;$</dd>
                    </div>
                </dl>
                {/* Admin */}
                {isAdmin && 
                <div>
                    <Button onClick={updateClick} variant="contained" color="primary">Update</Button>
                    <Button onClick={deleteClick} variant="contained" color="secondary">Delete</Button>
                </div>}
                {/* Customer */}
                {!isAdmin && 
                <div>
                    <Button onClick={addToShoppingCardClick} variant="contained" color="secondary">Add to shopping card</Button>
                </div>}                
                
            </div>
        </div>}
        </div>
    )
}

export default PhoneDetails
