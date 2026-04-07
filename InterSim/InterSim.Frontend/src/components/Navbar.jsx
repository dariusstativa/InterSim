import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const navigate = useNavigate();

  return (
    <nav
      style={{
        position: "fixed",
        top: 0,
        width: "100%",
        zIndex: 100,
        background: "rgba(13,15,14,0.85)",
        backdropFilter: "blur(10px)",
        borderBottom: "1px solid #242625",
      }}
    >
      <div
        style={{
          maxWidth: "1400px",
          margin: "0 auto",
          padding: "18px 24px",
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          gap: "20px",
        }}
      >
        <div
          style={{
            color: "#00FF41",
            fontWeight: "bold",
            fontSize: "28px",
            letterSpacing: "-1px",
            cursor: "pointer",
          }}
          onClick={() => navigate("/")}
        >
          InterSim
        </div>

        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "28px",
            color: "#aaaba9",
            fontSize: "14px",
            textTransform: "uppercase",
            letterSpacing: "1px",
          }}
        >
          <span style={{ cursor: "pointer" }} onClick={() => navigate("/")}>
            Simulations
          </span>
          <span style={{ cursor: "pointer" }}>Analysis</span>
          <span style={{ cursor: "pointer" }}>Resources</span>
        </div>

        <div
          style={{
            display: "flex",
            alignItems: "center",
            gap: "14px",
          }}
        >
          <div
            style={{
              width: "38px",
              height: "38px",
              borderRadius: "50%",
              border: "1px solid rgba(172,255,217,0.25)",
              background: "#121413",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              color: "#acffd9",
              fontSize: "16px",
              cursor: "pointer",
            }}
            title="User profile"
          >
            👤
          </div>

          <button
            onClick={() => navigate("/simulation-settings")}
            style={{
              background: "linear-gradient(90deg, #acffd9, #00fdbc)",
              color: "#004732",
              border: "none",
              padding: "10px 18px",
              fontWeight: "bold",
              cursor: "pointer",
            }}
          >
            START SESSION
          </button>
        </div>
      </div>
    </nav>
  );
}