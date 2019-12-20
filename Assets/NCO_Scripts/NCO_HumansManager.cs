using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using HumanManagement;

namespace WorldManagement
{
    public class NCO_HumansManager : MonoBehaviour
    {

        bool spawnStarted = false;

        public List<GameObject> spawnPoints;

        [SerializeField] GameObject[] humans;

        int globalDifficulty;

        void Start()
        {

        }

        void Update()
        {
            if(gameObject.GetComponent<NCO_WorldBuilder>().generationCompleted == true && spawnStarted == false)
            {
                spawnStarted = true;
                spawnPoints = GetComponent<NCO_WorldBuilder>().world.GetComponent<NCO_WorldBehaviour>().spawnPoints;
                globalDifficulty = GetComponent<NCO_GameManager>().globalDifficulty;
                StartCoroutine(SpawnHuman(globalDifficulty));
            }
        }

        IEnumerator SpawnHuman(int difficulty)
        {
            int ran = Random.Range(0, spawnPoints.Count);
            GameObject human = spawnPoints[ran].GetComponent<NCO_SpawnerBehaviour>().SpawnHuman();
            human.transform.SetParent(GetComponent<NCO_WorldBuilder>().anchor.gameObject.transform);
            yield return new WaitForSeconds((float)difficulty - (float)difficulty * 0.5f);
            globalDifficulty = GetComponent<NCO_GameManager>().globalDifficulty;
            StartCoroutine(SpawnHuman(globalDifficulty));


        }

        public GameObject GetRandomHuman()
        {
            int ran = Random.Range(0, humans.Length);
            GameObject humanToSpawn = humans[ran];
            return humanToSpawn;
        }
    }

}