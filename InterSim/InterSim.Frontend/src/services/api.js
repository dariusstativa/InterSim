const API_BASE = "http://localhost:5292";

export async function evaluateWithFollowUpRule(payload) {
  const response = await fetch(`${API_BASE}/interviews/evaluate-with-followup-rule`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    throw new Error("Failed to evaluate answer.");
  }

  return response.json();
}