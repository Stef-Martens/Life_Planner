import { useAuth0 } from "@auth0/auth0-react";

const LogoutButton = () => {
  const { logout, isAuthenticated } = useAuth0();
  return isAuthenticated ? (
    <button className="btn" onClick={() => logout()}>
      Log Out
    </button>
  ) : (
    <button className="btn ">
      <div className="loading loading-spinner "></div>
    </button>
  );
};

export default LogoutButton;
