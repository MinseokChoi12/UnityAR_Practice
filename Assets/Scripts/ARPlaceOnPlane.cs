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

    // ȭ�鿡 ���� �κп� ������Ʈ�� �� �����Ӹ��� ��ġ��Ű�� �Լ�
    private void UpdateCenterObject()
    {
        // ViewportTOScreenPoint
        // �Է¹��� ����Ʈ�� viewport ��ǥ���� screen ��ǥ�� ��ȯ
        // Camera screen�� center ������ �޾ƿ��� �Լ�
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // (Ray�� ��� �� ������, Ray�� ���� ��ü�� ����Ʈ�� ��ȯ, trackabletype ����)
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose; // ray�� ���� ù��° ��ü�� 3d object�� ��ġ�Ұ���
            placeObject.SetActive(true); // �Ⱥ��̴� ��ü�� ���� ���̰Բ� Ȱ��ȭ��
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation); // ��ü�� ��ġ ����
        }
        //else
        //{
        //    placeObject.SetActive(false);
        //}
    }

    private void PlaceObjectByTouch()
    {
        if (Input.touchCount > 0) // ȭ�鿡 ��ġ�Ǿ� �ִ� �հ����� ���� > 0
        {
            Touch touch = Input.GetTouch(0); // ù���� ��ġ�� �Ͼ ��ġ

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                if(!spawnObject) // dummy�� �ѹ��� �����ǵ���
                {
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation); // ȭ��󿡼� ��ġ�� �� ������ �� ������ object�� ��ġ��Ŵ
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
