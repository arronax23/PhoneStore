import React from 'react'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import CardActions from '@material-ui/core/CardActions'
import Typography from '@material-ui/core/Typography'
import CardMedia from '@material-ui/core/CardMedia'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'
import { useHistory } from 'react-router'

function PhoneCard({ phone }) {
    const history = useHistory();
    const handleClick = () => {
        history.push('/phonedetails/'+ phone.phoneId)
    }
    return (
        <Grid item xs={12} sm={4} md={2} className="phone-card">
            <Card >
                <CardContent>
                    <Typography color="textSecondary">
                        {phone.brand} {phone.model}
                    </Typography>
                    <img className="phone-card-img" src={phone.imageUrl} />
                </CardContent>
                <Button onClick={handleClick} color="primary">Show details</Button>
            </Card>
        </Grid>
    )
}

export default PhoneCard
