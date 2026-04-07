import Navbar from "../components/Navbar";
import Hero from "../components/Hero";
import Stats from "../components/Stats";
import Modules from "../components/Modules";
import Analysis from "../components/Analysis";
import CTA from "../components/CTA";
import Footer from "../components/Footer";

export default function HomePage() {
  return (
    <div style={{ minHeight: "100vh", background: "#0d0f0e", color: "#f7f6f4" }}>
      <Navbar />
      <Hero />
      <Stats />
      <Modules />
      <Analysis />
      <CTA />
      <Footer />
    </div>
  );
}