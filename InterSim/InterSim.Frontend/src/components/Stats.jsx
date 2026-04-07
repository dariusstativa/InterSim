export default function Stats() {
  const itemStyle = {
    display: "flex",
    flexDirection: "column",
    gap: "8px",
  };

  const labelStyle = {
    color: "#aaaba9",
    fontSize: "11px",
    letterSpacing: "3px",
    textTransform: "uppercase",
  };

  const barWrapStyle = {
    width: "100%",
    height: "6px",
    background: "#242625",
    marginTop: "10px",
    overflow: "hidden",
  };

  return (
    <section
      style={{
        background: "#121413",
        borderTop: "1px solid #242625",
        borderBottom: "1px solid #242625",
        padding: "64px 24px",
      }}
    >
      <div
        style={{
          maxWidth: "1200px",
          margin: "0 auto",
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(240px, 1fr))",
          gap: "40px",
        }}
      >
        <div style={itemStyle}>
          <span style={{ color: "#00FF41", fontSize: "42px", fontWeight: "bold" }}>50+</span>
          <span style={labelStyle}>Interview Scenarios</span>
          <div style={barWrapStyle}>
            <div style={{ width: "80%", height: "100%", background: "#00FF41" }} />
          </div>
        </div>

        <div style={itemStyle}>
          <span style={{ color: "#81ecff", fontSize: "42px", fontWeight: "bold" }}>120+</span>
          <span style={labelStyle}>Question Library</span>
          <div style={barWrapStyle}>
            <div style={{ width: "70%", height: "100%", background: "#81ecff" }} />
          </div>
        </div>

        <div style={itemStyle}>
          <span style={{ color: "#f7f6f4", fontSize: "42px", fontWeight: "bold" }}>AI</span>
          <span style={labelStyle}>Feedback Reports</span>
          <div style={barWrapStyle}>
            <div style={{ width: "90%", height: "100%", background: "#f7f6f4", opacity: 0.4 }} />
          </div>
        </div>
      </div>
    </section>
  );
}