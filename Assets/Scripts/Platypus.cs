using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Platypus : MonoBehaviour
    {
        public float timer;
        public float spawnPeriod;
		public float animDuration;
        public float offset;
		public int points;

		private Vector3 startPos;
		private Vector3 endPos;

		// Start is called before the first frame update
		void Start()
        {
            timer = 0;
            spawnPeriod = 1.5f;
        }

        public void SetPosition(Vector3 pos)
        {
			startPos = gameObject.transform.position + pos;
            startPos.y -= gameObject.transform.localScale.y + offset;// GetComponent<Renderer>().bounds.size.y + offset;
			endPos = gameObject.transform.position + pos;
            endPos.y += gameObject.transform.localScale.y - offset;//GetComponent<Renderer>().bounds.size.y + offset;
			StartCoroutine(ShowAnim(startPos, endPos));
            //gameObject.transform.position = pos;
        }

        // Update is called once per frame
        void Update()
        {
            // After timer seconds, despawn
            timer += Time.deltaTime;
            if (timer > spawnPeriod)
            {
                timer = 0;
                Die();
            }
        }

        public void Die()
        {
            var networkCommunication = FindObjectOfType<NetworkCommunication>();
            networkCommunication.IncrementScore(points);
			StartCoroutine(ShowAnim(endPos, startPos, true));
        }

        private IEnumerator ShowAnim(Vector3 startPos, Vector3 endPos, bool destroy = false)
        {
            gameObject.transform.position = startPos;
            float elapsed = 0f;
            while (elapsed < animDuration)
            {
                gameObject.transform.position = Vector3.Lerp(startPos, endPos, elapsed/animDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            if (destroy)
            {
                Destroy(gameObject);
            }
		}
    }
}
