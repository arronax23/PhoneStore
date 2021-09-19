import React, {useState} from 'react'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles({
    root: {
        width: '75%',
        marginBottom: 16,
        marginTop: 16
    }
});

function Register() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState();
    const classes = useStyles();

    const onSubmit = (e) => {
        e.preventDefault();
        fetch('api/Register',{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({username, password})
        })
        .then(resp => {
            console.log(resp);
            return resp.json();
            // if (!resp.ok){
            //     throw new Error('Registering user went wrong!');
            // }
            // else{
            //     console.log('Succesfully registered user')
            // }
        })
        .then(data => setError(data.detail))
        .catch(err =>console.log(err.message));
    }

    return (
        <div>
            <form onSubmit={onSubmit} className="form">
                <div>
                <TextField className={classes.root} variant="outlined" type="text" label="Username" value={username} onChange={(e)=> setUsername(e.target.value)} />
                </div>
                <div>
                    {error ? <TextField className={classes.root} error helperText={error} variant="outlined" type="text" label="Password" value={password} onChange={(e)=> setPassword(e.target.value)} />
                     : <TextField className={classes.root} variant="outlined" type="password" label="Password" value={password} onChange={(e)=> setPassword(e.target.value)} />}
                </div>
                <div>
                <Button type="submit" variant="contained" color="primary">Register</Button>
                </div>
            </form>
        </div>
    )
}

export default Register
