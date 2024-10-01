using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public GameObject[] furniturePrefabs;
    private GameObject selectedFurniture;
    private ARRaycastManager raycastManager;

    public void SelectFurniture(int index)
    {
        if (index >= 0 && index < furniturePrefabs.Length)
        {
            selectedFurniture = furniturePrefabs[index];
        }
    }

    public void PlaceFurniture(Vector3 position, Quaternion rotation)
    {
        if (selectedFurniture != null)
        {
            Instantiate(selectedFurniture, position, rotation);
        }
    }


    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    PlaceFurniture(hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
