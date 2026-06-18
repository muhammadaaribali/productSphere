import { useState } from "react";
import api from "../services/api";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const response = await api.post("/Auth/login", {
        email,
        password
      });

      console.log(response.data);

      alert("Login successful");
    }
    catch (error) {
      console.error(error);

      alert("Login failed");
    }
  };

  return (
    <div>
      <h1>Login</h1>

      <input
        type="email"
        placeholder="Email"
        value={email} //initially email = ""
        onChange={(e) => setEmail(e.target.value)}
      />

      <br/><br/>

      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <br /><br />

      <button onClick={handleLogin}>
        Login
      </button>
    </div>
  );
}

export default Login;

//UserState 
/*
Step 1: Page loads
email = ""

Input is empty.

Step 2: User types "a"
setEmail("a")

React updates state:

email = "a"

UI updates automatically.
 */