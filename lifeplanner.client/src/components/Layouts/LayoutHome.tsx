import React from 'react';

const LayoutHome = ({ children }: { children: React.ReactNode }) => {
    return (
        <div>
            <header>Home Header</header>
            <main>{children}</main>
            <footer>Home Footer</footer>
        </div>
    );
};

export default LayoutHome;