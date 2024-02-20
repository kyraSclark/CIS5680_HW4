using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    using Photon.Pun;

    public class GridSpawn : MonoBehaviour
    {
        public GameObject perryObjToSpawn;
        public GameObject platypusObjToSpawn;
        public GameObject doofPrefab;
        public GameObject maskPrefab;
        public float spawnPeriod;
        public int gridDimension;
        public float gridSideLength;

		private float timer;
		private List<Vector3> spawnPos = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {
            timer = 0;

            //spawning platypi over a grid centered at the origin
            float maxCoordinate = (gridDimension % 2 == 0) ? (gridDimension / 2f - 0.5f) * gridSideLength : (gridDimension - 1f) / 2f * gridSideLength;

            for (float x = -maxCoordinate; x <= maxCoordinate; x += gridSideLength)
            {
                for (float z = -maxCoordinate; z <= maxCoordinate; z += gridSideLength)
                {
					spawnPos.Add(new Vector3(x, 0f, z));
				}

			}
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer > spawnPeriod)
            {
                timer = 0;

                int perryPos = spawnPos.Count + 2; // set to impossible index first
                // Chance for Perry to spawn
                int r = Random.Range(1, 20);
                if (r < Doof.Instance.getChance())
                {
                    GameObject perry =
                        PhotonNetwork.Instantiate(
							perryObjToSpawn.name,
                            Vector3.zero,
							perryObjToSpawn.transform.rotation);

                    // Choose random grid postion index  
                    perryPos = Random.Range(0, gridDimension * gridDimension - 1);
					perry.GetComponent<Platypus>().SetPosition(spawnPos[perryPos]);
				}

                bool spawnedDoof = false; // Only spawn one doof at a time
                for (int i = 0; i < spawnPos.Count; i++)
                {
                    //spawn mask under each grid cell
                    Vector3 maskLoc = spawnPos[i];
                    maskLoc.y -= maskPrefab.transform.localScale.y / 2f;
					PhotonNetwork.Instantiate(maskPrefab.name, maskLoc, Quaternion.identity);

					// Do not spawn on Perry grid position
					if (i != perryPos)
                    {
                        // Random number to spawn platypus
                        r = Random.Range(1, 3);
                        if (r < 2)
                        {
                            string name = platypusObjToSpawn.name;
                            int d = Random.Range(1, 8);
                            if (!spawnedDoof && d < 2)
                            {
                                spawnedDoof = true;
                                name = doofPrefab.name;
                            }
                             
                            // Spawn platypus
                            GameObject platypus = 
                                PhotonNetwork.Instantiate(
                                    name, 
                                    Vector3.zero,
									platypusObjToSpawn.transform.rotation);

                            // Set platypus position
                            platypus.GetComponent<Platypus>().SetPosition(spawnPos[i]);
                        }
                    }
                }
            }
        }
    }
}
