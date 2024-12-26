import React from "react";
import BaseScreen from "./BaseScreenQuestions";

const FinancialGoalsQuestions: React.FC = () => {

    return (
        <BaseScreen>
            <div className="h-full flex-col space-between flex items-center justify-center ">
                <h1 className="text-4xl font-bold content-center basis-1/4">
                    Financial Goals Questions
                </h1>
                <div className="flex basis-3/4 items-center">
                    <p>Question 1</p>
                    <p>What is your financial goal?</p>
                    <input type="text"/>
                </div>
            </div>
        </BaseScreen>
    );
};

export default FinancialGoalsQuestions;
