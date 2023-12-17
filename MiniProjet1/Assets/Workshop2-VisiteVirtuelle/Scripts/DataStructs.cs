using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;


[Serializable]
public struct PositionStruct
{
    public float x, y;
    public PositionStruct(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
[Serializable]
public struct PointOfInterestStruct
{
    public int Id;
    [SerializeField] public PositionStruct Position;
    public string Title;
    public string Description;
    public PointOfInterestStruct(int id, PositionStruct position, string title = "Default Title", string description = "Default Text")
 {
        this.Id = id;
        this.Position = position;
        this.Title = title;
        this.Description = description;
    }
}

