import React, { useEffect } from "react";
import Header from "./Header";

const LayoutDashboard: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <div className="flex flex-col h-screen">
      {/* Header */}
      <Header />

      {/* Main content area */}
      <main className="flex-1 p-6 overflow-auto">
        {/* Children content */}
        {children}
        {/* You can replace the content here with dynamic children */}
      </main>
    </div>
  );
};

export default LayoutDashboard;
