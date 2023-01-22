import { useState, useEffect } from 'react'
import { useSelector } from 'react-redux'

const useFetchGet = (url) => {
    const [data, setData] = useState();
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState();
    const [httpResposne, setHttpResposne] = useState();
    const token = useSelector(state => state.token);
    useEffect(() => {
        const abortController = new AbortController();
        fetch(url, {
          signal: abortController.signal,
          headers: {
            "Authorization": "bearer "+token
          }
        })
        .then(response => {
          setHttpResposne(response.status);
          console.log(response);
          return response.json();
        })
        .then(responseData => {
            console.log(responseData);
            setData(responseData);
            setIsPending(false);
        })
        .catch(err => setError(err.message));
        return () => abortController.abort();
      }, [url])

      return {data, isPending, error, httpResposne};
}

export default useFetchGet;