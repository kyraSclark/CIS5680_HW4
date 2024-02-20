using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
	internal class GlobalClientState : MonoBehaviour
	{
		public GameObject timeUpUI;

		private bool gameOverTimer;

		private void Start()
		{
			gameOverTimer = false;
		}

		public void TimerEnded()
		{
			gameOverTimer = true;
			Time.timeScale = 0;
			timeUpUI.SetActive(true);
		}
	}
}
