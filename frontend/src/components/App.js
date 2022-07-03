import React, { useEffect, useState } from "react";
import { getExercises, modifyExercise } from "../api/exerciseClient";
import { addExercise } from "../api/exerciseClient";
import { NewExercise } from "./NewExercise";
import { Exercise } from "./Exercise";
import { deleteExercise } from "../api/exerciseClient";

export const App = () => {
  const [exercises, setExercises] = useState([]);

  useEffect(() => {
    getExercises()
      .then((data) => setExercises(data.data))
      .catch((error) => console.log(error));
  }, []);

  const onAddExercise = (exercise) => {
    addExercise(exercise)
      .then(() => {
        getExercises().then((data) => setExercises(data.data));
      })
      .catch((error) => console.log(error));
  };

  const onDeleteExercise = (id) => {
    deleteExercise(id)
      .then(() => getExercises().then((data) => setExercises(data.data)))
      .catch((error) => console.log(error));
  };

  const onModifyExercise = (id, exercise) => {
    modifyExercise(id, exercise)
      .then(() => getExercises().then((data) => setExercises(data.data)))
      .catch((error) => console.log(error));
  };
  return (
    <>
      <NewExercise addExercise={onAddExercise} />
      {exercises.map((element, index) => (
        <Exercise
          onModify={onModifyExercise}
          onDelete={onDeleteExercise}
          key={index}
          exercise={element}
        />
      ))}
    </>
  );
};
