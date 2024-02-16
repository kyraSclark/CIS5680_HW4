using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    internal class BulletManager : MonoBehaviour
    {
        private Dictionary<string, int> bullets;

        private void Start()
        {
            this.bullets = new Dictionary<string, int>();
        }

        public void SetBullets(string playerName, int num)
        {
            if (this.bullets.ContainsKey(playerName))
            {
                this.bullets[playerName] = num;
            }
            else
            {
                this.bullets.Add(playerName, num);
            }
        }

        public int GetBullets(string playerName)
        {
            if (this.bullets.ContainsKey(playerName))
            {
                return this.bullets[playerName];
            }
            else
            {
                return 0;
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach (var num in this.bullets)
            {
                GUILayout.Label($"{num.Key}: {num.Value}", new GUIStyle { normal = new GUIStyleState { textColor = Color.black }, fontSize = 22 });
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
