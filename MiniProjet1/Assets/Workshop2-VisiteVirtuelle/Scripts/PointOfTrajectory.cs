using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public struct PointOfTrajectoryStruct
{
    public int Id;
    [SerializeField] public PositionStruct Position;
    public PointOfTrajectoryStruct(int id, PositionStruct position)
    {
        this.Id = id;
        this.Position = position;
    }
}



public class PointOfTrajectory : MonoBehaviour
{

    private PointOfTrajectoryStruct _pointOfTrajectoryData;
    public PointOfTrajectoryStruct PointOfTrajectoryData
    {
        get { return _pointOfTrajectoryData; }
        set
        {
            _pointOfTrajectoryData = value;
            SetGameObjectPositionByPOI();
        }
    }
    public void InitializePOI(int id, PositionStruct position)
    {
        PointOfTrajectoryData = new PointOfTrajectoryStruct(id, position);
    }
    public void SetGameObjectPositionByPOI()
    {
        var position = _pointOfTrajectoryData.Position;
        this.transform.position = new Vector3(position.x, 2, position.y);
    }
}