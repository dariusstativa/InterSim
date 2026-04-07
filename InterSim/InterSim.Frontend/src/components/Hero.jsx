import { useNavigate } from "react-router-dom";

export default function Hero() {
  const navigate = useNavigate();

  return (
    <section
      style={{
        minHeight: "100vh",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        textAlign: "center",
        padding: "0 24px",
        background:
          "radial-gradient(circle at center, rgba(0,255,65,0.12) 0%, rgba(13,15,14,1) 55%)",
      }}
    >
      <div style={{ maxWidth: "1000px" }}>
        <div
          style={{
            display: "inline-block",
            padding: "8px 14px",
            border: "1px solid #2a2d2b",
            marginBottom: "24px",
            color: "#acffd9",
            fontSize: "12px",
            letterSpacing: "2px",
          }}
        >
          SYSTEM ONLINE
        </div>

        <h1
          style={{
            fontSize: "72px",
            fontWeight: "bold",
            lineHeight: 1,
            margin: "0 0 24px 0",
            color: "#f7f6f4",
          }}
        >
          MASTER THE
          <br />
          <span style={{ color: "#00FF41" }}>INTERVIEW GRID</span>
        </h1>

        <p
          style={{
            maxWidth: "700px",
            margin: "0 auto 36px auto",
            color: "#aaaba9",
            fontSize: "20px",
            lineHeight: 1.6,
          }}
        >
          AI-driven behavioral and technical interview simulations with adaptive
          follow-up questions and intelligent evaluation.
        </p>

        <div
          style={{
            display: "flex",
            gap: "16px",
            justifyContent: "center",
            flexWrap: "wrap",
          }}
        >
          <button
            onClick={() => navigate("/simulation-settings")}
            style={{
              background: "#acffd9",
              color: "#004732",
              border: "none",
              padding: "16px 28px",
              fontWeight: "bold",
              cursor: "pointer",
            }}
          >
            START SESSION
          </button>

          <button
            onClick={() =>
              window.scrollTo({
                top: document.body.scrollHeight,
                behavior: "smooth",
              })
            }
            style={{
              background: "transparent",
              color: "#f7f6f4",
              border: "1px solid #474847",
              padding: "16px 28px",
              fontWeight: "bold",
              cursor: "pointer",
            }}
          >
            VIEW PROTOCOL
          </button>
        </div>
      </div>
    </section>
  );
}