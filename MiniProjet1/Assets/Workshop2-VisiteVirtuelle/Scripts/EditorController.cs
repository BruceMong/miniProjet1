using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class EditorController : MonoBehaviour
{
    [SerializeField] private EditorManager EditorManager;
    [SerializeField] private RobotManager RobotManager;


    public void OnAction(InputAction.CallbackContext context)

    {
        Debug.Log("Action Try");

        if (!context.performed) return;
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform;


        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("POI"))
            {
                Debug.Log("hit POI");

                var poi = hit.collider.gameObject.GetComponent<PointOfInterest>();
                Debug.Log(poi);

                EditorManager.DisplayPOI(poi.PointOfInterestData);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MIR100"))

            {
                Debug.Log("hit MIR100");

                //var robot = hit.collider.gameObject.GetComponent<MIR100>();
                RobotManager.Display();
            }
        }


        /*
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2, ~LayerMask.NameToLayer("POI")))
        {
            Debug.Log("hit POI");

            var poi = hit.collider.gameObject.GetComponent<PointOfInterest>();
            Debug.Log(poi);

            EditorManager.DisplayPOI(poi.PointOfInterestData);
        }

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2, ~LayerMask.NameToLayer("MIR100")))
        {
            Debug.Log("hit MIR100");

            //var robot = hit.collider.gameObject.GetComponent<MIR100>();

            RobotManager.Display();
        }
        */

    }
    public void OnCreate(InputAction.CallbackContext context) 
    {
        Debug.Log("Create Try");
        if (!context.performed) return;
        Transform cameraTransform = Camera.main.transform;
        EditorManager.CreateNewPointOfInterest(cameraTransform.position, cameraTransform.forward);
    }

    public void OnCreateTrajectory(InputAction.CallbackContext context)
    {
        Debug.Log("Create Trajectory");
        if (!context.performed) return;
        Transform cameraTransform = Camera.main.transform;
        RobotManager.CreateNewPointOfTrajectory(cameraTransform.position, cameraTransform.forward);
    }
    public void OnSave(InputAction.CallbackContext context)
    {
        Debug.Log("Save Try");

        if (!context.performed) return;
        EditorManager.SaveScene();
        RobotManager.SaveScene();
    }
}