import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5067/api",
  headers: {
    "ngrok-skip-browser-warning": "true"
  }
});

api.interceptors.request.use(
  (config) => {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken){
      config.headers.Authorization= `Bearer ${accessToken}`;
    }
    return config;
  },
    (error) => {
      return Promise.reject(error);
    }
);

export default api;

//we create an instance of axios with a base URL, so we can easily make API calls to our backend without having to specify the full URL every time. 
//"My backend is at http://localhost:5067/api"

/*So later you can write:

api.get("/products")
api.post("/login")
api.delete("/products/1")
 */