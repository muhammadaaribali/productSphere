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

    return (
        <div>
            <h1>Products</h1>

            <button onClick={()=> navigate("/create-product")}>
            </button>

            <br/>
            <br/>
            
            {products.map(product => (
                <div key={product.id}>
                    <h3>{product.name}</h3>
                    <p>{product.description}    
                    </p>
                    <p>Price: {product.price}   </p>
                    <hr />
                </div>
            ))}
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