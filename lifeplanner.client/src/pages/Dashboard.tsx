import React, { useEffect, useState } from "react";
import { getUserByAuth0Id, createUser } from "../api/users"; // Removed updateUser
import { User } from "../types/user";
import { useAuth0 } from "@auth0/auth0-react";

const Dashboard = () => {
  const [isLoading, setIsLoading] = useState(true);
  const { user, isAuthenticated } = useAuth0();

  useEffect(() => {
    const checkUser = async () => {
      try {
        if (user && user.sub) {
          const existingUser = await getUserByAuth0Id(user.sub);
          if (!existingUser && isAuthenticated && user.email && user.name) {
            const newUser: User = {
              auth0Id: user.sub,
              email: user.email,
              name: user.name,
            };
            console.log("Creating user:", newUser);
            await createUser(newUser);
          }
        }
        setIsLoading(false);
      } catch (error) {
        console.error("Error in user handling:", error);
        setIsLoading(false);
      }
    };

    checkUser();
  }, [user, isAuthenticated]);

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
