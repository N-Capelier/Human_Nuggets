using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldManagement;

namespace HumanManagement
{
    public class NCO_SpawnerBehaviour : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        public GameObject SpawnHuman()
        {
            GameObject humanToSpawn = GameObject.Find("World Manager").GetComponent<NCO_HumansManager>().GetRandomHuman();
            GameObject instance = (GameObject)Instantiate(humanToSpawn, gameObject.transform);
            return instance;
        }
    }

}