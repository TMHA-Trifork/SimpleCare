import React from "react";
import { Link } from "react-router-dom";
import "./Topbar.css";

const Topbar: React.FC = () => {
  return (
    <div className="topbar-container">
      <div className="topbar-logo">SimpleCare</div>
      <div className="topbar-links">
        <Link to="/" className="topbar-link">
          Home
        </Link>
        <Link to="/emergency-ward" className="topbar-link">
          Emergency Ward
        </Link>
        <Link to="/bed-ward" className="topbar-link">
          Bed Ward
        </Link>
      </div>
    </div>
  );
};

export default Topbar;
