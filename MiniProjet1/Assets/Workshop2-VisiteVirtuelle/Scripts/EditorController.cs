using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class EditorController : MonoBehaviour
{
    [SerializeField] private EditorManager EditorManager;


    public void OnAction(InputAction.CallbackContext context)

    {
        Debug.Log("Action Try");

        if (!context.performed) return;
        RaycastHit hit;
        Transform cameraTransform = Camera.main.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2, ~LayerMask.NameToLayer("POI")))
        {
            Debug.Log("hit display");

            var poi = hit.collider.gameObject.GetComponent<PointOfInterest>();
            Debug.Log(poi);

            EditorManager.DisplayPOI(poi.PointOfInterestData);

        }
        else
        {
            Debug.Log("no hit ");

        }
    }
    public void OnCreate(InputAction.CallbackContext context) 
    {
        Debug.Log("Create Try");
        if (!context.performed) return;
        Transform cameraTransform = Camera.main.transform;
        EditorManager.CreateNewPointOfInterest(cameraTransform.position, cameraTransform.forward);
    }
    public void OnSave(InputAction.CallbackContext context)
    {
        Debug.Log("Save Try");

        if (!context.performed) return;
        EditorManager.SaveScene();
    }
}