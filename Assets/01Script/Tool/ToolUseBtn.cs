using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolUseBtn : MonoBehaviour
{
    public static Action OnUseTool;

    private Image btnImage; //���� ��� ��ư �̹���
    private GameObject tool; //���� ���� ������Ʈ
    private ToolSO toolSO; //������ ���� ����

    [SerializeField] private GameObject justUseBtn; //���� ��� ��ư
    [SerializeField] private GameObject yBtn; //yȸ��
    [SerializeField] private GameObject xBtn; //xȸ��

    private Quaternion toolAngle; //���� ȸ�� ��

    private GameObject player; //�÷��̾�
    
    private Vector2 mousePoint; //���콺 ��ġ

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
            tool.transform.localRotation = Quaternion.Inverse(player.transform.rotation) * toolAngle; //�÷��̾ �� �� ���� ���ƹ����ɷ� ������


            mousePoint = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, 1920), Mathf.Clamp(Input.mousePosition.y, 0, 1080));
            if (Input.GetMouseButtonDown(0) && mousePoint.y >= 250)
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
        toolAngle = toolAngle * Quaternion.Euler(90, 0, 0);
    }

    public void YBtnClcik()
    {
        toolAngle = toolAngle * Quaternion.Euler(0, 90, 0);
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

            GameObject toolPrepabs = Instantiate(tool, player.transform); //�̸����⸦ ����
            toolPrepabs.SetActive(true);
            this.tool = toolPrepabs;

            toolAngle = Quaternion.identity;
        }
    }

    private Vector3 ToolPosition() //�̸� ���� ������ ��ġ
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = 3;

        Vector3 worldMousePos = ray.GetPoint(distance);
        return tool.transform.parent.InverseTransformPoint(worldMousePos);
    }

    private void UseTool() //���� ��ġ
    {
        OnUseTool?.Invoke();

        GameObject tool = Instantiate(this.tool); //���߿� �θ� ���� (��������� ���� �ܰ躰�� �� ������ �ƴ� �α׸� ��� �Ѱ���)
        tool.SetActive(true);
        tool.transform.position = this.tool.transform.position;
        tool.transform.rotation = toolAngle;
    }

    private void NotActiveBtn() //���� ��Ȱ��ȭ
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
