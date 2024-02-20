namespace MyFirstARGame
{
	using Photon.Pun;
	using UnityEngine;

	/// <summary>
	/// You can use this class to make RPC calls between the clients. It is already spawned on each client with networking capabilities.
	/// </summary>
    public class NetworkCommunication : MonoBehaviourPun
    {
        [SerializeField]
        private Scoreboard scoreboard;

        [SerializeField]
        private BulletManager bulletManager;

		public float timerMaxInSeconds;
        public bool enableTimer = false;

        private float timeLeft;

        public void IncrementScore()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + 1);
        }

        public void IncrementScore(int points)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, currentScore + points);
        }

        public void DecrementBullets()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentBullets = this.bulletManager.GetBullets(playerName);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, currentBullets - 1);
        }
        
        public void IncrementBullets(int b)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentBullets = this.bulletManager.GetBullets(playerName);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, currentBullets + b);
        }

        public void SetBullets()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            this.bulletManager.SetBullets(playerName, 15);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, 15);

        }

        public int GetBullets()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            return this.bulletManager.GetBullets(playerName);
        }

        [PunRPC]
        public void Network_SetPlayerScore(string playerName, int newScore)
        {
            Debug.Log($"Player {playerName} scored {newScore}!");
            this.scoreboard.SetScore(playerName, newScore);
        }

        [PunRPC]
        public void Network_SetPlayerBullets(string playerName, int newBullets)
        {
            Debug.Log("set " + playerName + " bullets " + newBullets.ToString());
            this.bulletManager.SetBullets(playerName, newBullets);
        }

		[PunRPC]
		public void Network_SetTimerLeftText(string text)
		{
            this.scoreboard.SetTimerText(text);
		}

		[PunRPC]
		public void Network_EndTimerOnAllClients(string text)
		{
			var globalState = GameObject.Find("GlobalState").GetComponent<GlobalClientState>();            
			globalState.TimerEnded(text);
		}

		public void UpdateForNewPlayer(Photon.Realtime.Player player)
        {
            var playerName = $"Player {player.ActorNumber}";
            Debug.Log("update for new player" + playerName);
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, 0);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, 15);
		}

        public string GetTimeLeft()
        {
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            return string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        void Start()
        {
            timeLeft = timerMaxInSeconds;
            enableTimer = true;
        }

        void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            if (enableTimer)
            {
                if (timeLeft > 0f)
                {
                    timeLeft -= Time.deltaTime;
                    this.photonView.RPC("Network_SetTimerLeftText", RpcTarget.All, GetTimeLeft());
				}
                else
                {
                    enableTimer = false;
                    int max;
                    string winner;
					string gameOverText = this.scoreboard.GetWinnerText();
					this.photonView.RPC("Network_SetTimerLeftText", RpcTarget.All, "Time is up!");
					this.photonView.RPC("Network_EndTimerOnAllClients", RpcTarget.All, gameOverText);
				}
            }
        }
    }

}