import { useEffect, useState } from "react";
import api from "../services/api";

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

    return (
        <div>
            <h1>Products</h1>
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