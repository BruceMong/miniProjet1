using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PointOfInterest : MonoBehaviour
{
    private PointOfInterestStruct _pointOfInterestData;
    public PointOfInterestStruct PointOfInterestData
    {
        get { return _pointOfInterestData; }
        set
        {
            _pointOfInterestData = value;
            SetGameObjectPositionByPOI();
        }
    }
    public void InitializePOI(int id, PositionStruct position)
    {
        PointOfInterestData = new PointOfInterestStruct(id, position);
    }
    public void SetGameObjectPositionByPOI()
    {
        var position = _pointOfInterestData.Position;
        this.transform.position = new Vector3(position.x, 0, position.y);
    }
}







