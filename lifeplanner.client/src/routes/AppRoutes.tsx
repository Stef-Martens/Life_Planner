import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Dashboard from "../pages/Dashboard";
import { Navigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import ProtectedRoute from "./ProtectedRoute";
import IntroGoalQuestions from "../pages/questions/IntroGoalsQuestions";

const AppRoutes: React.FC = () => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    return <span className="loading loading-spinner loading-lg"></span>;
  }

  return (
    <Routes>
      <Route path="*" element={<Navigate to="/" />} />

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
    </Routes>
  );
};

export default AppRoutes;
