import { API_KEY, API_URL } from "./constants";
import axios from "axios";
axios.defaults.headers.common = { Authorization: `${API_KEY}` };

export const getExercises = async () => {
  const response = await axios.get(`${API_URL}/exercises`);
  return response;
};

export const addExercise = async (exercise) => {
  const response = axios.post(`${API_URL}/exercise`, exercise);
  return response;
};

export const modifyExercise = async (id, exercise) => {
  const response = await axios.put(`${API_URL}/exercise/${id}`, exercise);
  return response;
};

export const deleteExercise = async (id) => {
  const response = axios.delete(`${API_URL}/exercise/${id}`);
  return await response;
};

export const getExerciseOperations = async (id) => {
  const response = await axios.get(`${API_URL}/exercise/${id}/operations`);
  return response;
};

export const addExerciseOperation = async (id, operation) => {
  const response = axios.post(
    `${API_URL}/exercise/${id}/operations`,
    operation
  );
  return response;
};
