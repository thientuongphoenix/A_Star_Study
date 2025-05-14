using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý việc tạo và xử lý chướng ngại vật trong không gian 3D.
/// Class này cho phép người dùng tạo chướng ngại vật bằng chuột phải và tự động cập nhật lưới điểm.
/// </summary>
/// <remarks>
/// - Tạo chướng ngại vật tại vị trí chuột phải
/// - Tự động cập nhật lưới điểm khi có chướng ngại vật mới
/// - Tích hợp với PointGrid để quản lý các điểm xung quanh chướng ngại vật
/// - Hỗ trợ tạo chướng ngại vật với prefab tùy chỉnh
/// </remarks>
public class ObstaclesBuilder : MonoBehaviour
{
    public GameObject obstaclePrefab;



    private PointGrid grid;

    private void Start()
    {
        grid = Astar_Manager.Singleton.gameObject.GetComponent<PointGrid>();
    }

    private void Update()
    {
    
            if(Input.GetMouseButtonDown(1))
            {
               AddObstacles();
            }
    
    
    
    }
    
    
    
    
    
    void AddObstacles()
    {
        Vector3 Pos = Input.mousePosition;
        Pos.z = 20;
    
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Pos);
    
        GameObject obstacle = Instantiate(obstaclePrefab, mouseWorldPosition, new Quaternion(0, 0, 0, 0));
    
        obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.localScale.y/2, obstacle.transform.position.z);
    
        grid.AddObstaclesPointsToGrid(obstacle);
    
    }
}
