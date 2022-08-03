using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitch : MonoBehaviour
{
    ARFaceManager arFaceManger;

    public Material[] materials;

    private int switchCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        arFaceManger = GetComponent<ARFaceManager>();
        arFaceManger.facePrefab.GetComponent<MeshRenderer>().material = materials[0];
    }

    void SwitchFaces()
    {
        foreach (ARFace face in arFaceManger.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[switchCount];
        }

        switchCount = (switchCount + 1) % materials.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SwitchFaces();
        }
    }
}
