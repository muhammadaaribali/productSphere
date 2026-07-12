import { useState } from "react";
import { useNavigate } from "react-router-dom"
import api from "../services/api";

function CreateProduct(){

    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [image, setImage] = useState(null);

    const handleSubmit = async() =>{
        try{

            const token = localStorage.getItem("token");

            const formData = new FormData();

            formData.append("name",name);
            formData.append("description",description);
            formData.append("price",price);
            formData.append("image",image);

            await api.post("/Products", formData,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
                //we are sending the token in the Authorization header of the request, so that the backend can verify that the user is authenticated and authorized to create a product
            }
            
        );
            alert("Product created successfully!");
            navigate("/products");
        }

        catch(error){
            console.error(error);
            alert("Failed to create product. Please try again.");
        }
    };

    return (
        <div className="auth-container">

        <div className="auth-card">

            <h1>ProductHub</h1>

            <h2>Create Product</h2>

            <input
                type="text"
                placeholder="Product Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />

            <input
                type="text"
                placeholder="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />

            <input
                type="number"
                placeholder="Price"
                value={price}
                onChange={(e) => setPrice(e.target.value)}
            />

            <input
                type="file"
                accept="image/*"
                onChange={(e) => setImage(e.target.files[0])}
            />

            <button onClick={handleSubmit}>
                Upload Product
            </button>

        </div>

    </div>

    );
}

export default CreateProduct;