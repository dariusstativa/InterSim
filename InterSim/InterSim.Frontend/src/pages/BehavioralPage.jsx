import { useState } from "react";
import { evaluateWithFollowUpRule } from "../services/api";

export default function BehavioralPage() {
  const [questionText] = useState(
    "Tell me about a time you had a conflict in a team and how you handled it."
  );
  const [answerText, setAnswerText] = useState("");
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async () => {
    setLoading(true);
    setError("");
    setResult(null);

    try {
      const data = await evaluateWithFollowUpRule({
        questionText,
        answerText,
        followUpCount: 0,
        maxFollowUps: 2,
        mode: "Full",
        previousFollowUps: [],
      });

      setResult(data);
    } catch (err) {
      setError(err.message || "Something went wrong.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      style={{
        minHeight: "100vh",
        background: "#0d0f0e",
        color: "#f7f6f4",
        fontFamily: "Arial, sans-serif",
      }}
    >
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
            maxWidth: "1600px",
            margin: "0 auto",
            padding: "18px 24px",
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <div
            style={{
              color: "#00FF41",
              fontWeight: "bold",
              fontSize: "24px",
              letterSpacing: "-1px",
            }}
          >
            InterSim
          </div>

          <div style={{ display: "flex", alignItems: "center", gap: "16px" }}>
            <button
              style={{
                background: "transparent",
                color: "#aaaba9",
                border: "none",
                fontSize: "18px",
                cursor: "pointer",
              }}
            >
              ⚙
            </button>
            <div
              style={{
                width: "34px",
                height: "34px",
                borderRadius: "50%",
                border: "1px solid rgba(172,255,217,0.25)",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                color: "#acffd9",
                background: "#121413",
              }}
            >
              👤
            </div>
          </div>
        </div>
      </nav>

      <div style={{ paddingTop: "78px", display: "flex", minHeight: "100vh" }}>
        <aside
          style={{
            width: "70px",
            borderRight: "1px solid #242625",
            background: "#0d0f0e",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            paddingTop: "32px",
            gap: "28px",
          }}
        >
          <span style={{ color: "#00FF41", fontSize: "20px" }}>⌨</span>
          <span style={{ color: "#545554", fontSize: "20px" }}>📅</span>
          <span style={{ color: "#545554", fontSize: "20px" }}>📊</span>
        </aside>

        <main style={{ flex: 1, display: "flex", flexDirection: "column" }}>
          <div
            style={{
              padding: "20px 28px",
              borderBottom: "1px solid #242625",
              background: "#121413",
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
              gap: "20px",
            }}
          >
            <div>
              <div
                style={{
                  color: "#00FF41",
                  fontSize: "11px",
                  letterSpacing: "2px",
                  textTransform: "uppercase",
                  marginBottom: "6px",
                }}
              >
                Active Session
              </div>
              <div
                style={{
                  fontSize: "24px",
                  fontWeight: "bold",
                  textTransform: "uppercase",
                }}
              >
                Behavioral Interview Simulation
              </div>
            </div>

            <button
              style={{
                background: "linear-gradient(90deg, #acffd9, #00fdbc)",
                color: "#004732",
                border: "none",
                padding: "10px 18px",
                fontWeight: "bold",
                cursor: "pointer",
              }}
            >
              END SESSION
            </button>
          </div>

          <div style={{ display: "flex", flex: 1 }}>
            <div
              style={{
                flex: 1,
                display: "flex",
                flexDirection: "column",
                padding: "32px",
                gap: "24px",
              }}
            >
              <div
                style={{
                  background:
                    "linear-gradient(180deg, rgba(0,255,65,0.08), rgba(13,15,14,0.4))",
                  border: "1px solid #242625",
                  padding: "28px",
                }}
              >
                <div
                  style={{
                    color: "#00FF41",
                    fontSize: "11px",
                    textTransform: "uppercase",
                    letterSpacing: "2px",
                    marginBottom: "10px",
                  }}
                >
                  Architect
                </div>
                <div
                  style={{
                    fontSize: "30px",
                    lineHeight: 1.4,
                    fontWeight: "600",
                  }}
                >
                  “{questionText}”
                </div>
              </div>

              <div
                style={{
                  background: "#181a19",
                  border: "1px solid #242625",
                  padding: "24px",
                  flex: 1,
                  display: "flex",
                  flexDirection: "column",
                }}
              >
                <div
                  style={{
                    color: "#aaaba9",
                    fontSize: "11px",
                    textTransform: "uppercase",
                    letterSpacing: "2px",
                    marginBottom: "16px",
                  }}
                >
                  Real-time Interview Transcript
                </div>

                <div style={{ marginBottom: "20px" }}>
                  <div
                    style={{
                      color: "#00FF41",
                      fontSize: "11px",
                      textTransform: "uppercase",
                      letterSpacing: "2px",
                      marginBottom: "8px",
                    }}
                  >
                    Architect
                  </div>
                  <div style={{ fontSize: "20px", lineHeight: 1.6 }}>
                    {questionText}
                  </div>
                </div>

                {answerText.trim() && (
                  <div style={{ marginBottom: "20px" }}>
                    <div
                      style={{
                        color: "#aaaba9",
                        fontSize: "11px",
                        textTransform: "uppercase",
                        letterSpacing: "2px",
                        marginBottom: "8px",
                      }}
                    >
                      You
                    </div>
                    <div
                      style={{
                        fontSize: "18px",
                        lineHeight: 1.6,
                        color: "#f7f6f4",
                        opacity: 0.92,
                      }}
                    >
                      {answerText}
                    </div>
                  </div>
                )}

                <textarea
                  value={answerText}
                  onChange={(e) => setAnswerText(e.target.value)}
                  placeholder="Write your response here..."
                  style={{
                    width: "100%",
                    minHeight: "180px",
                    background: "#121413",
                    color: "#f7f6f4",
                    border: "1px solid #474847",
                    padding: "16px",
                    fontSize: "16px",
                    resize: "vertical",
                    boxSizing: "border-box",
                    marginTop: "auto",
                    marginBottom: "16px",
                  }}
                />

                <div style={{ display: "flex", gap: "12px", alignItems: "center" }}>
                  <button
                    onClick={handleSubmit}
                    disabled={loading || !answerText.trim()}
                    style={{
                      background: loading ? "#474847" : "#acffd9",
                      color: "#004732",
                      border: "none",
                      padding: "14px 24px",
                      fontWeight: "bold",
                      cursor: loading ? "default" : "pointer",
                    }}
                  >
                    {loading ? "EVALUATING..." : "SUBMIT RESPONSE"}
                  </button>

                  {error && (
                    <div style={{ color: "#ff7351" }}>
                      {error}
                    </div>
                  )}
                </div>
              </div>
            </div>

            <div
              style={{
                width: "340px",
                borderLeft: "1px solid #242625",
                background: "#121413",
                padding: "28px",
              }}
            >
              <div
                style={{
                  color: "#aaaba9",
                  fontSize: "11px",
                  textTransform: "uppercase",
                  letterSpacing: "2px",
                  marginBottom: "18px",
                }}
              >
                Interview Metrics
              </div>

              <div style={{ marginBottom: "18px" }}>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    fontSize: "12px",
                    marginBottom: "6px",
                  }}
                >
                  <span style={{ color: "#aaaba9" }}>Communication Clarity</span>
                  <span style={{ color: "#00FF41" }}>
                    {result ? `${result.relevance}%` : "—"}
                  </span>
                </div>
                <div style={{ height: "6px", background: "#242625" }}>
                  <div
                    style={{
                      width: result ? `${Math.min(result.relevance * 4, 100)}%` : "0%",
                      height: "100%",
                      background: "#00FF41",
                    }}
                  />
                </div>
              </div>

              <div style={{ marginBottom: "18px" }}>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    fontSize: "12px",
                    marginBottom: "6px",
                  }}
                >
                  <span style={{ color: "#aaaba9" }}>Structure</span>
                  <span style={{ color: "#81ecff" }}>
                    {result ? `${result.structure}%` : "—"}
                  </span>
                </div>
                <div style={{ height: "6px", background: "#242625" }}>
                  <div
                    style={{
                      width: result ? `${Math.min(result.structure * 4, 100)}%` : "0%",
                      height: "100%",
                      background: "#81ecff",
                    }}
                  />
                </div>
              </div>

              <div style={{ marginBottom: "28px" }}>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    fontSize: "12px",
                    marginBottom: "6px",
                  }}
                >
                  <span style={{ color: "#aaaba9" }}>Final Score</span>
                  <span style={{ color: "#f7f6f4" }}>
                    {result ? result.score : "—"}
                  </span>
                </div>
                <div style={{ height: "6px", background: "#242625" }}>
                  <div
                    style={{
                      width: result ? `${result.score}%` : "0%",
                      height: "100%",
                      background: "#f7f6f4",
                      opacity: 0.6,
                    }}
                  />
                </div>
              </div>

              <div
                style={{
                  background: "#0d0f0e",
                  border: "1px solid #242625",
                  padding: "18px",
                  fontFamily: "monospace",
                  fontSize: "11px",
                  lineHeight: 1.8,
                }}
              >
                <div style={{ color: "#00FF41" }}>
                  {result ? "> evaluation complete" : "> waiting for response"}
                </div>
                <div style={{ color: "#aaaba9" }}>
                  {result
                    ? `> follow-up required: ${result.shouldAskFollowUp ? "yes" : "no"}`
                    : "> no metrics yet"}
                </div>
                <div style={{ color: "#aaaba9" }}>
                  {result?.followUpReason
                    ? `> reason: ${result.followUpReason}`
                    : "> reason: -"}
                </div>
              </div>

              {result && (
                <div
                  style={{
                    marginTop: "24px",
                    background: "#181a19",
                    border: "1px solid #242625",
                    padding: "18px",
                  }}
                >
                  <div
                    style={{
                      color: "#aaaba9",
                      fontSize: "11px",
                      textTransform: "uppercase",
                      letterSpacing: "2px",
                      marginBottom: "10px",
                    }}
                  >
                    Deficits
                  </div>
                  <ul style={{ margin: 0, paddingLeft: "18px", color: "#f7f6f4" }}>
                    {result.deficits?.map((item, index) => (
                      <li key={index}>{item}</li>
                    ))}
                  </ul>

                  <div style={{ marginTop: "16px", color: "#acffd9" }}>
                    <strong>Follow-up:</strong>{" "}
                    {result.followUpQuestion || "No follow-up generated."}
                  </div>
                </div>
              )}
            </div>
          </div>

          <footer
            style={{
              height: "72px",
              borderTop: "1px solid #242625",
              background: "#1e201f",
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
              padding: "0 28px",
            }}
          >
            <div style={{ color: "#aaaba9", fontSize: "12px", letterSpacing: "1px" }}>
              SIMULATION PROGRESS
            </div>
            <div style={{ color: "#00FF41", fontWeight: "bold" }}>
              {result ? `CONFIDENCE INDEX ${result.score}` : "SESSION ACTIVE"}
            </div>
          </footer>
        </main>
      </div>
    </div>
  );
}