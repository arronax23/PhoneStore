const loggingReducer = (state = "NOT_LOGGED",action) => {
    switch (action.type) {
        case 'LOG_AS_ADMIN':
            state = "LOGGED_AS_ADMIN"
            return state;
            break;
        case 'LOG_AS_CUSTOMER':
            state = "LOGGED_AS_CUSTOMER"
            return state;
            break;       
        case 'LOGOUT':
            state = "NOT_LOGGED"
            return state;
            break;              
        default:
            return state;
            break;
    }
}

export default loggingReducer;