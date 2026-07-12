import { useEffect, useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

function Products() {
        const [products, setProducts] = useState([]);

        useEffect(() => {
                fetchProducts();
        }, []);

        const fetchProducts = async () => {
                try {
                        const response = await api.get("/Products");
                        setProducts(response.data);
                }

                catch (error) {
                        console.error(error);
                }
        };

        const navigate = useNavigate();

        const handleLogout = () => {
                localStorage.removeItem('token');
                navigate("/");
        }

        console.log(products.imageUrl);
        return (

               <div className="products-page">

        <div className="navbar">

            <h1>ProductHub</h1>

            <div className="nav-buttons">

                <button onClick={() => navigate("/create-product")}>
                    Add Product
                </button>

                <button onClick={handleLogout}>
                    Logout
                </button>

            </div>

        </div>

        <div className="products-container">

            {products.map(product => (

                <div className="product-card" key={product.id}>

                    <img
                        src={`${import.meta.env.VITE_BASE_URL || "http://localhost:5067"}${product.imageUrl}`}
                        alt={product.name}
                        onError={(e) => {
                                e.target.src="https://placehold.co/400x250?text=No+Image";
                        }}
                    />

                    <b><h2>{product.name}</h2></b>

                    <p>{product.description}</p>

                    <h3>Rs:{product.price}</h3>

                    <p>Uploaded by: <b>{product.uploadedBy}</b></p>
                    <br/>

                </div>

            ))}

        </div>

    </div>

        );
}

export default Products;

/*
User clicks Login
        ↓
handleLogin()
        ↓
navigate("/products")
        ↓
URL becomes /products
        ↓
Route matches
        ↓
<Products />
        ↓
React calls Products()
        ↓
useEffect()
        ↓
fetchProducts()
        ↓
GET /api/Products
        ↓
setProducts()
        ↓
UI appears
*/