using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotWindow : MonoBehaviour
{
    public RobotManager RobotManager;
    public TMP_Text  ContentText;


    public void FillData( List<PointOfTrajectory> trajectoryList)
    {
        string content = "";

        foreach (var point in trajectoryList)
        {
            content += $"ID: {point.PointOfTrajectoryData.Id}, X: {point.PointOfTrajectoryData.Position.x}, Z: {point.PointOfTrajectoryData.Position.y}\n";
        }

        ContentText.text = content;
    }
    /*
    public void SavePOI()
    {
        _poiInfo.Title = TitleInputField.text;
        _poiInfo.Description = ContentTextField.text;
        EditorManager.SavePointOfInterest(_poiInfo);
    }
    public void DeletePOI()
    {
        EditorManager.DeletePointOfInterest(_poiInfo.Id);
    }
    */

}
