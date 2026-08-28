import { useEffect, useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

function Products() {
        const [products, setProducts] = useState([]);
        const [user, setUser]= useState(null);
        const [showMenu, setShowMenu]=useState(false);
        const navigate= useNavigate();

        useEffect(() => {
                fetchProducts();
        }, []);

        const fetchProducts = async () => {
                try {
                        const response = await api.get("/Products");
                        setProducts(response.data);

                        const profileResponse = await api.get("/Users/profile");
                        setUser(profileResponse.data);
                }

                catch (error) {
                        console.error(error);
                }
        };

        const handleLogout = () => {
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                navigate("/");
        }

        return (

        <div className="products-page">

        <div className="navbar">

            <h1>ProductHub</h1>

            <div className="nav-buttons">

                <button onClick={() => navigate("/create-product")}>
                    Add Product
                </button>

                <div className="user-menu">

                        <button 
                        className="user-button"
                        onClick={()=> setShowMenu(!showMenu)}
                        >
                          <span className="user-icon">
                                👤
                          </span>

                          <span>
                                {user ? user.name: "User"}
                          </span>

                          <span>
                                ▾
                          </span>
                        </button>

                        {showMenu &&(
                                <div className="dropdown-menu">
                                        <div className="dropdown-user">
                                                <Strong>
                                                        {user?.name}
                                                </Strong>
                                                <span>
                                                        {user?.email}
                                                </span>
                                        </div>

                                        <button onClick={()=> navigate("/profile")}
                                        >
                                                Profile
                                        </button>
                                        <button>
                                                Settings
                                        </button>
                                        <button>
                                                Change Password
                                        </button>

                                        <div className="dropdown-divider"></div>

                                        <button 
                                        className="logout-button"
                                        onClick={handleLogout}
                                        >
                                         Logout
                                        </button>

                                </div>
                        )}
                </div>

            </div>

        </div>

        <div className="products-header">

                <div>
                        <h1>Products</h1>

                        <p>
                                Manage all your products
                        </p>
                </div>
        </div>

        <div className="products-stats">

                <div className="stat-card">

                        <h3>Total Products</h3>
                        <h2>{products.length}</h2>
                </div>
        </div>

        <div className="products-container">

            {products.map(product => (

                <div className="product-card" key={product.id}>

                    <img
                        src={product.imageUrl}
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