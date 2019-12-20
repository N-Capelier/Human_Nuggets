using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

namespace WorldManagement
{
    public class NCO_WorldBuilder : MonoBehaviour
    {
        private bool m_floorIsInit = false;

        private List<DetectedPlane> m_DetectedPlanes = new List<DetectedPlane>();

        [HideInInspector] public GameObject world;

        DetectedPlane originPlane;

        Pose pose;
        [HideInInspector] public Anchor anchor;

        [HideInInspector] public bool generationCompleted = false;

        void Start()
        {
            m_floorIsInit = false;

            StartCoroutine(WaitForPlane());
        }

        void Update()
        {
            if (!m_floorIsInit)
            {
                InitializeAnchor();
            }
        }

        IEnumerator WaitForPlane()
        {
            //Debug.Log("Looking for a plane...");
            int detectedPlanesNumber = 0;

            Session.GetTrackables<DetectedPlane>(m_DetectedPlanes, TrackableQueryFilter.All);

            foreach (DetectedPlane plane in m_DetectedPlanes)
            {
                detectedPlanesNumber++;
            }

            if (detectedPlanesNumber == 0)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine(WaitForPlane());
            }
            else
            {
                originPlane = m_DetectedPlanes[0];
                //Debug.Log("Found plane");
                StartCoroutine(WaitForSize(1f, 1f));
            }
        }

        IEnumerator WaitForSize(float xSize, float zSize)
        {
            //Debug.Log("Looking for a good size...");
            Session.GetTrackables<DetectedPlane>(m_DetectedPlanes, TrackableQueryFilter.All);
            /*foreach (DetectedPlane plane in m_DetectedPlanes)
            {*/
            DetectedPlane plane = originPlane;
            /*DetectedPlane parentPlane = plane.SubsumedBy;
            if(parentPlane != null)
            {
                plane = parentPlane;
            }*/
            //Debug.Log("xSize : " + plane.ExtentX + " ; zSize : " + plane.ExtentZ);
            if (plane.ExtentX >= xSize && plane.ExtentZ >= zSize)
            {
                Debug.Log("Found good size");
                world = (GameObject)Instantiate(Resources.Load("Prefabs/City"));
                world.transform.position = plane.CenterPose.position;
                world.transform.SetParent(anchor.transform);
                //m_drone.transform.position = pose.position;
                //m_drone.transform.SetParent(anchor.transform);
                generationCompleted = true;
            }
            else
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(1);
                StartCoroutine(WaitForSize(1f, 1f));
            }
            //}
        }

        void InitializeAnchor()
        {
            Session.GetTrackables<DetectedPlane>(m_DetectedPlanes, TrackableQueryFilter.All);

            if (m_DetectedPlanes.Count > 0)
            {
                //Check for horizontal plane : itération
                foreach (DetectedPlane plane in m_DetectedPlanes)
                {
                    if (plane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                    {

                        pose = new Pose(Camera.main.transform.position + GetFlatHorizontalDirection(Camera.main.transform.forward) * 1.25f, Quaternion.identity);

                        //Creation de l'ancre
                        anchor = plane.CreateAnchor(pose);

                        Debug.Log("new anchor created.");

                        m_floorIsInit = true;

                    }
                }
            }
        }

        private Vector3 GetFlatHorizontalDirection(Vector3 dir)
        {
            return new Vector3(dir.x, 0f, dir.z);
        }




    }
}
