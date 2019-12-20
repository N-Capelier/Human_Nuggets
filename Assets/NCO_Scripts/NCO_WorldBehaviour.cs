using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldManagement
{
    public class NCO_WorldBehaviour : MonoBehaviour
    {
        public List<GameObject> spawnPoints = new List<GameObject>();

        private void Start()
        {
            GameObject.Find("World Manager").GetComponent<NCO_HumansManager>().spawnPoints = spawnPoints;
        }
    }
}