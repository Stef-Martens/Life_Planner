// components/LayoutWrapper.tsx
import { useLocation } from "react-router-dom";
import LayoutHome from "./LayoutHome";
import LayoutDashboard from "./LayoutDashboard";

const LayoutWrapper = ({ children }: { children: React.ReactNode }) => {
  const location = useLocation();

  // Determine the layout based on the route
  // check if it is a pathname that ends with "question" and then something, like an asterisk
  const isQuestionPath = location.pathname.match(/\/question.*/);
  const layout =
    location.pathname === "/" || isQuestionPath ? LayoutHome : LayoutDashboard;

  const SelectedLayout = layout;
  return <SelectedLayout>{children}</SelectedLayout>;
};

export default LayoutWrapper;
