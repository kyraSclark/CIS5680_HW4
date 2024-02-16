using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class GridSpawn : MonoBehaviour
    {
        public GameObject perryObjToSpawn;
        public GameObject platypusObjToSpawn;
        public float timer;
        public float spawnPeriod;
        public Vector3 originInScreenCoords;
        private List<Vector3> spawnPos = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {
            timer = 0;
            spawnPeriod = 4.0f;
            originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
            
            // World space coordinates of 9x9 grid
            spawnPos.Add(new Vector3(-1.4f,0f,1.4f));
            spawnPos.Add(new Vector3(-1.4f,0f,0f));
            spawnPos.Add(new Vector3(-1.4f,0f,-1.4f));
            spawnPos.Add(new Vector3(0f,0f,1.4f));
            spawnPos.Add(new Vector3(0f,0f,0f));
            spawnPos.Add(new Vector3(0f,0f,-1.4f));
            spawnPos.Add(new Vector3(1.4f,0f,1.4f));
            spawnPos.Add(new Vector3(1.4f,0f,0f));
            spawnPos.Add(new Vector3(1.4f,0f,-1.4f));
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer > spawnPeriod)
            {
                // Spawn 9x9 grid of platypus 
                timer = 0;


                int perryPos = spawnPos.Count + 2; // set to impossible index first
                // Chance for Perry to spawn
                int r = Random.Range(1, 3);
                if (r < 2)
                {
                    GameObject perry = 
                        Instantiate(perryObjToSpawn,
                                new Vector3(originInScreenCoords.x, originInScreenCoords.y, originInScreenCoords.z),
                                Quaternion.identity);
                    // Choose random grid postion index  
                    perryPos = Random.Range(0, 8);
                    perry.GetComponent<Perry>().SetPosition(spawnPos[perryPos]);
                }

                for (int i = 0; i < spawnPos.Count; i++)
                {
                    // Do not spawn on Perry grid position
                    if (i != perryPos)
                    {
                        // Random number to spawn platypus
                        r = Random.Range(1, 3);
                        if (r < 2)
                        {
                            // Spawn platypus
                            GameObject platypus = 
                                Instantiate(platypusObjToSpawn,
                                        Camera.main.ScreenToWorldPoint(new
                                        Vector3(originInScreenCoords.x, originInScreenCoords.y, originInScreenCoords.z)),
                                        Quaternion.identity);
                            // Set platypus position
                            platypus.GetComponent<Platypus>().SetPosition(spawnPos[i]);
                        }
                    }
                }
            }
        }
    }
}
