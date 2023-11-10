import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./dashboard.css";

const Dashboard = () => {
  const [sessionToken, setSessionToken] = useState("");
  const [vegetablesData, setVegetablesData] = useState([]);
  const [insertValues, setInsertValues] = useState({});
  const [userId, setUserId] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("https://localhost:7190/api/Veg")
      .then((res) => setVegetablesData(res.data.value))
      .catch((err) => console.log(err));
  }, []);

  const generateSessionToken = () => {
    const characters =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    const token = Array.from(
      { length: 10 },
      () => characters[Math.floor(Math.random() * characters.length)]
    ).join("");
    return token;
  };

  useEffect(() => {
    const newToken = generateSessionToken();
    setSessionToken(newToken);
  }, []);

  const handleInsertChange = (vegetableId, value) => {
    const newInsertValues = { ...insertValues, [vegetableId]: value };
    setInsertValues(newInsertValues);
    // Log the entered values to the console
    console.log(`Vegetable ID: ${vegetableId}, Price: ${value}`);
  };

  const mapping = () => {
    navigate("/map");
  };

  const submitbutton = async () => {
    try {
      // Assuming you want to make an API call for each vegetable
      for (const vegetableId in insertValues) {
        const inputValue = document.getElementById(
          `input_${vegetableId}`
        ).value;
        const responded = await axios.post("https://localhost:7190/api/Price", {
          vegetableId,
          price1: inputValue,
          // Assuming inputValue contains the price
        });
        console.log(inputValue);
        // Handle the API response as needed
        console.log(
          `API response for Vegetable ID ${vegetableId}:`,
          responded.data
        );
      }
    } catch (error) {
      console.error("Error adding prices:", error);

      if (error.response) {
        console.error("Server responded with status:", error.response.status);
      }
    }
  };

  return (
    <div className="parent">
      <header className="header w-100">
        <h3 className="vegetable_heading">Vegetables</h3>
      </header>
      <table className="table table-striped table-dark">
        <thead>
          <tr>
            <th>Vegetables</th>
            <th>Prices</th>
          </tr>
        </thead>
        <tbody>
          {vegetablesData.map((vegetable) => (
            <tr key={vegetable.id}>
              <td>{vegetable.vegetableName}</td>
              <td>
                <input
                  className="veginp"
                  type="number"
                  value={insertValues[vegetable.id] || ""}
                  onChange={(e) =>
                    handleInsertChange(vegetable.id, e.target.value)
                  }
                  id={`input_${vegetable.id}`}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div>
        <p className="cookie">
          The user cookie for this session is: {sessionToken}
        </p>
        <div className="buttons d-flex gap-5">
          <button
            type="button"
            className="btn btn-outline-light ms-5"
            onClick={mapping}
          >
            Go to Map
          </button>
          <button
            type="button"
            className="btn btn-primary"
            onClick={submitbutton}
          >
            Submit
          </button>
        </div>
      </div>
      <footer className="footer w-100 mt-3">
        <p className="copy">©️Copyright 2022</p>
      </footer>
    </div>
  );
};

export default Dashboard;
