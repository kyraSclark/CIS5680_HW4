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

		private void Start()
		{
			gameOverTimer = false;
			gameOverText = "INVALID";
		}

		public void TimerEnded(string gameOverDueToTimerText)
		{
			gameOverTimer = true;
			gameOverText = gameOverDueToTimerText;
			Time.timeScale = 0;
			timeUpUI.SetActive(true);
		}
	}
}
