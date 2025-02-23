using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolUseBtn : MonoBehaviour
{
    public static Action OnUseTool; //설치 함
    public static Action OnNotUse; //설치 안함

    private Image btnImage; //도구 사용 버튼 이미지
    private GameObject toolObj; //실제 도구 오브젝트
    private ToolSO toolSO; //도구에 대한 정보
    private ToolCard card;

    [SerializeField] private GameObject justUseBtn; //도구 사용 버튼
    [SerializeField] private GameObject yBtn; //y회전
    [SerializeField] private GameObject xBtn; //x회전

    private Quaternion toolAngle; //도구 회전 각

    private GameObject player; //플레이어
    [SerializeField] private GameObject toolKeep; //도구 저장하는 곳
    
    private Vector2 mousePoint; //마우스 위치

    private void Awake()
    {
        btnImage = justUseBtn.GetComponent<Image>();
        ToolCard.toolBtnUse += ActiveBtn;
        ToolCard.toolBtnNotUse += NotActiveBtn;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (toolSO != null && toolSO.type != ToolType.car && toolSO.type != ToolType.delete) //so가 널이거나 차이거나 지우기가 아니라면
        {
            toolObj.transform.localPosition = ToolPosition();
            toolObj.transform.localRotation = Quaternion.Inverse(player.transform.rotation) * toolAngle; //플레이어가 돌 때 같이 돌아버린걸로 보여서


            mousePoint = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, 1920), Mathf.Clamp(Input.mousePosition.y, 0, 1080));
            if (Input.GetMouseButtonDown(0) && mousePoint.y >= 250)
            {
                StartCoroutine(UseTool());
            }
        }
    }

    public void JustBtnClick()
    {

    }

    public void XBtnClick()
    {
        toolAngle = toolAngle * Quaternion.Euler(90, 0, 0);
    }

    public void YBtnClcik()
    {
        toolAngle = toolAngle * Quaternion.Euler(0, 90, 0);
    }

    private void ActiveBtn(ToolSO so, GameObject tool, ToolCard toolCard) //버튼 활성화 될 때
    {
        toolObj = tool;
        toolSO = so;
        card = toolCard;
        if(so.type == ToolType.car) //차일 때
        {
            btnImage.gameObject.SetActive(true);
            btnImage.sprite = so.toolImage;
        }
        else if(so.type == ToolType.rotateTool) //도는 도구일 때
        {
            ToolTypeRotate();
        }
        else if(so.type == ToolType.delete) //지우는 도구일 때
        {
            DeleteTool.canDelete = true;
        }
    }

    private void ToolTypeRotate() //도는 도구일 때
    {
        yBtn.SetActive(true);
        xBtn.SetActive(true);

        GameObject toolPrepabs = Instantiate(toolObj, player.transform); //미리보기를 생성
        toolPrepabs.SetActive(true);
        toolObj = toolPrepabs;
        toolObj.GetComponent<Collider>().enabled = false;

        toolAngle = Quaternion.identity;
    }

    private Vector3 ToolPosition() //미리 보기 도구의 위치
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = 3;

        Vector3 worldMousePos = ray.GetPoint(distance);
        return toolObj.transform.parent.InverseTransformPoint(worldMousePos);
    }

    private IEnumerator UseTool() //도구 설치
    {
        OnUseTool?.Invoke();

        GameObject tool;

        if (card.toolList.Count <= 0)
        {
            tool = card.AddTool();
            tool.SetActive(true);
        }
        else
        {
            tool = card.toolList[0];
            card.toolList.RemoveAt(0);
            tool.SetActive(true);
        }
        //나중에 부모 결정 (점수계산을 위해 단계별로 할 것인지 아님 싸그리 모아 둘건지)
        tool.transform.SetParent(toolKeep.transform);
        tool.transform.position = toolObj.transform.position;
        tool.transform.rotation = toolAngle;

        yield return new WaitForSeconds(1.1f);
        OnNotUse?.Invoke();
    }

    private void NotActiveBtn() //도구 비활성화
    {
        justUseBtn.SetActive(false);
        yBtn.SetActive(false);
        xBtn.SetActive(false);

        if(toolSO != null && toolSO.type != ToolType.car && toolSO.type != ToolType.delete)
        {
            card.toolList.Add(toolObj);
            toolObj.SetActive(false);
            toolObj.transform.SetParent(card.transform, false);
        }

        toolObj = null;
        toolSO = null;
        DeleteTool.canDelete = false;
    }

    private void OnDisable()
    {
        ToolCard.toolBtnUse -= ActiveBtn;
        ToolCard.toolBtnNotUse -= NotActiveBtn;
    }
}
