import React, { useState } from "react";
import ".././style.css";

export const Operation = ({
  operation,
  onDeleteExerciseOperation,
  onModifyExerciseOperation,
  status,
}) => {
  const [isAddingTime, setIsAddingTime] = useState(false);
  const { description, idOperation, timeSpent } = operation;
  const [time, setTime] = useState("");

  const handleOnDeleteExerciseOperation = (e) => {
    e.preventDefault();
    onDeleteExerciseOperation(idOperation);
  };

  const handleOnModifyExerciseOperation = (e) => {
    e.preventDefault();
    onModifyExerciseOperation(idOperation, {
      description: description,
      timeSpent: Number(time) + Number(timeSpent),
    });
    setIsAddingTime(!isAddingTime);
  };

  return (
    <li className="card-main">
      <div className="card-header">
        <div>
          <span style={{ marginRight: "0.5rem" }}>
            <b>{description}</b>
          </span>
          <span>{timeSpent}m</span>
        </div>

        {isAddingTime && (
          <div>
            <form>
              <div>
                <input
                  type="number"
                  className="in"
                  value={time}
                  onChange={(e) =>
                    setTime(e.target.value.replace(/[^0-9]/g, ""))
                  }
                  placeholder="Spent time in minutes"
                  style={{ width: "12rem" }}
                />
                <button
                  className="button"
                  onClick={(e) => handleOnModifyExerciseOperation(e)}
                >
                  Save
                </button>
                <button
                  className="button"
                  onClick={() => setIsAddingTime(!isAddingTime)}
                >
                  Return
                </button>
              </div>
            </form>
          </div>
        )}

        {!isAddingTime && (
          <div>
            {status === "open" ? (
              <button
                className="button"
                onClick={() => setIsAddingTime(!isAddingTime)}
              >
                Add time
              </button>
            ) : null}

            <button
              className="button"
              onClick={(e) => handleOnDeleteExerciseOperation(e)}
            >
              Delete
            </button>
          </div>
        )}
      </div>
    </li>
  );
};
