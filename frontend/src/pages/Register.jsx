import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

function Register() {

    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [companyId, setCompanyId] = useState("");

    const navigate = useNavigate();

    const handleRegister = async () => {

        try {
            await api.post("/Auth/register", {
                name,
                email,
                password,
                companyId
            });

            alert("User registered successfully!");
            navigate("/");
        }
        catch (error) {

            console.error(error);
            alert("Error registering user. Please try again.");
        }
    };

   return (
    <div className="auth-container">

        <div className="auth-card">

            <h1>ProductHub</h1>

            <h2>Create Account</h2>

            <input
                type="text"
                placeholder="Full Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />

            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />

            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <input
                type="number"
                placeholder="Company ID"
                value={companyId}
                onChange={(e) => setCompanyId(e.target.value)}
            />

            <button onClick={handleRegister}>
                Register
            </button>

            <p>
                Already have an account?
                <span onClick={() => navigate("/")}>
                    Login
                </span>
            </p>

        </div>

    </div>
);
    
}

export default Register;