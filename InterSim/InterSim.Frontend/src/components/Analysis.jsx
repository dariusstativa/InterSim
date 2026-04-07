export default function Analysis() {
  const metric = (label, value, color, width) => (
    <div style={{ marginBottom: "18px" }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          fontSize: "11px",
          textTransform: "uppercase",
          letterSpacing: "2px",
          marginBottom: "6px",
        }}
      >
        <span>{label}</span>
        <span style={{ color }}>{value}</span>
      </div>
      <div style={{ height: "6px", background: "#242625" }}>
        <div style={{ width, height: "100%", background: color }} />
      </div>
    </div>
  );

  return (
    <section style={{ padding: "120px 24px", background: "#000000" }}>
      <div
        style={{
          maxWidth: "1200px",
          margin: "0 auto",
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(320px, 1fr))",
          gap: "50px",
          alignItems: "center",
        }}
      >
        <div>
          <div
            style={{
              color: "#81ecff",
              fontSize: "12px",
              letterSpacing: "4px",
              textTransform: "uppercase",
              marginBottom: "14px",
            }}
          >
            Neural Feedback
          </div>

          <h2
            style={{
              fontSize: "52px",
              fontWeight: "bold",
              lineHeight: 1.1,
              margin: "0 0 24px 0",
            }}
          >
            REAL-TIME
            <br />
            COGNITIVE MAPPING
          </h2>

          <p style={{ color: "#aaaba9", lineHeight: 1.8, fontSize: "18px", marginBottom: "28px" }}>
            InterSim analyzes interview answers, detects missing behavioral elements,
            and generates targeted follow-up questions to simulate a more realistic interview flow.
          </p>

          <ul style={{ color: "#aaaba9", paddingLeft: "18px", lineHeight: 1.9 }}>
            <li>Behavioral answer structure analysis</li>
            <li>Adaptive follow-up generation</li>
            <li>Rule-based and LLM-based decision logic</li>
          </ul>
        </div>

        <div
          style={{
            background: "#121413",
            border: "1px solid #242625",
            padding: "28px",
            boxShadow: "0 20px 50px rgba(0,0,0,0.4)",
          }}
        >
          <div
            style={{
              display: "flex",
              justifyContent: "space-between",
              marginBottom: "28px",
              fontSize: "12px",
              letterSpacing: "2px",
              textTransform: "uppercase",
            }}
          >
            <span style={{ color: "#aaaba9" }}>Session Analytics</span>
            <span style={{ color: "#00FF41" }}>Live Feed</span>
          </div>

          {metric("Communication Clarity", "88%", "#00FF41", "88%")}
          {metric("Resolution Speed", "94%", "#81ecff", "94%")}
          {metric("Stress Tolerance", "76%", "#f7f6f4", "76%")}

          <div
            style={{
              marginTop: "28px",
              background: "#0d0f0e",
              border: "1px solid #242625",
              padding: "18px",
              fontFamily: "monospace",
              fontSize: "11px",
              color: "#aaaba9",
              lineHeight: 1.8,
            }}
          >
            <div style={{ color: "#00FF41" }}>&gt; analyzing behavioral response...</div>
            <div>&gt; detected: missing measurable result</div>
            <div>&gt; detected: weak task clarity</div>
            <div style={{ color: "#81ecff" }}>&gt; recommendation: ask follow-up question</div>
          </div>
        </div>
      </div>
    </section>
  );
}