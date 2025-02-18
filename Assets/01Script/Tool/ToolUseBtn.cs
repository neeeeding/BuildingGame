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
            tool.transform.localRotation = Quaternion.Inverse(player.transform.rotation); //�÷��̾ �� �� ���� ���ƹ����ɷ� ������

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

    private void ActiveBtn(ToolSO so, GameObject tool) //��ư Ȱ��ȭ �� ��
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
        GameObject tool = Instantiate(this.tool); //���߿� �θ� ���� (��������� ���� �ܰ躰�� �� ������ �ƴ� �α׸� ��� �Ѱ���)
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
