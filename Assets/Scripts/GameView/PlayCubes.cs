using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCubes : MonoBehaviour
{
    [SerializeField] private StageManager _sStageManager;

    private void Start()
    {
        
    }
    public void ClearAnimationEnd()
    {
        _sStageManager.PlayNextStage();
    }

    private void Update()
    {
        
    }

}
