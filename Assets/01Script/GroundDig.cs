using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDig : MonoBehaviour
{
    private Terrain ground;
    private TerrainData groundData;
    private int groundResolution;

    [SerializeField]private float digDepth = 0.002f; // 땅을 팔 깊이
    [SerializeField] private float digRadius = 0.05f;  // 땅을 팔 반경

    private void Awake()
    {
        ground = GetComponent<Terrain>();
        groundData = ground.terrainData;
        groundResolution = groundData.heightmapResolution;

        ToolUseBtn.OnUseTool += GroundDigging;

        ResetGround();
    }

    private void GroundDigging(ToolSO tool)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = 3;

        Vector3 worldMousePos = ray.GetPoint(distance);
        if (tool.type == ToolType.soil)
        {
            ModifyTerrain(worldMousePos, false);
        }
        else if(tool.type == ToolType.car)
        {
            ModifyTerrain(StageManager.Instance.Player.transform.TransformPoint(new Vector3(0.65f, 1.25f, 3.11f)), true);
        }
    }

    private void ResetGround()
    {
        float baseDepth = digDepth;
        float baseRadius = digRadius;

        digDepth = 100f;
        digRadius = 100f;
        ModifyTerrain(gameObject.transform.position,true);
        digDepth = 0.013f;
        ModifyTerrain(gameObject.transform.position, false);

        digDepth = baseDepth;
        digRadius = baseRadius;
    }

    private void ModifyTerrain(Vector3 worldPos, bool isDigging) //true : 땅파기 , false : 채우기
    {
        // Terrain 좌표로 변환
        Vector3 terrainPos = worldPos - ground.transform.position;
        int x = Mathf.RoundToInt((terrainPos.x / groundData.size.x/*전체 사이즈*/) * groundResolution/*해상도*/); //픽셀 단위로 변환
        int z = Mathf.RoundToInt((terrainPos.z / groundData.size.z) * groundResolution);
        int radius = Mathf.RoundToInt((digRadius / groundData.size.x) * groundResolution);

        // 좌표가 범위를 벗어나지 않도록 보정
        int startX = Mathf.Clamp(x - radius, 0, groundResolution - 1);
        int startZ = Mathf.Clamp(z - radius, 0, groundResolution - 1);

        int width = Mathf.Clamp(radius * 2, 1, groundResolution - startX);
        int height = Mathf.Clamp(radius * 2, 1, groundResolution - startZ);

        float[,] heights = groundData.GetHeights(startX, startZ, width, height);  //팔 부분

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (isDigging)
                    heights[i, j] = Mathf.Max(0, heights[i, j] - digDepth); // 최소 높이 0 유지
                else
                    heights[i, j] = Mathf.Min(1, heights[i, j] + digDepth); // 최대 높이 1 유지
            }
        }

        groundData.SetHeightsDelayLOD(startX, startZ, heights);
    }

    private void OnDisable()
    {
        ToolUseBtn.OnUseTool -= GroundDigging;
    }
}
