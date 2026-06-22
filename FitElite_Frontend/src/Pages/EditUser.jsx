import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../services/api";

function EditUser() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [user, setUser] = useState({
        name: "",
        email: "",
        phone: ""
    });

    useEffect(() => {
        loadUser();
    }, []);

    const loadUser = async () => {
        try {
            const response =
                await api.get(`/User/${id}`);

            setUser(response.data);
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleChange = (e) => {
        setUser({
            ...user,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await api.put(`/User/${id}`, user);

            alert("User Updated Successfully");

            navigate("/dashboard");
        }
        catch (error) {
            console.error(error);
            alert("Update Failed");
        }
    };

    return (
        <div className="form-container">
            <h2>Edit User</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    value={user.name}
                    onChange={handleChange}
                    required
                />

                <input
                    type="email"
                    name="email"
                    value={user.email}
                    onChange={handleChange}
                    required
                />

                <input
                    type="text"
                    name="phone"
                    value={user.phone}
                    onChange={handleChange}
                    required
                />

                <button type="submit">
                    Update User
                </button>
            </form>
        </div>
    );
}

export default EditUser;