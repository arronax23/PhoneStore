import React, {useState, useEffect} from 'react'
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import { useSelector } from 'react-redux'
import { makeStyles } from '@material-ui/styles'
import { useHistory } from 'react-router';

const useStyles = makeStyles({
  tableContainer: {
    width: '90%',
    margin: 'auto',
    marginTop: 10
  }
})

function Orders() {
    const classes = useStyles();
    const history = useHistory(); 
    const username = useSelector(state => state.username);
    const [customerId, setCustomerId] = useState(0);
    const orderStatus = ["Open", "Closed", "Paid","Delivered"]
    const [orders, setOrders] = useState(
        [ 
            {
                orderId: 0,
                createdDate: new Date(),
                modifiedDate: new Date(),
                status: "Open",          
            }
        ]
    )
    useEffect(() => {
        fetch('api/GetCustomerIdByUsername/'+username)
        .then(response => response.text())
        .then(id => {
            setCustomerId(parseInt(id));
            fetch('api/GetOrdersByCustomerId/'+id)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                setOrders(data)
            });
        }); 
    },[]);

    const showPhones = (e) => {
      let orderId;
      if (e.target.className =='MuiButton-label'){
        orderId = e.target.parentNode.value;
      }
      else if (e.target.className =='MuiButtonBase-root MuiButton-root MuiButton-outlined MuiButton-outlinedPrimary'){
        orderId = e.target.value;
      }
      // console.log(orderId);
      history.push('/phonesInOrder/'+orderId);
    }

    return (
        <TableContainer className={classes.tableContainer} component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Order Id</TableCell>
              <TableCell>Created date</TableCell>
              <TableCell>Modified date</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Details</TableCell>
              <TableCell>Action</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {orders.map((order) => (
              <TableRow
                key={order.orderId}
              >
                <TableCell component="th" scope="row">
                  {order.orderId}
                </TableCell>
                <TableCell>{order.createdDate.toLocaleString()}</TableCell>
                <TableCell>{order.modifiedDate.toLocaleString()}</TableCell>
                <TableCell>{orderStatus[order.status]}</TableCell>
                <TableCell><Button value={order.orderId} onClick={showPhones} variant="outlined" color="primary">Show</Button></TableCell>
                {orderStatus[order.status] == "Open" ?
                <TableCell><Button value={order.orderId} onClick={showPhones} variant="outlined" color="secondary">Close</Button></TableCell>
                 :
                 orderStatus[order.status] == "Closed" ? 
                 <TableCell><Button value={order.orderId} onClick={showPhones} variant="outlined" color="secondary">Pay</Button></TableCell>
                :
                null
                }
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    )
}

export default Orders
