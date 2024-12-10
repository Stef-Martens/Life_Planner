import { BrowserRouter } from "react-router-dom";
import LayoutWrapper from "./components/Layouts/LayoutWrapper";
import AppRoutes from "./routes/AppRoutes";

const App = () => {
  return (
    <BrowserRouter>
      <LayoutWrapper>
        <AppRoutes />
      </LayoutWrapper>
    </BrowserRouter>
  );
};

export default App;
