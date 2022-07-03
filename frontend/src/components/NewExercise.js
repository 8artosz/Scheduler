import React, { useState } from "react";
import ".././style.css";

export const NewExercise = ({ addExercise }) => {
  const [data, setData] = useState({
    Title: "",
    Description: "",
  });

  const handleOnClick = (event) => {
    event.preventDefault();
    if (data.Title !== "") {
      addExercise({
        Description: data.Description,
        Status: "open",
        Title: data.Title,
      });
      setData({ Title: "", Description: "" });
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  return (
    <>
      <div className="card">
        <div className="card-body">
          <h1 style={{ marginBottom: "0.75rem" }}>New exercise</h1>
          <form>
            <div className="in">
              <input
                type="text"
                name="Title"
                value={data.Title}
                placeholder="Title"
                onChange={handleChange}
              />
            </div>
            <div className="in">
              <input
                type="text"
                name="Description"
                value={data.Description}
                placeholder="Description"
                onChange={handleChange}
              />
            </div>
            <div>
              <button
                onClick={(event) => handleOnClick(event)}
                style={{ backgroundColor: "cornflowerblue" }}
              >
                Add exercise
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};
