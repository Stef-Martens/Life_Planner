import React, {useState} from "react";
import BaseScreen from "./BaseScreenQuestions";
import Dropzone from "../../components/DnD/Dropzone.tsx";

const FinancialGoalsQuestions: React.FC = () => {
    const [myGoals, setMyGoals] = useState<any[]>([]);
    const [suggestions, setSuggestions] = useState<any[]>([
        {id: 1, title: "Card 1", description: "Card 1 description"},
        {id: 2, title: "Card 2", description: "Card 2 description"},
        {id: 3, title: "Card 3", description: "Card 3 description"},
        {id: 4, title: "Card 4", description: "Card 4 description"},
        {id: 5, title: "Card 5", description: "Card 5 description"},
        {id: 6, title: "Card 6", description: "Card 6 description"},
    ]);

    return (
        <BaseScreen>
            <div className="h-full flex-col space-between flex items-center justify-center ">
                <h1 className="text-4xl font-bold content-center basis-1/4">
                    Financial Goals Questions
                </h1>
                <div className="flex flex-col basis-3/4 items-center justify-center w-10/12 min-w-9/12">
                    <div className="basis-2/4 w-full">
                        <h1>My Goals</h1>
                        <div className="h-full w-full">
                            <Dropzone
                                thisList={myGoals}
                                otherList={suggestions}
                                setThisList={setMyGoals}
                                setOtherList={setSuggestions}
                            />
                        </div>
                    </div>
                    <br/>
                    <div className="basis-2/4 w-full">
                        <h1>Suggestions</h1>
                        <div className="h-full w-full">
                            <Dropzone
                                thisList={suggestions}
                                otherList={myGoals}
                                setThisList={setSuggestions}
                                setOtherList={setMyGoals}
                            />
                        </div>
                    </div>
                </div>
            </div>
        </BaseScreen>
    );
};

export default FinancialGoalsQuestions;