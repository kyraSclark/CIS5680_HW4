using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Doof : MonoBehaviour
    {
        public static Doof Instance { get; private set; }

        public int perryChance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            DontDestroyOnLoad(gameObject);

            if (Instance != null && Instance != this)
            {

            }
            else
            {
                Instance = this;
                perryChance = 11;
            }
        }

        public void increaseChance()
        {
            perryChance += 1;
        }

        public int getChance()
        {
            return perryChance;
        }

        public void resetChance()
        {
            perryChance = 11;
        }
    }
}
