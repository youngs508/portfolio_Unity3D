using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageView : MonoBehaviour
{
    [SerializeField] private GameObject sStageCellPrefab;   // 스테이지 Cell Prefab
    [SerializeField] private StageManager sStageManager;
    [SerializeField] private GameObject sContents;  // 스테이지 셀의 부모 오브젝트
    [SerializeField] private Convert3D sContert3D;
    [SerializeField] private GameObject sGameView;


    // Start is called before the first frame update
    void Start()
    {
        MakeStageCells();
    }

    private void MakeStageCells()
    {
        // 로드한 CSV파일의 데이타로 스크롤 뷰에 스테이지 Cell을 생성한다.
        foreach(StageData sd in sStageManager.StageDatas)
        {
            GameObject StageCell = Instantiate(sStageCellPrefab);

            string path = string.Format("StageImages/{0}/{1}", sd.CategoryName, sd.ImageName);
            Sprite sprite = Resources.Load<Sprite>(path);   // 이미지 파일 로드

            var stageCellcom = StageCell.GetComponent<StageCell>();

            stageCellcom.SetImage(sprite);
            stageCellcom.SetText(sd.ImageName);
            stageCellcom.categoryname = sd.CategoryName;
            stageCellcom.imagename = sd.ImageName;
            stageCellcom.stageView = this.gameObject;
            stageCellcom.convert3D = this.sContert3D;

            StageCell.transform.SetParent(sContents.transform);
        }

        // 스크롤뷰의 셀의 위치 표시 위치값을 보정한다.

        Vector2 pos = sContents.GetComponent<RectTransform>().anchoredPosition;
        int count = sStageManager.StageDatas.Count;

        float ypos = count / 2 * 250;
        sContents.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, -ypos);
    }

    public void ShowGameView()
    {
        this.gameObject.SetActive(false);
        sGameView.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
