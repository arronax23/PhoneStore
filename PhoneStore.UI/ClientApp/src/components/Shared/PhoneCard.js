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
        <Grid item xs={12} sm={4} md={2} className="phone-grid-item">
            <Card className="phone-card" >
                <CardContent className="phone-card-content">
                    <Typography color="textSecondary">
                        {phone.brand} {phone.model}
                    </Typography>
                    <img className="phone-card-img" src={phone.imageUrl} />
                    <Button onClick={handleClick} color="primary" size="small">Show details</Button>
                </CardContent>
            </Card>
        </Grid>
    )
}

export default PhoneCard
