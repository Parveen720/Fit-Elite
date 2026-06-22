import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:7125/api"
});

export default api;