namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;

    /// <summary>
    /// Controls projectile behaviour. In our case it currently only changes the material of the projectile based on the player that owns it.
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Material[] projectileMaterials;

        private void Awake()
        {
            // Pick a material based on our player number so that we can distinguish between projectiles. We use the player number
            // but wrap around if we have more players than materials. This number was passed to us when the projectile was instantiated.
            // See ProjectileLauncher.cs for more details.
            var photonView = this.transform.GetComponent<PhotonView>();
            var playerId = Mathf.Max((int)photonView.InstantiationData[0], 0);
            if (this.projectileMaterials.Length > 0)
            {
                var material = this.projectileMaterials[playerId % this.projectileMaterials.Length];
                this.transform.GetComponent<Renderer>().material = material;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collided");
            Collider collider = collision.collider;

            if (collider.CompareTag("Perry"))
            {
                // Perry gives player 1 bullet
                var networkCommunication = FindObjectOfType<NetworkCommunication>();
                networkCommunication.IncrementBullets(1);

                Platypus perry = collider.gameObject.GetComponent<Platypus>();
                networkCommunication.IncrementScore(perry.points); // add 5 points
                Debug.Log("collided with Perry");
                perry.Die();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Platypus"))
            {
                var networkCommunication = FindObjectOfType<NetworkCommunication>();
                networkCommunication.IncrementScore(plat.points); // subtract 1 point

                Debug.Log("collided with platypus");
                Platypus plat = collider.gameObject.GetComponent<Platypus>();
                plat.Die();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Doof"))
            {
                // Doof gives player 5 bullets
                var networkCommunication = FindObjectOfType<NetworkCommunication>();
                networkCommunication.IncrementBullets(5);

                // Increase Chance of Perry spawning
                Doof.Instance.increaseChance();

                Platypus doof = collider.gameObject.GetComponent<Platypus>();
                Debug.Log("collided with Doof");
                doof.Die();
                Destroy(gameObject);
            }
            else
            {
                // if we collided with something else, print to console 
                // what the other thing was
                Debug.Log("Collided with " + collider.tag);
            }
        }
    }
}
