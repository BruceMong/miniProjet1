using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.PackageManager.UI;
using UnityEngine;

using System.Linq;

public class RobotManager : MonoBehaviour
{

    public RobotWindow RobotWindow;
    public GameObject Player;
    public RobotController Robot;

    public PointOfTrajectory PointOfTrajectoryPrefab;
    public string ConfigFileName = "Trajectory.json";
    protected List<PointOfTrajectory> _pointOfTrajectoryList = new List<PointOfTrajectory>();
    protected string path;

    int _currentID = 0;


    //____

    private void Start()
    {
        path = Application.streamingAssetsPath + "/" + ConfigFileName;
        LoadScene();

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
            List<PointOfTrajectoryStruct> trajectoryList =
           (List<PointOfTrajectoryStruct>)serializer.Deserialize(file,
           typeof(List<PointOfTrajectoryStruct>));
            if (trajectoryList == null)
            {
                return;
            }
            foreach (var pointOfTrajectory in trajectoryList)
            {
                var trajectory = Instantiate<PointOfTrajectory>(PointOfTrajectoryPrefab);
                trajectory.PointOfTrajectoryData = pointOfTrajectory;
                _pointOfTrajectoryList.Add(trajectory);
            }
        }
    }


    public void CreateNewPointOfTrajectory(Vector3 playerPos, Vector3 playerRotation)
    {
        var poiPosition = playerPos + (playerRotation);
        poiPosition.y = 03.0f;
        PositionStruct posStruct = new PositionStruct(poiPosition.x, poiPosition.z);
        var poi = Instantiate(PointOfTrajectoryPrefab, poiPosition,
       PointOfTrajectoryPrefab.transform.rotation);
        poi.InitializePOI(_currentID++, posStruct);
        _pointOfTrajectoryList.Add(poi);
    }
    public List<PointOfTrajectory> GetTrajectoryList()
    {
        return _pointOfTrajectoryList;
    }
    public void SaveScene()
    {
        var trajectoryStructList = new List<PointOfTrajectoryStruct>();
        _pointOfTrajectoryList.ForEach(x => trajectoryStructList.Add(x.PointOfTrajectoryData));
        string json = JsonConvert.SerializeObject(trajectoryStructList);
        using (StreamWriter file = new StreamWriter(path))
        {
            file.Write(json);
        }
    }


    public void Display()
    {
        RobotWindow.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        RobotWindow.FillData(_pointOfTrajectoryList);
        Player.SetActive(false);
        if (Robot == null)
        {
            GameObject robotObject = GameObject.FindGameObjectWithTag("Robot");
            if (robotObject != null)
            {
                Robot = robotObject.GetComponent<RobotController>();
            }
        }
    }
    public void Hide()
    {
        RobotWindow.gameObject.SetActive(false);
        Player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected PointOfTrajectory GetPointOfTrajectoryById(int id)
    {
        return _pointOfTrajectoryList.Where(x => x.PointOfTrajectoryData.Id == id).First();
    }

    public void DeleteAllPointsOfTrajectory()
    {
        foreach (var trajectory in _pointOfTrajectoryList)
        {
            GameObject.Destroy(trajectory.gameObject);
        }

        _pointOfTrajectoryList.Clear();
        Robot.modeAuto = false;
        RobotWindow.FillData(_pointOfTrajectoryList);

    }

    public void Next()
    {
        if (Robot == null)
        {
            GameObject robotObject = GameObject.FindGameObjectWithTag("Robot");
            if (robotObject != null)
            {
                Robot = robotObject.GetComponent<RobotController>();
            }
        }
        Robot.getNextPoint();
        Robot.MoveToPoint();
    }

    public void ChangeMode()
    {
        Robot.ToggleAutoMode();
    }

}
