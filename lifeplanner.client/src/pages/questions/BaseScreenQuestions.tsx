import {ReactNode, useEffect} from "react";
import {useAuth0} from "@auth0/auth0-react";
import {useNavigate} from "react-router-dom";
import {DndProvider} from "react-dnd";
import {HTML5Backend} from "react-dnd-html5-backend";

interface BaseScreenQuestionsProps {
    children: ReactNode;
    onBack?: () => void;
    onContinue?: () => void;
    loadingText?: string;
}

const BaseScreenQuestions = ({
                                 children,
                                 onBack,
                                 onContinue,
                                 loadingText = "Loading...",
                             }: BaseScreenQuestionsProps) => {
    const {isAuthenticated, isLoading} = useAuth0();

    const urlPath = window.location.pathname;
    const match = urlPath.match(/question(\d+)/); // Extract question number
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuthenticated && !isLoading) {
            window.location.href = "/";
        }
    }, [isAuthenticated, isLoading]);

    const handleBack = () => {
        if (onBack) {
            onBack();
        } else {
            if (match) {
                const currentQuestionNumber = parseInt(match[1], 10);
                const previousQuestionNumber = currentQuestionNumber - 1;
                if (previousQuestionNumber > 0) {
                    navigate(`/question${previousQuestionNumber}${window.location.search}`);
                    return;
                }
            }
            alert("No previous question!"); // Default behavior if no valid "Back" target
        }
    }

    const handleContinue = () => {
        if (onContinue) {
            onContinue();
        } else {
            if (match) {
                const currentQuestionNumber = parseInt(match[1], 10);
                const nextQuestionNumber = currentQuestionNumber + 1;
                navigate(`/question${nextQuestionNumber}${window.location.search}`);
            }
        }
    }

    if (isLoading) {
        return (
            <div className="h-screen w-screen flex justify-center items-center ">
                <div className="flex flex-col items-center">
                    <div
                        className="loader ease-linear rounded-full border-4 border-t-4 border-white h-12 w-12 mb-4"></div>
                    <p>{loadingText}</p>
                </div>
            </div>
        );
    }

    return (
        <DndProvider backend={HTML5Backend}>
            <div className="h-screen flex flex-col">
                <div className="flex-grow">{children}</div>
                <div className="flex justify-between w-screen p-8">
                    <button className="btn btn-error" onClick={handleBack}>
                        Back
                    </button>
                    <button className="btn btn-success" onClick={handleContinue}>
                        Continue
                    </button>
                </div>
            </div>
        </DndProvider>

    );
};

export default BaseScreenQuestions;
