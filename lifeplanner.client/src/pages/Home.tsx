import { useEffect } from "react";
import LoginButton from "../components/Buttons/LoginButton";
import { useAuth0 } from "@auth0/auth0-react";
import { ReactTyped } from "react-typed";
import DoeiButton from "../components/Buttons/DoeiButton";

const Home = () => {
  const { isAuthenticated, isLoading } = useAuth0();

  useEffect(() => {
    if (isAuthenticated && !isLoading) {
      window.location.href = "/dashboard";
    }
  }, [isAuthenticated, isLoading]);

  if (isLoading) {
    return (
      <div className="h-screen flex justify-center items-center bg-gradient-to-br from-blue-500 to-indigo-700 text-white">
        <div className="flex flex-col items-center">
          <div className="loader ease-linear rounded-full border-4 border-t-4 border-white h-12 w-12 mb-4"></div>
          <p>Loading...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="h-screen flex flex-col justify-center items-center bg-gradient-to-br from-gray-100 to-blue-100">
      <h1 className="text-4xl font-bold text-gray-700 mb-6 text-center">
        <ReactTyped
          strings={[
            "Welcome to the Best Life Planner ooit xpxdx3!!! :)))",
            // "Achieve your dreams, one step at a time.",
          ]}
          typeSpeed={60}
          // backSpeed={30}
          // loop
        />
      </h1>
      <div className="flex gap-x-8">
        <DoeiButton />
        <LoginButton />
      </div>
    </div>
  );
};

export default Home;
