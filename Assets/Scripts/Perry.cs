using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Perry : MonoBehaviour
    {
        public float timer;
        public float spawnPeriod;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0;
            spawnPeriod = 1.5f;
        }

        public void SetPosition(Vector3 pos)
        {
            gameObject.transform.position = pos;
        }

        // Update is called once per frame
        void Update()
        {
            // After timer seconds, despawn
            timer += Time.deltaTime;
            if (timer > spawnPeriod)
            {
                timer = 0;
                Destroy(gameObject);
            }
        }
    }
}
