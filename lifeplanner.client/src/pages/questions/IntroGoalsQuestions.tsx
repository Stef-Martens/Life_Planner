import React from "react";
import { useNavigate } from "react-router-dom";
import { ReactTyped } from "react-typed";
import BaseScreen from "./BaseScreenQuestions";

const IntroGoalQuestions: React.FC = () => {
  const navigate = useNavigate();

  return (
    <BaseScreen
      onBack={() => alert("HAHAHAHAH GAAT NI LOSER")}
      onContinue={() => navigate("/question2")} // Replace with the appropriate next page
    >
      <div className="h-full flex items-center justify-center">
        <h1 className="text-4xl font-bold text-center">
          <ReactTyped
            strings={[
              "Are you ready to set your goals for the year?",
              "Let's get started!",
              ":)",
            ]}
            typeSpeed={40}
            backSpeed={20}
            loop
          />
        </h1>
      </div>
    </BaseScreen>
  );
};

export default IntroGoalQuestions;
