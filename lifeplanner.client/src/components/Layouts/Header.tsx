import React from "react";
import LogoutButton from "../Buttons/LogoutButton";
import logo from "../../../public/appa.png";
import { useAuth0 } from "@auth0/auth0-react";

const Header: React.FC = () => {
  const { user } = useAuth0();

  // Navigation items can be stored in an array for better scalability
  const navLinks = [
    { label: "Dashboard", href: "/dashboard" },
    { label: "Goals", href: "/goals" },
    { label: "Insights", href: "/insights" },
    { label: "History", href: "/history" },
    { label: "Settings", href: "/settings" },
  ];

  return (
    <header className="p-4">
      <div className="mx-auto flex justify-between items-center">
        <div className="flex items-center justify-center space-x-6">
          {/* Logo */}
          <div className="w-12 h-12 rounded-full overflow-hidden">
            <img src={logo} alt="Appa" className="w-full h-full object-cover" />
          </div>

          {/* Navigation Bar */}
          <nav className="flex flex-wrap w-auto rounded-full h-auto bg-neutral text-neutral-content justify-between">
            {navLinks.map((link) => (
              <a
                key={link.href}
                href={link.href}
                className={
                  "hover:bg-base-300 rounded-full px-6 sm:px-3 md:px-2 lg:px-6 xl:px-8 py-3  transition-colors duration-300"
                }
                aria-label={link.label}
              >
                {link.label}
              </a>
            ))}
          </nav>
        </div>

        <div className="flex items-center justify-center space-x-4">
          {/* show a search button */}
          <button className="btn hover:bg-base-300 border-0 bg-neutral  rounded-full transition-colors duration-300">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
            >
              <circle cx="11" cy="11" r="8" />
              <line x1="16" y1="16" x2="20" y2="20" />
            </svg>
          </button>
          <p>Hoi, {user?.name} :)</p>
          <div className="w-12 h-12 rounded-full overflow-hidden">
            <img
              src={user?.picture}
              alt="PF"
              className="w-full h-full object-cover"
            />
          </div>
          {/* Logout Button */}
          <LogoutButton />
        </div>
      </div>
    </header>
  );
};

export default Header;
