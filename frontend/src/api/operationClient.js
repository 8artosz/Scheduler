import { API_KEY, API_URL } from "./constants";
import axios from "axios";
axios.defaults.headers.common = { Authorization: `${API_KEY}` };

export const modifyOperation = async (id,operation) => {
    const response = await axios.put(`${API_URL}/operations/${id}`, operation);
    return response;
  };

  export const deleteOperation = async (id) => {
    const response = axios.delete(`${API_URL}/operations/${id}`);
    return await response;
  };