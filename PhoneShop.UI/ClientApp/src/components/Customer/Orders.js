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

function Orders() {
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

    return (
        <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Order Id</TableCell>
              <TableCell >Created date</TableCell>
              <TableCell >Modified date</TableCell>
              <TableCell >Status</TableCell>
              <TableCell >Details</TableCell>
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
                <TableCell >{order.createdDate.toLocaleString()}</TableCell>
                <TableCell >{order.modifiedDate.toLocaleString()}</TableCell>
                <TableCell >{orderStatus[order.status]}</TableCell>
                <TableCell ><Button variant="outlined" color="primary">Show</Button></TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    )
}

export default Orders
