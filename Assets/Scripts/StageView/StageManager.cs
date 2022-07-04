﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Convert3D _sConvert3D;
    [SerializeField] private StageView _sStageView;
    [SerializeField] private GameObject _sGameView;

    public List<StageData> StageDatas { set; get; } // 스테이지 데이타 저장용

    private int _currentStageNum = 2;   // 현재 플레이중인 스테이지 번호

    public int CurrentStageNum
    {
        set
        {
            _currentStageNum = value;
        }

        get
        {
            return _currentStageNum;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StageDatas = new List<StageData>();

        CsvFileLoad.OnLoadCSV("StageDatas", StageDatas);

        // 
        PlayGame();
    }

    private void PlayGame()
    {

    }

    public void PlayNextStage()
    {
        _sGameView.SetActive(false);
        _sStageView.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
