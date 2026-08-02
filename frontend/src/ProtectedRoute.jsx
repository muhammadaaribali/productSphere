import {Navigate} from "react-router-dom";

function ProtectedRoute({ children }) {

    const accessToken = localStorage.getItem("accessToken");

    if (!accessToken) {
        return <Navigate to="/" replace />;
        // replace prop is used to replace the current entry in the history stack with the new one, so that the user cannot go back to the protected route after being redirected to the login page.
    }

    return children;
}

export default ProtectedRoute;