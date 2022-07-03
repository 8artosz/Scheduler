import React, { useState } from "react";
import { Operation } from "./Operation";
import ".././style.css";

export const Operations = ({
  form,
  setForm,
  operations,
  onAddOperation,
  onDeleteExerciseOperation,
  onModifyExerciseOperation,
  status,
}) => {
  const [input, setInput] = useState("");

  const handleOnAdd = (e) => {
    e.preventDefault();
    if (input.length > 4) {
      onAddOperation({ description: input, timeSpent: 0 });
      setForm(!form);
      setInput("");
    }
  };

  return (
    <>
      {form ? (
        <div className="card-main">
          <form className="card-header">
            <div>
              <input
                style={{ width: "auto" }}
                type="text"
                value={input}
                onChange={(e) => setInput(e.target.value)}
                className="form-control"
                placeholder="Operation description"
              />
            </div>
            <div>
              <button onClick={(e) => handleOnAdd(e)}>Add</button>
            </div>
          </form>
        </div>
      ) : null}

      <ul className="ul">
        {operations.map((element, index) => (
          <Operation
            status={status}
            key={index}
            operation={element}
            onDeleteExerciseOperation={onDeleteExerciseOperation}
            onModifyExerciseOperation={onModifyExerciseOperation}
          />
        ))}
      </ul>
    </>
  );
};
