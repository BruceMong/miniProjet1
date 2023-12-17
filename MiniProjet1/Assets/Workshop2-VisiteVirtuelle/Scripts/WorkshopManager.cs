using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorkshopManager : MonoBehaviour
{
    public WorkshopWindow Window;
    public PointOfInterest PointOfInterestPrefab;
    public string ConfigFileName = "Configuration.json";
    protected List<PointOfInterest> _pointOfInterestList = new List<PointOfInterest>();
    protected string path;
    public GameObject Player;
    private void Start()
    {
        path = Application.streamingAssetsPath + "/" + ConfigFileName;
        LoadScene();
        //Window.gameObject.SetActive(false);

    }
    public void LoadScene()
    {
        if (!File.Exists(path))
        {
            return;
        }
        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            List<PointOfInterestStruct> poiList =
           (List<PointOfInterestStruct>)serializer.Deserialize(file,
           typeof(List<PointOfInterestStruct>));
            if (poiList == null)
            {
                return;
            }
            foreach (var pointOfInterest in poiList)
            {
                var poi = Instantiate<PointOfInterest>(PointOfInterestPrefab);
                poi.PointOfInterestData = pointOfInterest;
                _pointOfInterestList.Add(poi);
            }
        }
    }
    public void DisplayPOI(PointOfInterestStruct poiInfo)
    {
        Window.FillData(poiInfo);
        Window.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.SetActive(false);
    }
    public void HidePOI()
    {
        Window.gameObject.SetActive(false);
        Player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
