import React from "react";

const LayoutHome = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="min-h-screen flex flex-col">
      {/* Header */}
      {/* <header className="text-primary-content shadow-md">
        <div className="container mx-auto px-4 py-4 flex items-center justify-between">
          <h1 className="text-2xl font-bold">Home Header</h1>
          <nav className="space-x-4">
            <a href="#" className="btn btn-sm btn-ghost">
              Home
            </a>
            <a href="#" className="btn btn-sm btn-ghost">
              About
            </a>
            <a href="#" className="btn btn-sm btn-ghost">
              Contact
            </a>
          </nav>
        </div>
      </header> */}

      {/* Main Content */}
      <main className="">
        <div className="">{children}</div>
      </main>

      {/* Footer */}
      {/* <footer className="bg-neutral text-neutral-content py-4">
        <div className="container mx-auto text-center">
          <p>© {new Date().getFullYear()} Hoi :)</p>
        </div>
      </footer> */}
    </div>
  );
};

export default LayoutHome;
