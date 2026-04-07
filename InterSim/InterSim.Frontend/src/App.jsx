import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import BehavioralPage from "./pages/BehavioralPage";
import SimulationSettingsPage from "./pages/SimulationSettingsPage";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/simulation-settings" element={<SimulationSettingsPage />} />
      <Route path="/behavioral" element={<BehavioralPage />} />
    </Routes>
  );
}