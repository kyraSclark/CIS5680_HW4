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
            // the Collision contains a lot of info,
            // but it’s the colliding object we’re most
            // interested in.

            Collider collider = collision.collider;

            if (collider.CompareTag("Perry"))
            {
                Perry perry = collider.gameObject.GetComponent<Perry>();
                Debug.Log("collided with Perry");
                perry.Die();
                Destroy(gameObject);
            }
            else if (collider.CompareTag("Platypus"))
            {
                Platypus plat = collider.gameObject.GetComponent<Platypus>();
                plat.Die();
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
