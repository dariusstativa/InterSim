import { useNavigate } from "react-router-dom";

export default function Modules() {
  const navigate = useNavigate();

  const baseCardStyle = {
    background: "rgba(36, 38, 37, 0.45)",
    padding: "32px",
    cursor: "pointer",
    transition: "transform 0.2s ease, border-color 0.2s ease",
  };

  return (
    <section
      style={{
        padding: "100px 24px",
        background: "#0d0f0e",
      }}
    >
      <div style={{ maxWidth: "1200px", margin: "0 auto" }}>
        <div style={{ marginBottom: "48px" }}>
          <div
            style={{
              color: "#acffd9",
              fontSize: "12px",
              letterSpacing: "3px",
              marginBottom: "12px",
            }}
          >
            DEPLOYMENT MODULES
          </div>

          <h2
            style={{
              color: "#f7f6f4",
              fontSize: "48px",
              fontWeight: "bold",
              margin: 0,
            }}
          >
            SELECT YOUR PATH
          </h2>
        </div>

        <div
          style={{
            display: "grid",
            gridTemplateColumns: "repeat(auto-fit, minmax(320px, 1fr))",
            gap: "24px",
          }}
        >
          <div
            onClick={() => navigate("/simulation-settings")}
            onMouseEnter={(e) => {
              e.currentTarget.style.transform = "translateY(-6px)";
              e.currentTarget.style.borderColor = "rgba(172,255,217,0.28)";
            }}
            onMouseLeave={(e) => {
              e.currentTarget.style.transform = "translateY(0px)";
              e.currentTarget.style.borderColor = "rgba(172,255,217,0.12)";
            }}
            style={{
              ...baseCardStyle,
              border: "1px solid rgba(172,255,217,0.12)",
            }}
          >
            <div
              style={{
                width: "56px",
                height: "56px",
                background: "rgba(172,255,217,0.1)",
                border: "1px solid rgba(172,255,217,0.2)",
                marginBottom: "24px",
              }}
            />
            <h3
              style={{
                margin: "0 0 12px 0",
                color: "#f7f6f4",
                fontSize: "28px",
              }}
            >
              BEHAVIORAL INTELLIGENCE
            </h3>
            <p
              style={{
                margin: 0,
                color: "#aaaba9",
                lineHeight: 1.7,
                fontSize: "16px",
              }}
            >
              Practice behavioral interviews with adaptive follow-up questions,
              answer scoring, and AI-guided communication feedback.
            </p>
          </div>

          <div
            onClick={() => navigate("/simulation-settings")}
            onMouseEnter={(e) => {
              e.currentTarget.style.transform = "translateY(-6px)";
              e.currentTarget.style.borderColor = "rgba(129,236,255,0.28)";
            }}
            onMouseLeave={(e) => {
              e.currentTarget.style.transform = "translateY(0px)";
              e.currentTarget.style.borderColor = "rgba(129,236,255,0.12)";
            }}
            style={{
              ...baseCardStyle,
              border: "1px solid rgba(129,236,255,0.12)",
            }}
          >
            <div
              style={{
                width: "56px",
                height: "56px",
                background: "rgba(129,236,255,0.1)",
                border: "1px solid rgba(129,236,255,0.2)",
                marginBottom: "24px",
              }}
            />
            <h3
              style={{
                margin: "0 0 12px 0",
                color: "#f7f6f4",
                fontSize: "28px",
              }}
            >
              TECHNICAL MASTERY
            </h3>
            <p
              style={{
                margin: 0,
                color: "#aaaba9",
                lineHeight: 1.7,
                fontSize: "16px",
              }}
            >
              Simulate technical interviews focused on engineering reasoning,
              structured answers, and future coding or system-design modules.
            </p>
          </div>
        </div>
      </div>
    </section>
  );
}