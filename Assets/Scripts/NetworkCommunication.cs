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
        
        public void IncrementBullets()
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentBullets = this.bulletManager.GetBullets(playerName);
            this.photonView.RPC("Network_SetPlayerBullets", RpcTarget.All, playerName, currentBullets + 5);
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
            Debug.Log($"Player {playerName} scored!");
            this.scoreboard.SetScore(playerName, newScore);
        }

        [PunRPC]
        public void Network_SetPlayerBullets(string playerName, int newBullets)
        {
            this.bulletManager.SetBullets(playerName, newBullets);
        }

        public void UpdateForNewPlayer(Photon.Realtime.Player player)
        {
            var playerName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var currentScore = this.scoreboard.GetScore(playerName);
            this.photonView.RPC("Network_SetPlayerScore", player, playerName, currentScore);
            this.photonView.RPC("Network_SetPlayerBullets", player, playerName, 15);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}