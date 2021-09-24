const usernameReducer = (state = "",action) => {
    switch (action.type) {
        case 'SELECT_USERNAME':
            state = action.payload;
            return state;
            break;    
        case 'FORGET_USERNAME':
            state = '';
            return state;
            break;                  
        default:
            return state;
            break;
    }
}

export default usernameReducer;