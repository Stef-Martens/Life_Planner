import { useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";

import { ReactNode } from "react";

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
  const { isAuthenticated, isLoading } = useAuth0();

  useEffect(() => {
    if (!isAuthenticated && !isLoading) {
      window.location.href = "/";
    }
  }, [isAuthenticated, isLoading]);

  if (isLoading) {
    return (
      <div className="h-screen w-screen flex justify-center items-center text-white">
        <div className="flex flex-col items-center">
          <div className="loader ease-linear rounded-full border-4 border-t-4 border-white h-12 w-12 mb-4"></div>
          <p>{loadingText}</p>
        </div>
      </div>
    );
  }

  return (
    <div className="h-screen flex flex-col text-white">
      <div className="flex-grow">{children}</div>
      <div className="flex justify-between w-screen p-8">
        {onBack && (
          <button className="btn btn-error" onClick={onBack}>
            Back
          </button>
        )}
        {onContinue && (
          <button className="btn btn-success" onClick={onContinue}>
            Continue
          </button>
        )}
      </div>
    </div>
  );
};

export default BaseScreenQuestions;
