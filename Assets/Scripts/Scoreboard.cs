using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Scoreboard : MonoBehaviour
{
    private Dictionary<string, int> scores;
    private string timerText;

    private void Start()
    {
        this.scores = new Dictionary<string, int>();
    }

    public void SetScore(string playerName, int score)
    {
        if (this.scores.ContainsKey(playerName))
        {
            this.scores[playerName] = score;
        }
        else
        {
            this.scores.Add(playerName, score);
        }
    }

    public int GetScore(string playerName)
    {
        if (this.scores.ContainsKey(playerName))
        {
            return this.scores[playerName];
        }
        else
        {
            return 0;
        }
    }

    public string GetWinnerText()
        {
            int max = -7;
            var winner = "";

            Debug.Log("total scores: " + scores.Count);
            foreach (var score in this.scores)
            {
                Debug.Log($"debugging final call : {score.Key}: {score.Value}");
                if (score.Value > max)
                {
                    max = score.Value;
                    winner = score.Key;
                }
            }
            Debug.Log($"{winner} wins with a score of {max}");
            return $"{winner} wins with a score of {max}";
        }

    public void SetTimerText(string timeLeftText)
    {
        this.timerText = timeLeftText;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        foreach (var score in this.scores)
        {
            if (score.Key != "Player 1")
            {
                GUILayout.Label($"{score.Key}: {score.Value}", new GUIStyle { normal = new GUIStyleState { textColor = Color.black }, fontSize = 22});
            }
        }
		GUILayout.Label(timerText, new GUIStyle { normal = new GUIStyleState { textColor = Color.red }, fontSize = 30 });

		GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
