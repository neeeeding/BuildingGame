using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolUseBtn : MonoBehaviour
{
    private Image btnImage;
    private GameObject tool;
    private ToolSO toolSO;

    [SerializeField] private GameObject justUseBtn;
    [SerializeField] private GameObject yBtn;
    [SerializeField] private GameObject xBtn;

    private GameObject player;
    private void Awake()
    {
        btnImage = justUseBtn.GetComponent<Image>();
        ToolCard.toolBtnUse += ActiveBtn;
        ToolCard.toolBtnNotUse += NotActiveBtn;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (toolSO != null && toolSO.type != ToolType.car)
        {
            tool.transform.localPosition = ToolPosition();
            tool.transform.localRotation = Quaternion.Inverse(player.transform.rotation); //플레이어가 돌 때 같이 돌아버린걸로 보여서

            if (Input.GetMouseButtonDown(0))
            {
                UseTool();
            }
        }
    }

    public void JustBtnClick()
    {

    }

    public void XBtnClick()
    {

    }

    public void YBtnClcik()
    {

    }

    private void ActiveBtn(ToolSO so, GameObject tool) //버튼 활성화 될 때
    {
        this.tool = tool;
        toolSO = so;
        if(so.type == ToolType.car)
        {
            btnImage.gameObject.SetActive(true);
            btnImage.sprite = so.toolImage;
        }
        else if(so.type == ToolType.rotateTool)
        {
            yBtn.SetActive(true);
            xBtn.SetActive(true);

            GameObject toolPrepabs = Instantiate(tool, player.transform);
            toolPrepabs.SetActive(true);
            this.tool = toolPrepabs;
        }
    }

    private Vector3 ToolPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = 3;

        Vector3 worldMousePos = ray.GetPoint(distance);
        return tool.transform.parent.InverseTransformPoint(worldMousePos);
    }

    private void UseTool()
    {
        GameObject tool = Instantiate(this.tool); //나중에 부모 결정 (점수계산을 위해 단계별로 할 것인지 아님 싸그리 모아 둘건지)
        tool.SetActive(true);
        tool.transform.position = this.tool.transform.position;
        tool.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void NotActiveBtn()
    {
        justUseBtn.SetActive(false);
        yBtn.SetActive(false);
        xBtn.SetActive(false);

        if(toolSO != null && toolSO.type != ToolType.car)
        {
            Destroy(tool);
        }

        tool = null;
        toolSO = null;
    }

    private void OnDisable()
    {
        ToolCard.toolBtnUse -= ActiveBtn;
        ToolCard.toolBtnNotUse -= NotActiveBtn;
    }
}
