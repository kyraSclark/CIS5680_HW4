using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class Scoreboard : MonoBehaviour
{
    private Dictionary<string, int> scores;
    private Dictionary<string, bool> readyStat;
	private string timerText;

    private void Start()
    {
        this.scores = new Dictionary<string, int>();
		this.readyStat = new Dictionary<string, bool>();
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

    public void ClientSignalReady(string playerName, bool status)
    {
		if (this.readyStat.ContainsKey(playerName))
		{
			this.readyStat[playerName] = status;
		}
		else
		{
			this.readyStat.Add(playerName, status);
		}
	}

    public void ResetScoreBoard(int initScore)
    {
        scores = scores.ToDictionary(e => e.Key, e => initScore);
		readyStat = readyStat.ToDictionary(e => e.Key, e => false);
        if (readyStat.ContainsKey("Player 1"))
        {
            Debug.Log("defaulting server to true");
            readyStat["Player 1"] = true;
        }
	}

    public bool areAllClientsReady()
    {
		if(readyStat.Count == 0) return false;
        if ( readyStat.ContainsKey("Player 1")) readyStat["Player 1"] = true;

		bool allClientsReady = true;
		foreach (var status in this.readyStat)
		{
            //Debug.Log($"refs {status.Key} -> {status.Value}");
            allClientsReady &= status.Value;
		}
        return allClientsReady;
	}

    public string GetWinnerText()
        {
            int max = -7;
            var winner = "";

            foreach (var score in this.scores)
            {
                if (score.Value > max)
                {
                    max = score.Value;
                    winner = score.Key;
                }
            }
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
