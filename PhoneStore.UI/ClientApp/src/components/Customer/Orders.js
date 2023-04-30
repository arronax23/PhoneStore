import React, {useState, useEffect} from 'react'
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Popper from '@material-ui/core/Popper';
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


function Orders({username}) {
    const classes = useStyles();
    const history = useHistory(); 
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
    
    const fetchOrders = () =>{
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
    }

    useEffect(() => {
      fetchOrders();
    },[]);

    const showPhones = (e) => {
      console.log(e.target);
      let id;
      if (e.target.classList.contains('MuiButton-label')){
        id = e.target.parentNode.value;
      }
      else if (e.target.classList.contains('btn-show')){
        id = e.target.value   
      }
      setOrderId(id);
      history.push('/phonesInOrder/'+id);

    }

    const handlePopper = (btn, e) => {
      if (btn === "close"){
        setIsPopperOpen((state) => !state);
        setAnchorEl(anchorEl ? null : e.target);
      }
      else if (btn === "pay"){
        setIsPopperOpenPay((state) => !state);
        setAnchorElPay(anchorElPay ? null : e.target);
      }

      let id;
      if (e.target.classList.contains('MuiButton-label')){
        id = e.target.parentNode.value;
      }
      else if (e.target.classList.contains('btn-close') || e.target.classList.contains('btn-pay')){
        id = e.target.value;
      }
      setOrderId(id);
    }

    const dontCloseOrder = (e) => {
      setIsPopperOpen(false);
      setAnchorEl(null);
    }

    const closeOrder = (e) => {
      fetch('api/ChangeOrderStatus?orderId='+orderId+"&newStatus=Closed",{
        method: 'POST'
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

    const dontPayOrder = (e) => {
      setIsPopperOpenPay(false);
      setAnchorElPay(null);
    }

    const payOrder = (e) => {
      fetch('api/ChangeOrderStatus?orderId='+orderId+"&newStatus=Paid",{
        method: 'POST'
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
            {orders && orders.map((order) => (
              <TableRow
                key={order.orderId}
              >
                <TableCell component="th" scope="row">{order.orderId}</TableCell>
                <TableCell>{new Date(order.createdDate).toLocaleString()}</TableCell>
                <TableCell>{new Date(order.modifiedDate).toLocaleString()}</TableCell>
                <TableCell>{orderStatus[order.orderStatusId]}</TableCell>
                <TableCell><Button className="btn-show" value={order.orderId} onClick={showPhones} variant="outlined" color="primary">Show</Button></TableCell>
                {orderStatus[order.orderStatusId] == "Open" ?
                <TableCell>
                  <Button className="btn-close" aria-describedby="close" value={order.orderId} onClick={(e) => handlePopper("close",e)} variant="outlined" color="secondary">Close</Button>
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
                 orderStatus[order.orderStatusId] == "Closed" ? 
                 <TableCell>
                   <Button className="btn-pay" value={order.orderId} onClick={(e) => handlePopper("pay",e)} variant="outlined" color="secondary">Pay</Button>
                   <Popper  id="close" open={isPopperOpenPay} anchorEl={anchorElPay} >
                     <Paper className={classes.popperPaper}>
                       <div>
                          Are you sure to pay for this order?
                        </div>
                        <div className={classes.buttonGroup}>
                          <Button className={classes.closeYes} onClick={payOrder}>Yes</Button>
                          <Button className={classes.closeNo} onClick={dontPayOrder}>No</Button>
                        </div>
                    </Paper>
                  </Popper>
                 </TableCell>
                :
                orderStatus[order.orderStatusId] == "Paid" ? 
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
