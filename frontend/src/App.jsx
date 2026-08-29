import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Products from "./pages/Products";
import CreateProduct from "./pages/CreateProduct";
import Register from "./pages/Register";
import ProtectedRoute from "./ProtectedRoute";
import useIdleLogout from "./hooks/useIdleLogout";
import Profile from "./pages/Profile";

function App() {
  
  useIdleLogout();
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/products" element={
          <ProtectedRoute>
            <Products />
          </ProtectedRoute>
        } 
        />
        <Route path="/create-product" element={
          <ProtectedRoute>
            <CreateProduct />
          </ProtectedRoute>
        } />
        <Route path="/profile" element={
          <ProtectedRoute>
            <Profile/>
          </ProtectedRoute>}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

//BrowserRouter tells my applicaiton will have multile pages
//Routes is a container for all the routes in my application
//Route is a single route in my application