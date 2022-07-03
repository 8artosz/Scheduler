import React, { useEffect, useState } from "react";
import { Operations } from "./Operations";
import { getExerciseOperations } from "../api/exerciseClient";
import { addExerciseOperation } from "../api/exerciseClient";
import { deleteOperation } from "../api/operationClient";
import { modifyOperation } from "../api/operationClient";
import ".././style.css";

export const Exercise = ({ exercise, onDelete, onModify }) => {
  const { title, description, status, idExercise } = exercise;
  const [form, setForm] = useState(false);

  const [operations, setOperations] = useState([]);

  useEffect(() => {
    getExerciseOperations(idExercise)
      .then((response) => setOperations(response.data))
      .catch((error) => console.log(error));
  }, [idExercise]);

  const onAddExerciseOperation = (operation) => {
    addExerciseOperation(idExercise, operation)
      .then(() =>
        getExerciseOperations(idExercise).then((response) => setOperations(response.data))
      )
      .catch((error) => console.log(error));
  };

  const onModifyExerciseOperation = (idOperation, operation) => {
    modifyOperation(idOperation, operation).then(() =>
      getExerciseOperations(idExercise)
        .then((response) => setOperations(response.data))
        .catch((error) => console.log(error))
    );
  };

  const onDeleteExerciseOperation = (idOperation) => {
    deleteOperation(idOperation)
      .then(() =>
        getExerciseOperations(idExercise).then((response) => {
          setOperations(response.data);
          console.log(response.data);
        })
      )
      .catch((error) => console.log(error));
  };

  const handleOnDelete = (e) => {
    e.preventDefault();
    onDelete(idExercise);
  };

  const handleModifyExercise = (e) => {
    e.preventDefault();

    onModify(idExercise, {
      Title: title,
      Description: description,
      Status: "closed"
    });
  };

  return (
    <>
      <div className="card-task">
        <div className="card-header">
          <div>
            <h4 style={{ marginTop: "0", marginBottom: "0.25rem" }}>{title}</h4>
            <h6 style={{ marginTop: "0", marginBottom: "0.25rem" }}>
              {description}
            </h6>
          </div>

          <div>
            {status === "open" ? (
              <button className="button" onClick={() => setForm(!form)}>
                Add operation
              </button>
            ) : null}

            {status === "open" ? (
              <button className="button" onClick={(e) => handleModifyExercise(e)}>
                Finish
              </button>
            ) : null}

            {operations.length < 1 ? (
              <button className="button" onClick={(e) => handleOnDelete(e)}>
                Delete
              </button>
            ) : null}
          </div>
        </div>
      </div>
      <Operations
        form={form}
        status={status}
        setForm={setForm}
        operations={operations}
        onAddOperation={onAddExerciseOperation}
        onDeleteExerciseOperation={onDeleteExerciseOperation}
        onModifyExerciseOperation={onModifyExerciseOperation}
      />
    </>
  );
};
