using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class VisiteController : MonoBehaviour
{
    [SerializeField] private WorkshopManager WorkshopManager;
    public void OnAction(CallbackContext context)
    {
        Debug.Log("action !");
        if (!context.performed) return;
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2,
       ~LayerMask.NameToLayer("POI")))
        {
            var poi = hit.collider.gameObject.GetComponentInParent<PointOfInterest>();
            WorkshopManager.DisplayPOI(poi.PointOfInterestData);
        }
    }
}
