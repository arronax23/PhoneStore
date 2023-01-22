const tokenReducer = (state = "",action) => {
    switch (action.type) {
        case 'INSERT_TOKEN':
            state = action.payload;
            return state;
            break;    
        case 'RESET_TOKEN':
            state = '';
            return state;
            break;                  
        default:
            return state;
            break;
    }
}
export default tokenReducer;