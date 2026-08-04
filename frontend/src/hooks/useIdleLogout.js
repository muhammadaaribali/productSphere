import {useEffect} from "react";

function useIdleLogout(){
    useEffect(()=>{

        let timeoutId;

        const logout =()=>{

            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");

            window.location.href="/";

        };

        const resetTimer=()=>{

            clearTimeout(timeoutId);
            timeoutId=setTimeout(logout,60*60*1000); // 1 hour in milliseconds
        };

        const events=["mousemove","keydown","mousedown","touchstart"];

        resetTimer();

        events.forEach((event)=>{
            window.addEventListener(event,resetTimer);
        });

        return ()=>{
            clearTimeout(timeoutId);
            events.forEach((event)=>{
                window.removeEventListener(event,resetTimer);
            });
        };

    },[]);
}

export default useIdleLogout;