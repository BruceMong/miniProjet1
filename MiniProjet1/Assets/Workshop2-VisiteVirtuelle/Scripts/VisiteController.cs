using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class VisiteController : MonoBehaviour
{
    [SerializeField] private WorkshopManager WorkshopManager;
    [SerializeField] private RobotManager RobotManager;
    public void OnAction(CallbackContext context)
    {
        Debug.Log("action !");
        if (!context.performed) return;
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform;


        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("POI"))
            {
                Debug.Log("hit POI");
                var poi = hit.collider.gameObject.GetComponentInParent<PointOfInterest>();
                WorkshopManager.DisplayPOI(poi.PointOfInterestData);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MIR100"))

            {
                Debug.Log("hit MIR100");

                //var robot = hit.collider.gameObject.GetComponent<MIR100>();
                Debug.Log(RobotManager);
                RobotManager.Next();
            }
        }
    }
}
