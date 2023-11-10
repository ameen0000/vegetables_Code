import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./service.css";

const Service = () => {
  const [username, setusername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleIdInput = (e) => {
    setusername(e.target.value);
  };

  const handlePwInput = (e) => {
    setPassword(e.target.value);
  };

  const enableLoginButton = () => {
    return !username || !password;
  };

  const changeLoginButtonStyle = () => {
    return enableLoginButton() ? "disabled" : "";
  };

  const clickLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7190/api/Auth/verify",
        {
          username: username,
          password: password,
        }
      );

      const result = response.data;
      console.log("response:", response);

      if (response.data || response.data.status === true) {
        alert("You are logged in.");
        console.log(response);

        navigate("/dashboard");
      } else {
        alert("Please check your login information.");
      }
    } catch (error) {
      console.error(error);
      setError("An error occurred during login. Please try again.");
    }
  };

  return (
    <div className="container-fluid-fluid w-100 parentcontainer">
      <div className="row justify-content-center">
        <div className="col-md-6 transprentbg mt-5 p-5">
          <form className="mt-5">
            <div className="mb-3">
              <h1 className="login justify-content-center d-flex mb-3 ">
                LOGIN
              </h1>
              <label htmlFor="username" className="form-label fond">
                <b>UserName :</b>
              </label>
              <input
                type="text"
                className="form-control back  border-0"
                onChange={handleIdInput}
                id="username"
                placeholder="username"
              />
            </div>
            <div className="mb-3">
              <label htmlFor="password" className="form-label fond">
                <b>Password :</b>
              </label>
              <input
                type="password"
                className="form-control   border-0"
                onChange={handlePwInput}
                id="password"
                placeholder="Password"
              />
            </div>
            <button
              className={`btn btn-primary ${changeLoginButtonStyle()}`}
              disabled={enableLoginButton()}
              onClick={clickLogin}
              type="submit"
            >
              Login
            </button>
            {error && <p className="text-danger mt-3">{error}</p>}
          </form>
        </div>
      </div>
    </div>
  );
};

export default Service;
