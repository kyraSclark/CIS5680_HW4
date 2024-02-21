using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
	internal class GlobalClientState : MonoBehaviour
	{
		public GameObject timeUpUI;

		private bool gameOverTimer;
		public string gameOverText;
		private bool isClientReady;

		private void Start()
		{
			ResetTimer("INVALID");
		}

		public void ResetTimer(string gameOverDefaultText)
		{
			gameOverTimer = false;
			isClientReady = false;
			gameOverText = gameOverDefaultText;
			timeUpUI.SetActive(false);
		}

		public void ToggleClientReady()
		{
			isClientReady = !isClientReady;
		}

		public bool IsClientReady()
		{
			return isClientReady;
		}

		public void TimerEnded(string gameOverDueToTimerText)
		{
			gameOverTimer = true;
			gameOverText = gameOverDueToTimerText;
			timeUpUI.SetActive(true);
		}

		public bool IsGameOver()
		{
			return gameOverTimer;
		}
	}
}
