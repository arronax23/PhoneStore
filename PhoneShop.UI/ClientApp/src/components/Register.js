import React, {useState} from 'react'

function Register() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

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
            if (!resp.ok){
                throw new Error('Registering user went wrong!');
            }
            else{
                console.log('Succesfully registered user')
            }
        })
        .catch(err =>console.log(err.message));
    }

    return (
        <div>
            <form onSubmit={onSubmit}>
                <label htmlFor="username">Username</label>
                <input type="text" name="username" value={username} onChange={(e)=> setUsername(e.target.value)} />
                <label htmlFor="password">Password</label>
                <input type="text" name="password" value={password} onChange={(e)=> setPassword(e.target.value)} />
                <button type="submit">Register</button>
            </form>
        </div>
    )
}

export default Register
