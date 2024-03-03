using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
   GameObject objSelected = null;
   public GameObject[] snapPoints;
   private float snapSensitivity = 2.0f;
   public GameObject craftedObjectPrefab;

   void Update()
   {
        if(Input.GetMouseButtonDown(0))
        {
            CheckHitObject();
        }
        if(Input.GetMouseButton(0) && objSelected != null)
        {
            DragObject();
        }
        if(Input.GetMouseButtonUp(0) && objSelected != null)
        {
            Dropobject();
        }
   }

   void CheckHitObject()
   {
        if(objSelected == null )
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                objSelected = hit.transform.gameObject;
            }
        }
   }

   void DragObject()
   {
    objSelected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 20.0f));
   }

   void Dropobject()
   {
    bool crafted = false;
    for(int i = 0; i < snapPoints.Length; i++)
    {
        if(Vector3.Distance(snapPoints[i].transform.position, objSelected.transform.position) < snapSensitivity)
        {
            GameObject craftedObject = Instantiate(craftedObjectPrefab, snapPoints[i].transform.position, Quaternion.identity);
            crafted = true;

            Destroy(objSelected);
            objSelected = null;
            break;
        }
    }
    if (!crafted)
    {
        objSelected = null;
    }
   }
} 
