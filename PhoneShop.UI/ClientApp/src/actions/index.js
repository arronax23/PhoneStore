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

export const logout = () => {
    return {
        type: 'LOGOUT'
    }
}

export const selectUsername = (payload) => {
    return {
        type: 'SELECT_USERNAME',
        payload: payload
    }
}

export const forgetUsername = () => {
    return {
        type: 'FORGET_USERNAME'
    }
}

export const insertToken = (payload) => {
    return {
        type: 'INSERT_TOKEN',
        payload: payload
    }
}

export const resetToken = () => {
    return {
        type: 'RESET_TOKEN',
    }
}
