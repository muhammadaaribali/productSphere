import { useState } from "react";
import api from "../services/api";

function CreateProduct(){

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [imageUrl, setImageUrl] = useState("");

    const handleSubmit = async() =>{
        try{
            await api.post("/Products", {
                name,
                description,
                price,
                imageUrl,
                userId: 1
            });

            alert("Product created successfully!");
        }

        catch(error){
            console.error(error);
            alert("Failed to create product. Please try again.");
        }
    };

    return (
        <div>
            <h1>Create Product</h1>

        <input
            type="text"
            placeholder="Product Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
        />

        <br /><br />
        <input
            type="text"
            placeholder="Product Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
        />

        <br /><br />

        <input
            type="number"
            placeholder="Product Price"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
        />

        <br /><br />

        <input
            type="text"
            placeholder="Product Image URL"
            value={imageUrl}
            onChange={(e) => setImageUrl(e.target.value)}
        />

        <br /><br />

        <button onClick={handleSubmit}>Create Product</button>
    </div>

    );
}

export default CreateProduct;