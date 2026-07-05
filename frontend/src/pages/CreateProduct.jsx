import { useState } from "react";
import api from "../services/api";

function CreateProduct(){

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [imageUrl, setImageUrl] = useState("");

    const handleSubmit = async() =>{
        try{

            const token = localStorage.getItem("token");
            await api.post("/Products", {
                name,
                description,
                price,
                imageUrl,
            },
            //these details goes to dto
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
                //we are sending the token in the Authorization header of the request, so that the backend can verify that the user is authenticated and authorized to create a product
            }
            
        );

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