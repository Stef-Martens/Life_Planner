import { useEffect } from "react";
import LoginButton from "../components/LoginButton";
import { useAuth0 } from "@auth0/auth0-react";

const Home = () => {
  const { isAuthenticated, isLoading } = useAuth0();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  useEffect(() => {
    if (isAuthenticated && !isLoading) {
      window.location.href = "/dashboard";
    }
  }, [isAuthenticated, isLoading]);

  return (
    <div>
      <h1>Welcome to Life Planner</h1>
      <p>Please log in to continue</p>
      <LoginButton />
    </div>
  );
};

export default Home;
