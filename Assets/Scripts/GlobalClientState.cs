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
			SetNetworkClientReadyStatus(isClientReady);
		}

		public bool IsClientReady()
		{
			return isClientReady;
		}

		private void SetNetworkClientReadyStatus(bool status)
		{
			var networkCommunication = FindObjectOfType<MyFirstARGame.NetworkCommunication>();
			networkCommunication.SetStatus(status);
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

		public bool AreaAllClientsReady()
		{
			var networkCommunication = FindObjectOfType<MyFirstARGame.NetworkCommunication>();
			return networkCommunication.areAllClientsReady();
		}
	}
}
