using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCell : MonoBehaviour
{
    [SerializeField] private Image sStageImage;
    [SerializeField] private Text sText;

    [HideInInspector]
    public GameObject stageView;
    [HideInInspector]
    public Convert3D convert3D;
    [HideInInspector]
    public string categoryname;
    [HideInInspector]
    public string imagename;

    public void SetImage(Sprite sprite)
    {
        sStageImage.sprite = sprite;
    }

    public void SetText(string text)
    {
        sText.text = text;
    }


    public void OnClickCell()
    {        
        convert3D.enabled = true;
        convert3D.PlayGame(categoryname, imagename);
        stageView.GetComponent<StageView>().ShowGameView();
    }
}
