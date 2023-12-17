using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EditorWindow : WorkshopWindow
{
    public EditorManager EditorManager;
    public TMP_InputField TitleInputField, ContentTextField;
    private PointOfInterestStruct _poiInfo;
    public override void FillData(PointOfInterestStruct poiInfo)
    {
        this._poiInfo = poiInfo;
        TitleInputField.text = poiInfo.Title;
        ContentTextField.text = poiInfo.Description;
    }
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
}