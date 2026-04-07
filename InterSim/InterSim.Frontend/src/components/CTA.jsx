import { useNavigate } from "react-router-dom";

export default function CTA() {
  const navigate = useNavigate();

  return (
    <section
      style={{
        padding: "140px 24px",
        textAlign: "center",
        background:
          "radial-gradient(circle at center, rgba(0,255,65,0.08) 0%, rgba(13,15,14,1) 60%)",
      }}
    >
      <div style={{ maxWidth: "900px", margin: "0 auto" }}>
        <h2
          style={{
            fontSize: "64px",
            fontWeight: "bold",
            margin: "0 0 24px 0",
            lineHeight: 1.1,
          }}
        >
          Ready to <span style={{ color: "#00FF41" }}>Evolve?</span>
        </h2>

        <p
          style={{
            color: "#aaaba9",
            fontSize: "20px",
            lineHeight: 1.7,
            marginBottom: "36px",
          }}
        >
          Start your behavioral or technical simulation and receive AI-driven evaluation,
          feedback, and adaptive follow-up questions.
        </p>

        <button
          onClick={() => navigate("/simulation-settings")}
          style={{
            background: "#acffd9",
            color: "#004732",
            border: "none",
            padding: "18px 34px",
            fontWeight: "bold",
            fontSize: "14px",
            letterSpacing: "2px",
            cursor: "pointer",
          }}
        >
          INITIATE PROTOCOL
        </button>
      </div>
    </section>
  );
}