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

		public AudioClip perryGrowl;
		public AudioClip platypusScream;
		public AudioClip doofJingle;

		public float timerMaxInSeconds;
        public bool enableTimer;

        private float timeLeft;
        private bool gameOver;
        private int STARTING_SCORE = 0;
        private int STARTING_BULLETS = 15;

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
            if(points < 0)
                AudioSource.PlayClipAtPoint(perryGrowl, gameObject.transform.position);
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
            if(b>1)
				AudioSource.PlayClipAtPoint(doofJingle, gameObject.transform.position);
            else
				AudioSource.PlayClipAtPoint(platypusScream, gameObject.transform.position);
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

        public void SetStatus(bool status)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            this.photonView.RPC("Network_SetPlayerStatus", RpcTarget.All, playerName, status);
        }

        [PunRPC]
        public void Network_ResetScoreBoard(int initScore)
        {
            this.scoreboard.ResetScoreBoard(initScore);
			this.timeLeft = timerMaxInSeconds;
			this.gameOver = false;
		}

        [PunRPC]
        public void Network_ResetBullets(int initBulletCount)
        {
            this.bulletManager.ResetBulletManager(initBulletCount);
        }

        [PunRPC]
        public void Network_SetPlayerScore(string playerName, int newScore)
        {
            Debug.Log($"Player {playerName} scored {newScore}!");
            this.scoreboard.SetScore(playerName, newScore);
        }

        [PunRPC]
        public void Network_SetPlayerStatus(string playerName, bool status)
        {
            this.scoreboard.ClientSignalReady(playerName, status);
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
        public void Network_ResetTimerOnAllClients(string text)
        {
            var globalState = GameObject.Find("GlobalState").GetComponent<GlobalClientState>();
            globalState.ResetTimer(text);
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
            this.photonView.RPC("Network_SetPlayerScore", RpcTarget.All, playerName, STARTING_SCORE);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, STARTING_BULLETS);
            this.photonView.RPC("Network_SetPlayerStatus", RpcTarget.All, playerName, false);
        }

        public string GetTimeLeft()
        {
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            return string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        public void ResetGameState()
        {
            timeLeft = timerMaxInSeconds;
            this.photonView.RPC("Network_ResetScoreBoard", RpcTarget.All, STARTING_SCORE);
            this.photonView.RPC("Network_ResetBullets", RpcTarget.All, STARTING_BULLETS); //resets both scores and client ready status
            this.photonView.RPC("Network_ResetTimerOnAllClients", RpcTarget.All, "INVALID");
			this.photonView.RPC("Network_SetTimerLeftText", RpcTarget.All, GetTimeLeft());
		}

        public bool areAllClientsReady()
        {
            return this.scoreboard.areAllClientsReady();
		}

        void Start()
        {
            timeLeft = timerMaxInSeconds;
            gameOver = false;
        }

        void Update()
        {            
			if (!PhotonNetwork.IsMasterClient)
                return;

			enableTimer = this.scoreboard.areAllClientsReady() && !gameOver;

			if (enableTimer)
            {
                if (timeLeft > 0f)
                {
                    timeLeft -= Time.deltaTime;
                    this.photonView.RPC("Network_SetTimerLeftText", RpcTarget.All, GetTimeLeft());
				}
                else
                {
					gameOver = true;
					Debug.Log($"I reset it back hehehehe");
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