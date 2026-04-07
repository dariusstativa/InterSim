import json
from pathlib import Path
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import Ridge
from sklearn.metrics import mean_absolute_error, r2_score

base_dir = Path(__file__).resolve().parent
json_path = base_dir.parent / "InterSim.Api" / "behavioral_training.json"

with open(json_path, "r", encoding="utf-8") as f:
    data = json.load(f)


df = pd.json_normalize(data)

print("Columns:")
print(df.columns.tolist())

features = [
    "Features.SimQA",
    "Features.MaxChunkSim",
    "Features.AvgTopChunkSim",
    "Features.Situation",
    "Features.Task",
    "Features.Action",
    "Features.Result",
    "Features.Reflection",
    "Features.WordCount",
    "Features.ActionVerbHits",
    "Features.HasFirstPerson",
    "Features.HasMetrics"
]

X = df[features]
y = df["TargetTotalScore"]

X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.2, random_state=42
)

model = Ridge(alpha=1.0)
model.fit(X_train, y_train)

pred = model.predict(X_test)

print("MAE:", mean_absolute_error(y_test, pred))
print("R2:", r2_score(y_test, pred))

print("\nCoefficients:")
for f, c in zip(features, model.coef_):
    print(f"{f}: {c}")

print("\nIntercept:", model.intercept_)