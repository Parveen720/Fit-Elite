import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

function Register() {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    name: "",
    email: "",
    phone: "",
    password: "",
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await api.post("/auth/register", form);

      alert("Registration Successful");

      navigate("/");
    } catch (error) {
      if (error.response?.status === 400) {
       alert("Email already exists");
      } else {
       alert("Registration Failed");
      }
}
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h2>Create Account</h2>

        <form onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Name"
            className="auth-input"
            onChange={(e) =>
              setForm({ ...form, name: e.target.value })
            }
          />

          <input
            type="email"
            placeholder="Email"
            className="auth-input"
            onChange={(e) =>
              setForm({ ...form, email: e.target.value })
            }
          />

          <input
            type="text"
            placeholder="Phone"
            className="auth-input"
            onChange={(e) =>
              setForm({ ...form, phone: e.target.value })
            }
          />

          <input
            type="password"
            placeholder="Password"
            className="auth-input"
            onChange={(e) =>
              setForm({ ...form, password: e.target.value })
            }
          />

          <button type="submit" className="auth-btn">
            Register
          </button>
        </form>

        <p className="switch-text">
          Already have an account?
        </p>

        <button
          className="secondary-btn"
          onClick={() => navigate("/")}
        >
          Login
        </button>
      </div>
    </div>
  );
}

export default Register;