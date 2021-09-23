export const logAsAdmin = () => {
    return {
        type: 'LOG_AS_ADMIN'
    }
}

export const logAsCustomer = () => {
    return {
        type: 'LOG_AS_CUSTOMER'
    }
}

export const selectUsername = (payload) => {
    return {
        type: 'SELECT_USERNAME',
        payload: payload
    }
}