using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldManagement;

namespace HumanManagement
{
    public class NCO_NuggetsSpawnerBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject nugget;
        void Start()
        {

        }

        void Update()
        {

        }

        private void OnEnable()
        {
            Instantiate(nugget, gameObject.transform);
            gameObject.SetActive(false);
        }
    }

}