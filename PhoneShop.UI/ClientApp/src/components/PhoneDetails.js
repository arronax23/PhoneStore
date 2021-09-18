import React from 'react'
import Typography from '@material-ui/core/Typography'
import Button from '@material-ui/core/Button'
import Grid from '@material-ui/core/Grid'
import { useParams, useHistory } from 'react-router-dom'
import useFetchGet from './../customHooks/useFetchGet'

const phoneColor = ["White","Black","Red","Blue","Pink"]

function PhoneDetails() {
    const history = useHistory();
    const { id } = useParams();
    const {data: phone, isPending, error} = useFetchGet("api/GetPhoneById/"+id);
    const deleteClick = () => {
        fetch('/api/DeletePhoneById/'+id, 
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
                </dl>
                <Button onClick={deleteClick} variant="contained" color="secondary">Delete</Button>
            </div>
        </div>}
        </div>
    )
}

export default PhoneDetails