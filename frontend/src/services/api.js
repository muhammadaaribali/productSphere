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

ap1.interceptors.response.use(

  (response) => {
    return response;
  },

  async (error) => {

    const originalRequest = error.config;
    //error.config is the configuration of the request that caused the error. It contains information such as the URL, method, headers, and data of the request.

    if( error.response?.status === 401 && !originalRequest._retry) {

      originalRequest._retry = true;

      const refreshToken= localStorage.getItem("refreshToken");

      const response = await api.post("/Auth/refresh", {
        refreshToken
      });

      localStorage.setItem("accessToken",response.data.accessToken);

      localStorage.setItem("refreshToken",response.data.refreshToken);

      originalRequest.headers.Authorization =
      `Bearer ${response.data.accessToken}`;

      return api(originalRequest);
    }

    return Promise.reject(error);
  }
);

export default api;


//internaly entire object is passed as error
// {
//     response: {
//         status: 401,
//         data: {
//             message: "Unauthorized"
//         }
//     },

//     config: {
//         method: "GET",
//         url: "/products",
//         headers: {
//             Authorization: "Bearer expiredToken"
//         }
//     }
// }


// Notice that request.use() accepts two functions:

// request.use(
//     successFunction,
//     errorFunction
// )

//this is config
// {
//     method: "GET",
//     url: "/products",
//     headers: {
//         Authorization: "Bearer abc123"
//     }
// }

//we create an instance of axios with a base URL, so we can easily make API calls to our backend without having to specify the full URL every time. 
//"My backend is at http://localhost:5067/api"

/*So later you can write:

api.get("/products")
api.post("/login")
api.delete("/products/1")
 */