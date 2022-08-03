using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;

    GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObjectByTouch();
        //UpdateCenterObject();
    }

    // 화면에 센터 부분에 오브젝트를 매 프레임마다 위치시키는 함수
    private void UpdateCenterObject()
    {
        // ViewportTOScreenPoint
        // 입력받은 포인트를 viewport 좌표에서 screen 좌표로 변환
        // Camera screen의 center 지점을 받아오는 함수
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // (Ray를 어디에 쏠 것인지, Ray가 닿은 객체를 리스트로 반환, trackabletype 결정)
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose; // ray에 닿은 첫번째 객체에 3d object를 위치할거임
            placeObject.SetActive(true); // 안보이던 객체를 눈에 보이게끔 활성화함
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation); // 객체의 위치 설정
        }
        //else
        //{
        //    placeObject.SetActive(false);
        //}
    }

    private void PlaceObjectByTouch()
    {
        if (Input.touchCount > 0) // 화면에 터치되어 있는 손가락의 갯수 > 0
        {
            Touch touch = Input.GetTouch(0); // 첫번쨰 터치가 일어난 위치

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                if(!spawnObject) // dummy가 한번만 생성되도록
                {
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation); // 화면상에서 터치가 될 때마다 그 지점에 object를 위치시킴
                }
                else
                {
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
