import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

function AddUser() {
    const navigate = useNavigate();

    const [user, setUser] = useState({
        name: "",
        email: "",
        phone: ""
    });

    const handleChange = (e) => {
        setUser({
            ...user,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await api.post("/User", user);

            alert("User Added Successfully");

            navigate("/dashboard");
        }
        catch (error) {
            console.error(error);
            alert("Failed To Add User");
        }
    };

    return (
        <div className="form-container">
            <h2>Add User</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    placeholder="Name"
                    value={user.name}
                    onChange={handleChange}
                    required
                />

                <input
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={user.email}
                    onChange={handleChange}
                    required
                />

                <input
                    type="text"
                    name="phone"
                    placeholder="Phone"
                    value={user.phone}
                    onChange={handleChange}
                    required
                />

                <button type="submit">
                    Add User
                </button>
            </form>
        </div>
    );
}

export default AddUser;