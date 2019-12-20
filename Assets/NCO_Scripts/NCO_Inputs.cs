﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using HumanManagement;
using WorldManagement;

namespace Inputs
{
    public class NCO_Inputs : MonoBehaviour
    {

        Touch touch;
        public LayerMask humanLayer;

        RaycastHit hit;
        GameObject lastHumanGrabbed = null;

        void Start()
        {

        }

        void Update()
        {
            if (Input.touches.Length == 0)
                return;

            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f, humanLayer))
                {
                    lastHumanGrabbed = hit.transform.gameObject;
                    hit.transform.parent = Camera.main.transform;
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    Debug.Log("Begin Touch");
                }else
                {
                    lastHumanGrabbed = null;
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                if (lastHumanGrabbed != null)
                {
                    lastHumanGrabbed.transform.parent = null;
                    lastHumanGrabbed.GetComponent<Rigidbody>().useGravity = true;
                    Debug.Log("End Touch");
                }
                

            }
        }
    }

}