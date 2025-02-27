import { useEffect, useState } from "react";
import { useUserApi } from "../api/users";
import { useGoalApi } from "../api/goals";
import { User } from "../types/user";
import { useAuth0 } from "@auth0/auth0-react";

const Dashboard = () => {
  const [isLoading, setIsLoading] = useState(true);
  const { user, isAuthenticated } = useAuth0();
  const [userRecord, setUserRecord] = useState<User | null>(null);
  const [goals, setGoals] = useState([] as any[]);
  const { getUserByAuth0Id, createUser } = useUserApi();
  const { getGoals, checkIfGoalsAreNeeded } = useGoalApi();

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

      if (userRecord && userRecord.id) {
        await getGoalsFromBackend();

        var goalsNeeded = await checkIfGoalsAreNeeded(userRecord.id);
        console.log(goalsNeeded);
        if(goalsNeeded?.newGoalsNeeded) {
          // go to question page 1
          window.location.href = "/question1?year=" + goalsNeeded?.year;
        }
      }

      setIsLoading(false);
    };

    initializeDashboard();
  }, [user, isAuthenticated, isUserInitialized]); // Dependency array ensures proper updates

  const getGoalsFromBackend = async () => {
    if (!userRecord) {
      console.error("User record not found");
      return;
    }

    try {
      if (userRecord.id !== undefined) {
        setGoals(await getGoals(userRecord.id));
      } else {
        console.error("User ID is undefined");
      }
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
    </div>
  );
};

export default Dashboard;
