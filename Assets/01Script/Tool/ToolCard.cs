using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject realTool; //도구 (실제)
    [SerializeField] private ToolSO toolType; //도구 종류
    private Image _myImage; //도구 이미지 (버튼)
    public static Action<ToolSO, GameObject, ToolCard> toolBtnUse; //도구 사용 버튼
    public static Action toolBtnNotUse; //도구 사용 버튼

    private static bool isUseTool; //true : 사용중인 도구가 존재, false : 사용 가능함

    private bool isTool; // true : 설치용 도구 , false : 그외의 도구

    public List<GameObject> toolList; //도구 리스트
    [SerializeField] private float ListMax = 50f; //최대 수
    [SerializeField] private float ListMin = 10f; // 최소 수
    private bool activeList; // true : 삭제하거나 추가중, false : 아무것도 안함
    private float noUseTime; //이 도구를 사용하지 않은 시간

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        toolType.isUse = false;

        if (toolType.type != ToolType.delete && toolType.type != ToolType.soil) //지우기만 아니면
        {
            realTool.SetActive(true);
        }

        CompleteBtn.CurrentStep += ShowToolBtn;
    }

    private void Start()
    {
        isTool = toolType.type == ToolType.clcikTool || toolType.type == ToolType.rotateTool;
        if (isTool)
        {
            StartCoroutine(AddListTools(ListMin));
        }

        if (toolType.type != ToolType.delete)
            _myImage.sprite = toolType.toolImage;
        NotUse();
        isUseTool = false;
    }

    private void Update()
    {
        if (!activeList && gameObject.activeSelf && isTool) //도구가 비활성(생성,삭제 중이 아니거나 클릭 상태 아니면)이고 본인은 활성화 중일 때
        {
            if (toolList.Count < ListMin && noUseTime < 60 * 5) //도구 수가 20보다 작고, 마지막 사용 시간이 5분 이하면
            {
                StopAllCoroutines();
                StartCoroutine(AddListTools(toolList.Count - ListMin));
            }
            else if (toolList.Count >= ListMax) //도구 수가 50을 넘을 때
            {
                StopAllCoroutines();
                StartCoroutine(DeleteTools(ListMax));
            }
            else if (toolList.Count >= 1 && noUseTime > 60 * 5) //도구가 1이상이고 마지막 사용 시간이 5분 이상이면
            {
                DeleteTools(1);
            }
            else
            {
                noUseTime += Time.deltaTime;
            }

        }
    }

    public GameObject AddTool() //도구 생성
    {
        GameObject newTool = Instantiate(realTool, transform);
        newTool.SetActive(false);
        newTool.transform.SetParent(transform, false);

        if (newTool.TryGetComponent(out DeleteTool toolSC))
        {
            toolSC.myRoot = this;
        }
        return newTool;
    }

    private IEnumerator AddListTools(float num) //도구 (리스트) 채우기
    {
        activeList = true;
        float nowNum = 0;
        do
        {
            nowNum++;

            toolList.Add(AddTool());

            yield return new WaitForSeconds(5f);
        }
        while (nowNum < num);

        activeList = false;
    }

    private IEnumerator DeleteTools(float num) //도구 (리스트) 비우기
    {
        activeList = true;
        float nowNum = 0;
        while (nowNum++ >= num)
        {
            GameObject Delete = toolList[0];
            toolList.RemoveAt(0);
            Destroy(Delete);

            yield return new WaitForSeconds(5f);
        }
        activeList = false;
    }


    private void ShowToolBtn(StepType step) //단계별 도구 보이기
    {
        NotUse();
        gameObject.SetActive((toolType.useStep & step) != 0);
    }

    public void ClickTool() //도구(버튼) 누를 때
    {
        if (!toolType.isUse && !isUseTool) UseTool(); //사용
        else if (toolType.isUse && isUseTool) NotUse(); //비활성
        else return; //사용 중인데 다른 도구를 누름

        toolType.isUse = !toolType.isUse;
        isUseTool = !isUseTool;
    }

    public void NotUse() //도구 비활성
    {
        StopAllCoroutines();
        activeList = false ;

        _myImage.color = Color.white;

        toolBtnNotUse?.Invoke();
        
        if(toolType.type == ToolType.car)
        {
            realTool.SetActive(false);
            realTool.transform.SetParent(transform, false);
        }
    }

    public void UseTool() //사용 버튼을 활성화
    {
        noUseTime = 0;
        activeList = true;

        _myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);

        toolBtnUse?.Invoke(toolType, realTool, this);

        if (toolType.type == ToolType.car) //차 타입이라면
        {
            realTool.SetActive(true);
            realTool.transform.SetParent(StageManager.Instance.Player.transform, false);
        }
    }

    private void OnDisable()
    {
        CompleteBtn.CurrentStep -= ShowToolBtn;
    }
}
