import React, { useState, useEffect } from 'react'
const useFetchPost = (url, phone) => {
    const [error,setError] = useState();
    useEffect(() => {
        const abortController = new AbortController();
        fetch(url, 
            {
                method: 'POST',
                signal: abortController.signal,
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(phone)  
            })
        .then(response => {
            if (!response.ok){
                console.log('Error');
                throw new Error("Something went wrong!");
            }
        })
        .catch(err => setError(err.message));
        return () => abortController.abort();
      }, [url])
      return {error};
}

export default useFetchPost
