using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldManagement;

namespace HumanManagement
{
    public class NCO_HumanBehaviour : MonoBehaviour
    {
        GameObject gameManager;
        GameObject nuggetSpawner;

        void Start()
        {
            gameManager = GameObject.Find("World Manager");
            nuggetSpawner = GameObject.Find("Nugget Spawner");
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "shredder_enter")
            {
                gameManager.GetComponent<NCO_GameManager>().score++;
                nuggetSpawner.SetActive(true);
                Destroy(gameObject);
            }
        }

    }

}