using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class MaskObject : MonoBehaviour
    {
        void Start()
        {
            GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
}
