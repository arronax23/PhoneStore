import React, {useState} from 'react'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import MenuItem from '@material-ui/core/MenuItem'
import {makeStyles} from '@material-ui/styles'
import {useHistory} from 'react-router'

const useStyle = makeStyles({
    root: {
        width: '75%',
        marginBottom: 16
    }
});

function AddPhone() {
    const classes = useStyle();
    const history = useHistory();

    const [brand, setBrand] = useState('');
    const [model, setModel] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [ram, setRam] = useState(0);
    const [camera, setCamera] = useState(0);
    const [memory, setMemory] = useState(0);
    const [os, setOs] = useState('');
    const [color, setColor] = useState(0);

    const [error, setError] = useState();

    const onSubmit = (e) => {
        e.preventDefault();
        const phone = {brand, model, imageUrl, ram: parseInt(ram),camera: parseInt(camera),memory: parseInt(memory), os, color: parseInt(color)};
        console.log(phone);
        fetch('api/SavePhone', 
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(phone)  
            })
        .then(response => {
            if (!response.ok){
                alert('Error');
                throw new Error("Something went wrong!");
            }
            else{
                history.push('/phonelist');
            }
        })
        .catch(err => {
            setError(err.message)
            console.log(error);
        });
    }
    return (
        <div className="form">
             <form onSubmit={onSubmit}>
                    <TextField className={classes.root} type="text" label="Brand" value={brand} onChange={(e)=> setBrand(e.target.value)} />
                    <TextField className={classes.root} type="text" label="Model" value={model} onChange={(e)=> setModel(e.target.value)} />
                    <TextField className={classes.root} type="text" label="Image URL" value={imageUrl} onChange={(e)=> setImageUrl(e.target.value)} />
                    <TextField className={classes.root} type="number" label="RAM [GB]" value={ram} onChange={(e)=> setRam(e.target.value)} />
                    <TextField className={classes.root} type="number" label="Camera [Mpx]" value={camera} onChange={(e)=> setCamera(e.target.value)} />          
                    <TextField className={classes.root} type="number" label="Memory [GB]" value={memory} onChange={(e)=> setMemory(e.target.value)} />
                    <TextField className={classes.root} type="text" label="OS" value={os} onChange={(e)=> setOs(e.target.value)} />
                    <TextField className={classes.root} select={true} label="Color" value={color} onChange={(e)=> setColor(e.target.value)} >
                        <MenuItem value={0}>White</MenuItem>
                        <MenuItem value={1}>Black</MenuItem>
                        <MenuItem value={2}>Red</MenuItem>
                        <MenuItem value={3}>Blue</MenuItem>
                        <MenuItem value={4}>Pink</MenuItem>
                    </TextField>                         
                <Button type="submit" variant="contained" color="primary">Add phone</Button>
            </form>           
        </div>
    )
}

export default AddPhone
