using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisiteWindow : WorkshopWindow
{
    public WorkshopManager WorkshopManager;
    public TMP_Text TitleText, ContentText;
    public override void FillData(PointOfInterestStruct poiInfo)
    {
        TitleText.text = poiInfo.Title;
        ContentText.text = poiInfo.Description;
    }
}