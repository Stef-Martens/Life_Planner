import { useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { ReactTyped } from "react-typed";

const IntroGoalQuestions = () => {
  const { isAuthenticated, isLoading } = useAuth0();

  useEffect(() => {
    if (!isAuthenticated && !isLoading) {
      window.location.href = "/";
    }
  }, [isAuthenticated, isLoading]);

  if (isLoading) {
    return (
      <div className="h-screen flex justify-center items-center bg-gradient-to-br from-blue-500 to-indigo-700 text-white">
        <div className="flex flex-col items-center">
          <div className="loader ease-linear rounded-full border-4 border-t-4 border-white h-12 w-12 mb-4"></div>
        </div>
      </div>
    );
  }

  return (
    <div className="h-screen flex flex-col justify-end items-center bg-gradient-to-br ">
      <h1 className="text-4xl font-bold  mb-6 content-center text-center h-full ">
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
      <div className="flex justify-between w-screen justify-end h-min p-8">
        <button
          className="btn btn-error"
          onClick={() => alert("HAHAHAHAH GAAT NI LOSER")}
        >
          Back
        </button>
        <button className="btn btn-success">Continue :)</button>
      </div>
    </div>
  );
};

export default IntroGoalQuestions;
