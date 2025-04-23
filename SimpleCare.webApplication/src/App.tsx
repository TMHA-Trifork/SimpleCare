import "./App.css";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import PatientDetails from "./emergencyWard/PatientDetails";
import Topbar from "./components/Topbar";
import EmergencyWard from "./emergencyWard/emergencyWard";
import { BedWard } from "./bedWard/BedWard";

function App() {
  return (
    <Router>
      <Topbar />
      <Routes>
        <Route path="/" element={<div>Welcome to Simple Care</div>} />
        <Route path="/emergency-ward" element={<EmergencyWard />} />
        <Route path="/emergency-ward/:id" element={<PatientDetails />} />
        <Route path="/bed-ward" element={<BedWard />} />
      </Routes>
    </Router>
  );
}

export default App;
