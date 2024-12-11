import React, { useEffect, useState } from "react";
import { useUserApi } from "../api/users";
import { useGoalApi } from "../api/goals";
import { User } from "../types/user";
import { useAuth0 } from "@auth0/auth0-react";

const Dashboard = () => {
  const [isLoading, setIsLoading] = useState(true);
  const { user, isAuthenticated } = useAuth0();
  const [userRecord, setUserRecord] = useState<User | null>(null);
  const { getUserByAuth0Id, createUser } = useUserApi();
  const { getGoals } = useGoalApi();

  // Prevent re-calling backend if user has already been processed
  const [isUserInitialized, setIsUserInitialized] = useState(false);

  // Helper to fetch or create the user
  const fetchOrCreateUser = async () => {
    if (!user || !user.sub || !isAuthenticated) return;

    try {
      const existingUser = await getUserByAuth0Id(user.sub);

      if (existingUser) {
        setUserRecord(existingUser);
      } else if (isAuthenticated && user.email && user.name) {
        const newUser: User = {
          auth0Id: user.sub,
          email: user.email,
          name: user.name,
        };

        await createUser(newUser);
        setUserRecord(newUser);
      }

      setIsUserInitialized(true); // Mark user initialization as complete
    } catch (error) {
      console.error("Error handling user data:", error);
    }
  };

  useEffect(() => {
    const initializeDashboard = async () => {
      setIsLoading(true);
      await fetchOrCreateUser();

      setIsLoading(false);
    };

    initializeDashboard();
  }, [user, isAuthenticated, isUserInitialized]); // Dependency array ensures proper updates

  const logGoals = async () => {
    if (!userRecord) {
      console.error("User record not found");
      return;
    }

    try {
      const goals = await getGoals(userRecord.id);
      console.log("Goals for user:", goals);
    } catch (error) {
      console.error("Error fetching goals:", error);
    }
  };

  if (isLoading) {
    return <span className="loading loading-spinner loading-lg"></span>;
  }

  return (
    <div>
      <h1>Dashboard</h1>
      <button className="btn" onClick={logGoals}>
        Log goals
      </button>
    </div>
  );
};

export default Dashboard;
