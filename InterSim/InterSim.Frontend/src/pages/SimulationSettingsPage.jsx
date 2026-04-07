import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function SimulationSettingsPage() {
  const navigate = useNavigate();

  const [domain, setDomain] = useState("Backend");
  const [level, setLevel] = useState("Senior / Lead");
  const [sessionType, setSessionType] = useState("Technical");

  const domainCardStyle = (active) => ({
    background: active ? "#242625" : "#181a19",
    border: active ? "1px solid rgba(0,255,65,0.35)" : "1px solid rgba(71,72,71,0.25)",
    boxShadow: active ? "inset 3px 0 0 #00FF41" : "none",
    padding: "22px",
    cursor: "pointer",
    transition: "all 0.2s ease",
    minHeight: "128px",
    display: "flex",
    flexDirection: "column",
    justifyContent: "space-between",
  });

  const levelButtonStyle = (active) => ({
    width: "100%",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    padding: "14px 16px",
    background: active ? "#242625" : "#181a19",
    border: active ? "1px solid rgba(0,255,65,0.35)" : "1px solid rgba(71,72,71,0.25)",
    boxShadow: active ? "inset 3px 0 0 #00FF41" : "none",
    color: active ? "#acffd9" : "#f7f6f4",
    cursor: "pointer",
    fontWeight: active ? "bold" : "normal",
    transition: "all 0.2s ease",
  });

  const sessionButtonStyle = (active) => ({
    flex: 1,
    padding: "12px 10px",
    border: "none",
    background: active ? "#242625" : "transparent",
    color: active ? "#f7f6f4" : "#747674",
    fontWeight: "bold",
    cursor: "pointer",
    textTransform: "uppercase",
    letterSpacing: "1px",
    fontSize: "11px",
  });

  const sidebarItemStyle = (active) => ({
    display: "flex",
    alignItems: "center",
    gap: "12px",
    padding: "12px 14px",
    color: active ? "#00FF41" : "#747674",
    background: active ? "#242625" : "transparent",
    borderRight: active ? "4px solid #00FF41" : "4px solid transparent",
    cursor: "pointer",
    textTransform: "uppercase",
    letterSpacing: "1px",
    fontSize: "12px",
    fontWeight: active ? "bold" : "normal",
  });

  return (
    <div
      style={{
        minHeight: "100vh",
        background: "#0d0f0e",
        color: "#f7f6f4",
      }}
    >
      <nav
        style={{
          position: "fixed",
          top: 0,
          width: "100%",
          zIndex: 100,
          background: "rgba(13,15,14,0.88)",
          backdropFilter: "blur(12px)",
          borderBottom: "1px solid #242625",
        }}
      >
        <div
          style={{
            maxWidth: "1700px",
            margin: "0 auto",
            padding: "16px 24px",
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            gap: "20px",
          }}
        >
          <div
            onClick={() => navigate("/")}
            style={{
              color: "#00FF41",
              fontWeight: "bold",
              fontSize: "24px",
              cursor: "pointer",
            }}
          >
            InterSim
          </div>

          <div
            style={{
              display: "flex",
              alignItems: "center",
              gap: "28px",
              color: "#747674",
              fontSize: "13px",
            }}
          >
            <span style={{ color: "#acffd9", borderBottom: "2px solid #acffd9", paddingBottom: "4px" }}>
              Simulate
            </span>
            <span>History</span>
            <span>Academy</span>
            <span>Benchmarks</span>
          </div>

          <div style={{ display: "flex", alignItems: "center", gap: "12px" }}>
            <button
              style={{
                background: "transparent",
                border: "none",
                color: "#747674",
                cursor: "pointer",
                fontSize: "16px",
              }}
            >
              🔔
            </button>
            <button
              style={{
                background: "transparent",
                border: "none",
                color: "#747674",
                cursor: "pointer",
                fontSize: "16px",
              }}
            >
              ⚙
            </button>
            <div
              style={{
                width: "34px",
                height: "34px",
                borderRadius: "50%",
                border: "1px solid #474847",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                background: "#121413",
              }}
            >
              👤
            </div>
          </div>
        </div>
      </nav>

      <div style={{ display: "flex", paddingTop: "74px", minHeight: "100vh" }}>
        <aside
          style={{
            width: "250px",
            background: "#0d0f0e",
            borderRight: "1px solid #242625",
            padding: "20px 16px",
            display: "flex",
            flexDirection: "column",
          }}
        >
          <div style={{ marginBottom: "28px", padding: "6px 10px" }}>
            <div
              style={{
                color: "#00FF41",
                fontSize: "11px",
                textTransform: "uppercase",
                letterSpacing: "1.6px",
                fontWeight: "bold",
                marginBottom: "4px",
              }}
            >
              Interview Engine
            </div>
            <div style={{ color: "#747674", fontSize: "10px" }}>v2.4.0-stable</div>
          </div>

          <div style={{ display: "flex", flexDirection: "column", gap: "8px" }}>
            <div style={sidebarItemStyle(false)}>▣ Dashboard</div>
            <div style={sidebarItemStyle(true)}>▣ Interviews</div>
            <div style={sidebarItemStyle(false)}>▣ Skills</div>
            <div style={sidebarItemStyle(false)}>▣ Settings</div>
            <div style={sidebarItemStyle(false)}>▣ Support</div>
          </div>

          <div style={{ marginTop: "auto", padding: "12px 8px 4px 8px" }}>
            <button
              style={{
                width: "100%",
                padding: "14px",
                background: "#00fdbc",
                color: "#004732",
                border: "none",
                fontWeight: "bold",
                fontSize: "12px",
                letterSpacing: "1px",
                cursor: "pointer",
                marginBottom: "16px",
              }}
            >
              NEW SIMULATION
            </button>
            <div style={{ color: "#747674", padding: "8px 6px", fontSize: "12px" }}>↩ Logout</div>
          </div>
        </aside>

        <main
          style={{
            flex: 1,
            padding: "34px 32px",
          }}
        >
          <div style={{ maxWidth: "1280px" }}>
            <header style={{ marginBottom: "30px", maxWidth: "860px" }}>
              <h1
                style={{
                  fontSize: "56px",
                  margin: "0 0 10px 0",
                  fontWeight: "bold",
                  lineHeight: 1.05,
                }}
              >
                Simulation <span style={{ color: "#00fdbc" }}>Parameters</span>
              </h1>
              <p style={{ color: "#aaaba9", fontSize: "16px", lineHeight: 1.7, margin: 0 }}>
                Configure your environment for the optimal stress-test. InterSim
                adapts its algorithmic difficulty based on these initial seeds.
              </p>
            </header>

            <div
              style={{
                display: "grid",
                gridTemplateColumns: "minmax(0, 1fr) 320px",
                gap: "28px",
                alignItems: "start",
              }}
            >
              <div style={{ display: "flex", flexDirection: "column", gap: "30px" }}>
                <section>
                  <div
                    style={{
                      display: "flex",
                      alignItems: "center",
                      gap: "10px",
                      marginBottom: "16px",
                    }}
                  >
                    <div style={{ width: "28px", height: "2px", background: "#00FF41" }} />
                    <h3 style={{ margin: 0, fontSize: "18px", textTransform: "uppercase" }}>
                      Technical Domain
                    </h3>
                  </div>

                  <div
                    style={{
                      display: "grid",
                      gridTemplateColumns: "repeat(2, minmax(0, 1fr))",
                      gap: "14px",
                    }}
                  >
                    <div
                      style={domainCardStyle(domain === "Frontend")}
                      onClick={() => setDomain("Frontend")}
                    >
                      <div style={{ display: "flex", justifyContent: "space-between" }}>
                        <span style={{ color: "#acffd9", fontSize: "20px" }}>🖥</span>
                        <span style={{ color: "#747674", fontSize: "9px", letterSpacing: "1px" }}>
                          DOM_ENGINE
                        </span>
                      </div>
                      <div>
                        <h4 style={{ margin: "0 0 8px 0", fontSize: "20px" }}>Frontend</h4>
                        <p style={{ margin: 0, color: "#aaaba9", fontSize: "12px", lineHeight: 1.6 }}>
                          Focus on React, performance optimization, and UI architecture patterns.
                        </p>
                      </div>
                    </div>

                    <div
                      style={domainCardStyle(domain === "Backend")}
                      onClick={() => setDomain("Backend")}
                    >
                      <div style={{ display: "flex", justifyContent: "space-between" }}>
                        <span style={{ color: "#acffd9", fontSize: "20px" }}>🗄</span>
                        <span style={{ color: "#747674", fontSize: "9px", letterSpacing: "1px" }}>
                          LOGIC_CORE
                        </span>
                      </div>
                      <div>
                        <h4 style={{ margin: "0 0 8px 0", fontSize: "20px" }}>Backend</h4>
                        <p style={{ margin: 0, color: "#aaaba9", fontSize: "12px", lineHeight: 1.6 }}>
                          Systems design, concurrency, distributed architecture, and API efficiency.
                        </p>
                      </div>
                    </div>

                    <div
                      style={domainCardStyle(domain === "DevOps")}
                      onClick={() => setDomain("DevOps")}
                    >
                      <div style={{ display: "flex", justifyContent: "space-between" }}>
                        <span style={{ color: "#acffd9", fontSize: "20px" }}>☁</span>
                        <span style={{ color: "#747674", fontSize: "9px", letterSpacing: "1px" }}>
                          INFRA_STRAT
                        </span>
                      </div>
                      <div>
                        <h4 style={{ margin: "0 0 8px 0", fontSize: "20px" }}>DevOps</h4>
                        <p style={{ margin: 0, color: "#aaaba9", fontSize: "12px", lineHeight: 1.6 }}>
                          CI/CD pipelines, observability, Kubernetes, and security at scale.
                        </p>
                      </div>
                    </div>

                    <div
                      style={domainCardStyle(domain === "Data Science")}
                      onClick={() => setDomain("Data Science")}
                    >
                      <div style={{ display: "flex", justifyContent: "space-between" }}>
                        <span style={{ color: "#acffd9", fontSize: "20px" }}>📈</span>
                        <span style={{ color: "#747674", fontSize: "9px", letterSpacing: "1px" }}>
                          DATA_MINER
                        </span>
                      </div>
                      <div>
                        <h4 style={{ margin: "0 0 8px 0", fontSize: "20px" }}>Data Science</h4>
                        <p style={{ margin: 0, color: "#aaaba9", fontSize: "12px", lineHeight: 1.6 }}>
                          Model evaluation, statistical analysis, and algorithmic optimization.
                        </p>
                      </div>
                    </div>
                  </div>
                </section>

                <div
                  style={{
                    display: "grid",
                    gridTemplateColumns: "1fr 1fr",
                    gap: "24px",
                  }}
                >
                  <section>
                    <div
                      style={{
                        display: "flex",
                        alignItems: "center",
                        gap: "10px",
                        marginBottom: "16px",
                      }}
                    >
                      <div style={{ width: "28px", height: "2px", background: "#81ecff" }} />
                      <h3 style={{ margin: 0, fontSize: "18px", textTransform: "uppercase" }}>
                        Expertise Level
                      </h3>
                    </div>

                    <div style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
                      <button
                        style={levelButtonStyle(level === "Junior / Entry")}
                        onClick={() => setLevel("Junior / Entry")}
                      >
                        <span>Junior / Entry</span>
                        <span>{level === "Junior / Entry" ? "◉" : "○"}</span>
                      </button>

                      <button
                        style={levelButtonStyle(level === "Senior / Lead")}
                        onClick={() => setLevel("Senior / Lead")}
                      >
                        <span>Senior / Lead</span>
                        <span>{level === "Senior / Lead" ? "◉" : "○"}</span>
                      </button>

                      <button
                        style={levelButtonStyle(level === "Staff+ Architect")}
                        onClick={() => setLevel("Staff+ Architect")}
                      >
                        <span>Staff+ Architect</span>
                        <span>{level === "Staff+ Architect" ? "◉" : "○"}</span>
                      </button>
                    </div>
                  </section>

                  <section>
                    <div
                      style={{
                        display: "flex",
                        alignItems: "center",
                        gap: "10px",
                        marginBottom: "16px",
                      }}
                    >
                      <div style={{ width: "28px", height: "2px", background: "#b7efc9" }} />
                      <h3 style={{ margin: 0, fontSize: "18px", textTransform: "uppercase" }}>
                        Session Type
                      </h3>
                    </div>

                    <div style={{ background: "#000000", padding: "4px", display: "flex", gap: "4px" }}>
                      <button
                        style={sessionButtonStyle(sessionType === "Technical")}
                        onClick={() => setSessionType("Technical")}
                      >
                        Technical
                      </button>
                      <button
                        style={sessionButtonStyle(sessionType === "Behavioral")}
                        onClick={() => setSessionType("Behavioral")}
                      >
                        Behavioral
                      </button>
                      <button
                        style={sessionButtonStyle(sessionType === "Hybrid")}
                        onClick={() => setSessionType("Hybrid")}
                      >
                        Hybrid
                      </button>
                    </div>

                    <div
                      style={{
                        marginTop: "16px",
                        border: "1px solid rgba(71,72,71,0.25)",
                        padding: "16px",
                        color: "#aaaba9",
                        fontSize: "12px",
                        lineHeight: 1.7,
                        background: "#121413",
                      }}
                    >
                      <div
                        style={{
                          color: "#81ecff",
                          fontSize: "10px",
                          letterSpacing: "1.4px",
                          textTransform: "uppercase",
                          marginBottom: "8px",
                        }}
                      >
                        Simulation Focus
                      </div>
                      Technical sessions emphasize coding speed, algorithmic precision,
                      and architectural reasoning under pressure.
                    </div>
                  </section>
                </div>
              </div>

              <div style={{ position: "sticky", top: "96px" }}>
                <div
                  style={{
                    background: "rgba(36, 38, 37, 0.42)",
                    border: "1px solid rgba(71,72,71,0.25)",
                    padding: "24px",
                    boxShadow: "0 18px 40px rgba(0,0,0,0.28)",
                  }}
                >
                  <div
                    style={{
                      display: "flex",
                      justifyContent: "space-between",
                      alignItems: "center",
                      marginBottom: "20px",
                    }}
                  >
                    <h3 style={{ margin: 0, fontSize: "18px", textTransform: "uppercase" }}>
                      Diagnostics
                    </h3>
                    <span style={{ color: "#00FF41" }}>●</span>
                  </div>

                  <div style={{ display: "flex", flexDirection: "column", gap: "16px", marginBottom: "22px" }}>
                    <div style={{ display: "flex", justifyContent: "space-between", fontSize: "12px" }}>
                      <span style={{ color: "#aaaba9" }}>Audio Input</span>
                      <span style={{ color: "#00FF41" }}>READY</span>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", fontSize: "12px" }}>
                      <span style={{ color: "#aaaba9" }}>Video Feed</span>
                      <span style={{ color: "#00FF41" }}>READY</span>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", fontSize: "12px" }}>
                      <span style={{ color: "#aaaba9" }}>Network Latency</span>
                      <span style={{ color: "#81ecff" }}>14ms</span>
                    </div>
                    <div style={{ height: "4px", background: "#121413", overflow: "hidden" }}>
                      <div style={{ width: "74%", height: "100%", background: "#81ecff" }} />
                    </div>
                  </div>

                  <div
                    style={{
                      height: "150px",
                      background:
                        "radial-gradient(circle at center, rgba(0,255,65,0.16), rgba(13,15,14,1) 70%)",
                      border: "1px solid #242625",
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "center",
                      marginBottom: "22px",
                    }}
                  >
                    <div style={{ textAlign: "center" }}>
                      <div style={{ color: "rgba(247,246,244,0.12)", fontSize: "28px", fontWeight: "bold" }}>
                        01001001
                      </div>
                      <div
                        style={{
                          color: "rgba(0,255,65,0.45)",
                          fontSize: "10px",
                          letterSpacing: "2px",
                          textTransform: "uppercase",
                        }}
                      >
                        Establishing Secure Link
                      </div>
                    </div>
                  </div>

                  <button
                    onClick={() => navigate("/behavioral")}
                    style={{
                      width: "100%",
                      padding: "16px",
                      background: "linear-gradient(135deg, #acffd9, #00fdbc)",
                      color: "#004732",
                      border: "none",
                      fontWeight: "bold",
                      letterSpacing: "1px",
                      cursor: "pointer",
                      marginBottom: "18px",
                    }}
                  >
                    INITIATE SIMULATION
                  </button>

                  <div style={{ color: "#747674", fontSize: "11px", lineHeight: 1.6 }}>
                    Your session will be recorded for local analysis. Data is processed
                    securely inside the simulation environment.
                  </div>
                </div>
              </div>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}