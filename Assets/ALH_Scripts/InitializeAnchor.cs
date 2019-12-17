using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System.Threading.Tasks;
//using GoogleARCore.Examples.Common;

public class InitializeAnchor : MonoBehaviour
{
    public GameObject m_drone;

    private bool m_floorIsInit = false;
    private List<DetectedPlane> m_detectedPlanes = new List<DetectedPlane>();



    void Start()
    {
        m_floorIsInit = false;
    }

    private void Update()
    {
        if (m_floorIsInit)
            return;

        //On récupère les planes qui ont été détectés
        Session.GetTrackables<DetectedPlane>(m_detectedPlanes, TrackableQueryFilter.All);

        
        if (m_detectedPlanes.Count > 0)
        {
            //Check for horizontal plane : itération
            foreach (DetectedPlane plane in m_detectedPlanes)
            {
                if (plane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                {
                    
                    Pose pose = new Pose(Camera.main.transform.position + GetFlatHorizontalDirection(Camera.main.transform.forward) * 1.25f, Quaternion.identity);

                    //Creation de l'ancre
                    Anchor anchor = plane.CreateAnchor(pose);

                    //Assigne à notre objet la position et on le parente à l'ancre
                    m_drone.transform.position = pose.position;
                    m_drone.transform.SetParent(anchor.transform);

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



    private void TestCoroutineWithCallback ()
    {
        string path = "testpath";
        StartCoroutine(CoroutineMethod(path, CallbackMethod));
    }

    private void CallbackMethod (string result)
    {
        Debug.Log("Coroutine completed with result= " + result);
    }

    private IEnumerator CoroutineMethod (string path, System.Action<string> resultCallback)
    {
        yield return new WaitForSeconds(0.1f);
        resultCallback(path);
    }

    /*
    private async Task<Texture> GetTextureFromUrl (string url)
    {
        using (System.IO.FileStream reader = System.IO.File.OpenRead(url))
        {
            reader.ReadByte();
        }

        using (System.IO.StreamReader reader = System.IO.File.OpenText(url))
        {
           
        }

        byte[] bytes = System.IO.File.ReadAllBytes(url);
        Texture2D texture = new Texture2D(256, 256);
        texture.LoadRawTextureData(bytes);

    }*/

}
