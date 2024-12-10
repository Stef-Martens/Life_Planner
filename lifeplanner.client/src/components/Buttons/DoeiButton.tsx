import React from "react";

const DoeiButton = () => {
  const handleClose = () => {
    window.opener = null;
    window.open("", "_self");
    window.close();
    console.log("Doei!");
  };

  return (
    <button className="btn btn-primary" onClick={handleClose}>
      nvm doooeeiii
    </button>
  );
};

export default DoeiButton;
