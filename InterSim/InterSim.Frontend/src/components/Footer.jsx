export default function Footer() {
  return (
    <footer
      style={{
        borderTop: "1px solid #242625",
        background: "#000000",
        padding: "36px 24px",
      }}
    >
      <div
        style={{
          maxWidth: "1200px",
          margin: "0 auto",
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          gap: "20px",
          flexWrap: "wrap",
        }}
      >
        <div style={{ color: "#00FF41", fontWeight: "bold", fontSize: "24px" }}>
          CYBERNODE
        </div>

        <div
          style={{
            color: "#aaaba9",
            fontSize: "11px",
            letterSpacing: "2px",
            textTransform: "uppercase",
          }}
        >
          © 2026 InterSim. System Status: Operational
        </div>
      </div>
    </footer>
  );
}