import React from "react";
import {
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Home from "../pages/Home";
import Dashboard from "../pages/Dashboard";
import { useAuth0 } from "@auth0/auth0-react";
import ProtectedRoute from "./ProtectedRoute";
import IntroGoalQuestions from "../pages/questions/IntroGoalsQuestions";
import FinancialGoalsQuestions from "../pages/questions/FinancialGoalsQuestions.tsx";

const AppRoutes: React.FC = () => {
  const { isLoading, isAuthenticated } = useAuth0();

  if (isLoading) {
    return <span className="loading loading-spinner loading-lg"></span>;
  }

  return (
    <Routes>
      {/* Redirect to home if unauthenticated, otherwise dashboard */}
      <Route
        path="*"
        element={<Navigate to={isAuthenticated ? "/dashboard" : "/"} />}
      />

      <Route path="/" element={<Home />} />
      <Route
        path="/dashboard"
        element={
          <ProtectedRoute>
            <Dashboard />
          </ProtectedRoute>
        }
      />
      <Route
        path="/question1"
        element={
          <ProtectedRoute>
            <IntroGoalQuestions />
          </ProtectedRoute>
        }
      />
        <Route
        path="/question2"
        element={
          <ProtectedRoute>
            <FinancialGoalsQuestions />
          </ProtectedRoute>
        }
        />
    </Routes>
  );
};

export default AppRoutes;
