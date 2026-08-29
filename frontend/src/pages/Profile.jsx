import {useEffect, usestate} from "react";
import { useNavigate} from "react-router-dom";
import api from "../services/api";

function Profile(){

    const [user, setUser]=useState(null);
    const [name, setName]=useState("");

    const navigate= useNavigate();

    useEffect(()=>{
        fetchProfile();
    },[]);

    const fetchProfile = async()=>{
        try{
            const response = await api.get("/Users/profile");

            setUser(response.data);
            setName(response.data.name);
        }
        catch(error){
            console.error(error);
        }
    };

    const handleSubmit = async(e)=>{
        e.preventDefault();
        try{
            const response = await api.put("/Users/profile",{
                name:name
            });

            alert(response.data.message);

            setUser({
                ...user,
                name: response.data.name
            });
        }
        catch (error){
            console.error(error);
        }
    };
return (
    <div className="profile-page">

        <div className="profile-card">

            <button
            className="back-button"
            onClick={()=>navigate("/products")}
            >
                ← Back to Products
            </button>

            <h1>My Profile</h1>

            <p className="profile-subtitle">
                Manage your account information
            </p>

            {user &&(
                <form onSubmit={handleSubmit}>
                    <label>Name</label>

                    <input
                        type="text"
                        value={name}
                        onChange={(e)=> setName(e.target.value)}
                    />

                    <label>Email</label>

                    <input
                        type="email"
                        value={user.email}
                        disabled
                    />

                    <button
                        className="save-profile-button"
                        type="submit"
                    >
                        Save Changes
                    </button>
                </form>
            )}
        </div>
    </div>
);
}

export default Profile;