using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageData
{
    public int StageNum { set; get; }   // 스테이지 번호
    public string CategoryName { set; get; } // 폴더명

    public string ImageName { set; get; }   // 이미지 파일명

    public int DropColorCount { set; get; } // 드롭 블럭 갯수
    public int EndingAnimationNum { set; get; } // 스테이지 클리어시 보여줄 애니메이션 번호

    public StageData() { } // 기본 생성자
    public StageData(int stageNum, string categoryName, string imageName, int dropColorCount, int endingAnimationNum)
    {
        StageNum = stageNum;
        CategoryName = categoryName;
        ImageName = imageName;
        DropColorCount = dropColorCount;
        EndingAnimationNum = endingAnimationNum;
    }
}


/// <summary>
/// Resources 폴더의 CSV파일을 로드한다.
/// </summary>
public class CsvFileLoad
{

    public static void OnLoadCSV(string filename, List<StageData> stageDatas)
    {
        string file_path = "CSV/";
        file_path = string.Concat(file_path, filename);

        TextAsset ta = Resources.Load<TextAsset>(file_path);

        OnLoadTextAsset(ta.text, stageDatas);

        Resources.UnloadAsset(ta);

        ta = null;
    }

    static public void OnLoadTextAsset(string data, List<StageData> stagedatas)
    {
        string[] str_lines = data.Split('\n');

        // 첫 라인은 설명이어서 제외한다.
        for(int i = 1; i < str_lines.Length - 1; ++i)
        {
            string[] values = str_lines[i].Split(',');

            StageData sd = new StageData();

            sd.StageNum = int.Parse(values[0]); // 스테이지 번호
            sd.CategoryName = values[1];    // 폴더명(카테고리명)
            sd.ImageName = values[2];   // 이미지명
            sd.DropColorCount = int.Parse(values[3]);   // 남길 컬러수
            sd.EndingAnimationNum = int.Parse(values[4]);   // 클리어 애니메이션 종류번호

            stagedatas.Add(sd);

        }



    }

}
