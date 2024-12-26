import React from "react";
import { useNavigate } from "react-router-dom";
import { ReactTyped } from "react-typed";
import BaseScreen from "./BaseScreenQuestions";

const FinancialGoalsQuestions: React.FC = () => {
    const navigate = useNavigate();

    return (
        <BaseScreen
            onBack={() => navigate("/question1")} 
            onContinue={() => navigate("/question3")} // Replace with the appropriate next page
        >
            <div className="h-full flex items-center justify-center">
                <h1 className="text-4xl font-bold text-center">
                    <ReactTyped
                        strings={[
                            "hihi haha",
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

export default FinancialGoalsQuestions;
