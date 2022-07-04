using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum State
    {
        STOP,   // 정지
        MOVE,   // 움직이려고 하는 상태
        MOVING, // 움직이고 있는 상태
        FIXED   // 백보드에 자기위치에 고정된 상태
    };

    [SerializeField] private GameObject _sEdge;
    [SerializeField] private TextMesh _sNumberText;

    public Color OriginColor { set; get; }  // 블럭 컬러값을 저장.

    public int BlockNumber { set; get; }    // 컬러값에 매긴 번호

    public int col { set; get; }    // 세로열
    public int row { set; get; }    // 가로열

    public Vector3 OriginPosition { set; get; } // 블럭 생성시 위치값
    public Vector3 OriginScale { set; get; }    // 블럭의 스케일값을 기록.

    private const float CHECKPOSITIONRANGE = 0.1f;


    public string NumberText
    {
        set
        {
            BlockNumber = int.Parse(value);
            _sNumberText.text = value;
        }

        get
        {
            return _sNumberText.text;
        }
    }

    public State CurrentState { set; get; } // 블럭의 현재 상태 값 저장

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = State.STOP;
    }

    /// <summary>
    /// 블럭이 매치되었을때 애니메이션 처리함수
    /// </summary>
    public void MatchBlockAnimationStart()
    {
        LeanTween.scale(this.gameObject, new Vector3(1.8f, 1.8f, 1.8f), 0.3f)
            .setEase(LeanTweenType.easeInBack)
            .setOnComplete(MatchBlockAnimationEnd);

            /*
            .setOnComplete(()=>
            {
                LeanTween.scale(this.gameObject, Vector3.one, 0.3f)
                    .setEase(LeanTweenType.easeInBounce);
            });
            */

    }

    public void MatchBlockAnimationEnd()
    {
        LeanTween.scale(this.gameObject, Vector3.one, 0.2f)
            .setEase(LeanTweenType.easeInBack);           

    }

    /// <summary>
    /// 드래그 하는 블럭과 백블럭이 겹쳤는지 판단한다.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool CheckMatchPosition(GameObject target)
    {
        if((OriginPosition.x - CHECKPOSITIONRANGE) < target.transform.position.x &&
           (OriginPosition.x + CHECKPOSITIONRANGE) > target.transform.position.x &&
           (OriginPosition.y - CHECKPOSITIONRANGE) < target.transform.position.y &&
           (OriginPosition.y + CHECKPOSITIONRANGE) > target.transform.position.y)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 인자로 전달된 obj(back보드상의 블럭)의 위치로 클릭되 블럭을 이동시킨다.
    /// </summary>
    /// <param name="obj"></param>
    public void MoveToFixedPosition(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<Block>().CurrentState = State.FIXED;
        LeanTween.move(this.gameObject, obj.transform, 0.8f)
            .setEase(LeanTweenType.easeInBack)
            .setOnComplete(MoveToFiexedPositionComplete);

    }

    /// <summary>
    /// 이동이 완료된 후에 처리.
    /// </summary>
    public void MoveToFiexedPositionComplete()
    {
        this.gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        this.GetComponent<MeshRenderer>().material.renderQueue = 2900;
        this.gameObject.transform.localScale = OriginScale;

        CurrentState = State.FIXED;
        ShowOnOffNumberText(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;

        Destroy(this.gameObject.GetComponent<Rigidbody>());

    }

    /// <summary>
    /// 인자로 들어온 블럭의 컬러값과 같은지 체크..
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool CheckMatchColor(GameObject target)
    {
        if(OriginColor == target.GetComponent<Block>().OriginColor)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 큐브에 부여된 넘버값을 표시/비표시
    /// </summary>
    /// <param name="onOff"></param>
    public void ShowOnOffNumberText(bool onOff)
    {
        _sNumberText.gameObject.SetActive(onOff);
        _sEdge.SetActive(onOff);
        gameObject.GetComponent<MeshRenderer>().enabled = onOff;
    }

    /// <summary>
    /// 블럭에 부여된 컬러값으로 텍스트의 컬러값을 지정한다.
    /// </summary>
    /// <param name="color"></param>
    public void SetNumberTextColor(Color color)
    {
        _sNumberText.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
