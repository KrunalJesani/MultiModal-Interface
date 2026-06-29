using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RayPicking : MonoBehaviour
{
    public string RayCollisionLayer = "Default";
    public string RayUNCollisionLayer = "hitLayer";
    
    
    public RaycastHit lastRayCastHit;
    public RaycastHit finalRayCastHit;
    public GameObject pickedObject = null;

    private GameObject previousObjectCollidingWithRay = null;
    private GameObject lastObjectCollidingWithRay = null;
    private bool IsThereAnewObjectCollidingWithRay = false;

    void Start()
    {
        
    }

    void Update()
    {

        GetTargetedObjectCollidingWithRayCasting();
        UpdateObjectCollidingWithRay();
        UpdateFlagNewObjectCollidingWithRay();
        OutlineObjectCollidingWithRay();
        GetUNTargetedObjectCollidingWithRayCasting();
        
        if (lastObjectCollidingWithRay != null)
        {
            var outliner = lastObjectCollidingWithRay.GetComponent<OutlineModified>();
            if (outliner != null && pickedObject== null)
            {
                outliner.enabled = false;
            }
        }
    }

    private void GetTargetedObjectCollidingWithRayCasting()
    {
        if (Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit hit,
            Mathf.Infinity,
            1 << LayerMask.NameToLayer(RayCollisionLayer))) // 1 << because must use bit shifting to get final mask!
        {
            lastRayCastHit = hit;
            pickedObject = lastRayCastHit.collider.gameObject;
        }
         else
        {   
            pickedObject = null; // If raycast doesn't hit any object, set pickedObject to null
        }
    }

    private void GetUNTargetedObjectCollidingWithRayCasting()
    {
        if (Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit hit,
            Mathf.Infinity,
            1 << LayerMask.NameToLayer(RayUNCollisionLayer))) // 1 << because must use bit shifting to get final mask!
        {
            finalRayCastHit = hit;
        }
    }
    private void UpdateObjectCollidingWithRay()
    {
        if (lastRayCastHit.collider != null)
        {
            GameObject currentObjectCollidingWithRay = lastRayCastHit.collider.gameObject;
            if (lastObjectCollidingWithRay != currentObjectCollidingWithRay)
            {
                previousObjectCollidingWithRay = lastObjectCollidingWithRay;
                lastObjectCollidingWithRay = currentObjectCollidingWithRay;
            }
        }
    }

    private void UpdateFlagNewObjectCollidingWithRay()
    {
        if (lastObjectCollidingWithRay != previousObjectCollidingWithRay)
        {
            IsThereAnewObjectCollidingWithRay = true;
        }
        else
        {
            IsThereAnewObjectCollidingWithRay = false;
        }
    }

    private void OutlineObjectCollidingWithRay()
    {
        if (IsThereAnewObjectCollidingWithRay)
        {
            //add outline to new one
            if (lastObjectCollidingWithRay != null)
            {
                var outliner = lastObjectCollidingWithRay.GetComponent<OutlineModified>();
                if (outliner == null) // if not, we will add a component to be able to outline it
                {
                    outliner = lastObjectCollidingWithRay.AddComponent<OutlineModified>();
                }

                if (outliner != null)
                {
                    outliner.enabled = true;
                }
                // remove outline from previous one
                //add outline new one
                if (previousObjectCollidingWithRay != null)
                {
                    outliner = previousObjectCollidingWithRay.GetComponent<OutlineModified>();
                    if (outliner != null)
                    {
                        outliner.enabled = false;
                    }
                }
            }
        }
    }

   
}
