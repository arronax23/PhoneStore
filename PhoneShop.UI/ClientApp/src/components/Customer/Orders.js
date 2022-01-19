import React, {useState, useEffect} from 'react'
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Box from '@material-ui/core/Box';
import Popper from '@material-ui/core/Popper';
import { useSelector } from 'react-redux'
import { makeStyles } from '@material-ui/styles'
import { useHistory } from 'react-router';

const useStyles = makeStyles({
  tableContainer: {
    width: '90%',
    margin: 'auto',
    marginTop: 10,
    height: '87.5vh'
  },
  closeYes: {
    backgroundColor: '#52b202'
  },
  closeNo: {
    backgroundColor: '#b2102f'
  },
  buttonGroup: {
    marginTop: 10
  },
  popperPaper: {
    padding: 10,
    textAlign: 'center'
  }  
})


function Orders() {
    const classes = useStyles();
    const history = useHistory(); 
    const username = useSelector(state => state.username);
    const [customerId, setCustomerId] = useState(0);
    const [orderId, setOrderId] = useState(0);
    const [isPopperOpen, setIsPopperOpen] = useState(false);
    const [anchorEl, setAnchorEl] = useState(null);
    const [isPopperOpenPay, setIsPopperOpenPay] = useState(false);
    const [anchorElPay, setAnchorElPay] = useState(null);
    const orderStatus = ["Open", "Closed", "Paid","Delivered"];
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
    const token = useSelector(state => state.token);
    
    const fetchOrders = () =>{
      fetch('api/GetCustomerIdByUsername/'+username)
      .then(response => response.text())
      .then(id => {
          setCustomerId(parseInt(id));
          fetch('api/GetOrdersByCustomerId/'+id, {
            headers: {
              "Authorization": "bearer "+token
            }
          })
          .then(response => response.json())
          .then(data => {
              console.log(data);
              setOrders(data)
          });
      }); 
    }

    useEffect(() => {
      fetchOrders();
    },[]);

    const showPhones = (e) => {
      if (e.target.className =='MuiButton-label'){
        setOrderId(e.target.parentNode.value);
        history.push('/phonesInOrder/'+e.target.parentNode.value);
      }
      else if (e.target.className =='MuiButtonBase-root MuiButton-root MuiButton-outlined MuiButton-outlinedPrimary'){
        setOrderId(e.target.value);
        history.push('/phonesInOrder/'+e.target.value);
      }
      // console.log(orderId);
    }

    const handlePopper = (e) => {
       setIsPopperOpen((state) => !state);
       setAnchorEl(anchorEl ? null : e.target);

      if (e.target.className =='MuiButton-label'){
        setOrderId(e.target.parentNode.value);
        console.log(e.target.parentNode.value);
      }
      else if (e.target.className =='MuiButtonBase-root MuiButton-root MuiButton-outlined MuiButton-outlinedSecondary'){
        setOrderId(e.target.value);

        console.log(e.target.value);
      }
    }

    const dontCloseOrder = (e) => {
      setIsPopperOpen(false);
      setAnchorEl(null);
    }

    const closeOrder = (e) => {
      fetch('api/ChangeOrderStatus?orderId='+orderId+"&newStatus=Closed",{
        method: 'POST',
        headers: {
          "Authorization": "bearer "+token
        }
      })
      .then(resp => {
        console.log(resp);
        if (resp.ok){
          setIsPopperOpen(false);
          fetchOrders();
        }
        else{
          alert('Closing order went wrong!');
        }
      });
    }

    
    const handlePopperPay = (e) => {
      setIsPopperOpenPay((state) => !state);
      setAnchorElPay(anchorElPay ? null : e.target);

     if (e.target.className =='MuiButton-label'){
       setOrderId(e.target.parentNode.value);
       console.log(e.target.parentNode.value);
     }
     else if (e.target.className =='MuiButtonBase-root MuiButton-root MuiButton-outlined MuiButton-outlinedSecondary'){
       setOrderId(e.target.value);
       console.log(e.target.value);
     }
    }

    const dontPayOrder = (e) => {
      setIsPopperOpenPay(false);
      setAnchorElPay(null);
    }

    const payOrder = (e) => {
      fetch('api/ChangeOrderStatus?orderId='+orderId+"&newStatus=Paid",{
        method: 'POST',
        headers: {
          "Authorization": "bearer "+token
        }
      })
      .then(resp => {
        console.log(resp);
        if (resp.ok){
          setIsPopperOpenPay(false);
          setAnchorElPay(null);
          fetchOrders();
        }
        else{
          alert('Paying for order went wrong!');
        }
      });
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
                <TableCell>
                  <Button aria-describedby="close" value={order.orderId} onClick={handlePopper} variant="outlined" color="secondary">Close</Button>
                  <Popper  id="close" open={isPopperOpen} anchorEl={anchorEl} >
                     <Paper className={classes.popperPaper}>
                       <div>
                          Are you sure to close this order?
                        </div>
                        <div className={classes.buttonGroup}>
                          <Button className={classes.closeYes} onClick={closeOrder}>Yes</Button>
                          <Button className={classes.closeNo} onClick={dontCloseOrder}>No</Button>
                        </div>
                    </Paper>
                  </Popper>
                </TableCell>
                 :
                 orderStatus[order.status] == "Closed" ? 
                 <TableCell>
                   <Button value={order.orderId} onClick={handlePopperPay} variant="outlined" color="secondary">Pay</Button>
                   <Popper  id="close" open={isPopperOpenPay} anchorEl={anchorElPay} >
                     <Paper className={classes.popperPaper}>
                       <div>
                          Are you sure that you want to pay for this order?
                        </div>
                        <div className={classes.buttonGroup}>
                          <Button className={classes.closeYes} onClick={payOrder}>Yes</Button>
                          <Button className={classes.closeNo} onClick={dontPayOrder}>No</Button>
                        </div>
                    </Paper>
                  </Popper>
                 </TableCell>
                :
                orderStatus[order.status] == "Paid" ? 
                <TableCell>
                  <Button value={order.orderId} variant="outlined" color="secondary" disabled>To be delivered</Button>
                </TableCell>
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
