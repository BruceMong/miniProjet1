using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class EditorManager : WorkshopManager
{
    int _currentID = 0;
    public void CreateNewPointOfInterest(Vector3 playerPos, Vector3 playerRotation)
    {
        var poiPosition = playerPos + (playerRotation);
        poiPosition.y = 03.0f;
        PositionStruct posStruct = new PositionStruct(poiPosition.x, poiPosition.z);
        var poi = Instantiate(PointOfInterestPrefab, poiPosition,
       PointOfInterestPrefab.transform.rotation);
        poi.InitializePOI(_currentID++, posStruct);
        _pointOfInterestList.Add(poi);
    }
    public void DeletePointOfInterest(int id)
    {
        var poi = GetPointOfInterestById(id);
        _pointOfInterestList.Remove(poi);
        GameObject.Destroy(poi.gameObject);
    }
    public void SavePointOfInterest(PointOfInterestStruct poiData)
    {
        var poi = GetPointOfInterestById(poiData.Id);
        poi.PointOfInterestData = poiData;
    }
    protected PointOfInterest GetPointOfInterestById(int id)
    {
        return _pointOfInterestList.Where(x => x.PointOfInterestData.Id == id).First();
    }
    public void SaveScene()
    {
        var poiStructList = new List<PointOfInterestStruct>();
        _pointOfInterestList.ForEach(x => poiStructList.Add(x.PointOfInterestData));
        string json = JsonConvert.SerializeObject(poiStructList);
        using (StreamWriter file = new StreamWriter(path))
        {
            file.Write(json);
        }
    }
}