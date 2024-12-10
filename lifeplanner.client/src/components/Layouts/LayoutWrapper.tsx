// components/LayoutWrapper.tsx
import { useLocation } from "react-router-dom";
import LayoutHome from "./LayoutHome";
import LayoutDashboard from "./LayoutDashboard";

const LayoutWrapper = ({ children }: { children: React.ReactNode }) => {
  const location = useLocation();

  // Determine the layout based on the route
  const layout = location.pathname === "/" ? LayoutHome : LayoutDashboard;

  const SelectedLayout = layout;
  return <SelectedLayout>{children}</SelectedLayout>;
};

export default LayoutWrapper;
