﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển di chuyển của agent trong không gian 3D, sử dụng đường đi được tìm bởi thuật toán A*.
/// Class này xử lý việc di chuyển mượt mà và xoay hướng của agent theo đường đi.
/// </summary>
/// <remarks>
/// - Nhận input từ người dùng để đặt điểm đích
/// - Di chuyển agent theo đường đi đã được tính toán
/// - Tự động xoay hướng agent về phía điểm đích tiếp theo
/// - Hỗ trợ điều chỉnh tốc độ di chuyển và khoảng cách dừng
/// </remarks>
public class Agent : MonoBehaviour
{
    

    public float speed = 3;

    public float stoppingDistance = 0.5f;

    


    private Camera _camera;

    private Vector3[] _targetPath;

    private int _indexPath = 0;


    private void Start()
    {
        _camera = Camera.main;
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetNewTarget();
        }

        if(_targetPath == null )
        {
            return;
        }

        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (_indexPath  >=  _targetPath.Length)
        {
            return;
        }


        RoateToTarget(_targetPath[_indexPath]);
     

        transform.position = Vector3.MoveTowards(transform.position, _targetPath[_indexPath], speed * Time.deltaTime);


        float distanceToTheNextWayPoint = Vector3.Distance(transform.position, _targetPath[_indexPath]);

        float distanceToFinaltWayPoint= Vector3.Distance(transform.position, _targetPath[_targetPath.Length - 1]);



        if (distanceToTheNextWayPoint < 0.05f)
        {
            _indexPath++;
        }


        if(distanceToFinaltWayPoint < stoppingDistance)
        {
            _indexPath = _targetPath.Length;
        }
        
        
    }

    void RoateToTarget(Vector3 target)
    {
        transform.LookAt(target);
    }


    void SetNewTarget()
    {

        Vector3 Pos = Input.mousePosition;
        Pos.z = 20;

        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Pos);

        PathRequest pathRequest = new PathRequest(transform.position, mouseWorldPosition, OnRequestReceived);

        PathRequestManager.Singleton.Request(pathRequest);

    }

    public void OnRequestReceived(Vector3[] path, bool succes)
    {
        _targetPath = path;
        _indexPath = 0;
    }

}
