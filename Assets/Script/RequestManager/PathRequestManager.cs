using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Quản lý các yêu cầu tìm đường và xử lý kết quả trả về.
/// Class này hỗ trợ xử lý đa luồng để tối ưu hiệu suất khi có nhiều yêu cầu tìm đường.
/// </summary>
/// <remarks>
/// - Sử dụng Singleton pattern để đảm bảo chỉ có một instance duy nhất
/// - Hỗ trợ xử lý đa luồng thông qua biến multiThreading
/// - Quản lý hàng đợi các kết quả tìm đường
/// - Xử lý callback khi tìm đường hoàn tất
/// </remarks>
public class PathRequestManager : MonoBehaviour
{

    public bool multiThreading = false;

    //Store The Response Result
    Queue<PathResponse> _results = new Queue<PathResponse>();


    public static PathRequestManager Singleton;

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
    }

    private void Update()
    {
        CallBackTheResult();
    }


    // Call back The Result to Each Agent Request
    void CallBackTheResult()
    {
        if (_results.Count > 0)
        {

            lock (_results)
            {
                for (int i = 0; i < _results.Count; i++)
                {
                    PathResponse pathResponse = _results.Dequeue();

                    pathResponse.callBack(pathResponse.path, pathResponse.succes);
                }
            }
        }
    }

    public void Request(PathRequest pathRequest)
    {

        if(multiThreading)
        {
            //Make New Thread for Finding Path
            ThreadStart threadStart = delegate
            {
                Astar_Manager.Singleton.FindingPath(pathRequest, FinishProcessing);
            };

            threadStart.Invoke();
        }
        else
        {
            Astar_Manager.Singleton.FindingPath(pathRequest, FinishProcessing);
        }

    }


    public void FinishProcessing(PathResponse pathResponse)
    {
        if(multiThreading)
        {
            //Add New Result To Queue Result To Call Back it 
            lock (_results)
            {
                _results.Enqueue(pathResponse);
            }
        }
        else
        {
            pathResponse.callBack(pathResponse.path, pathResponse.succes);
        }


    }

}

/// <summary>
/// Cấu trúc dữ liệu chứa thông tin về kết quả tìm đường.
/// </summary>
/// <remarks>
/// - Chứa đường đi dưới dạng mảng các Vector3
/// - Lưu trữ trạng thái thành công của việc tìm đường
/// - Chứa callback function để thông báo kết quả
/// </remarks>
public struct PathResponse
{
    public Vector3[] path;
    public bool succes;
    public Action<Vector3[], bool> callBack;

    public PathResponse(Vector3[] path, bool succes, Action<Vector3[], bool> callBack)
    {
        this.path = path;
        this.succes = succes;
        this.callBack = callBack;
    }

}

/// <summary>
/// Cấu trúc dữ liệu chứa thông tin về yêu cầu tìm đường.
/// </summary>
/// <remarks>
/// - Chứa điểm xuất phát và điểm đích
/// - Lưu trữ callback function để nhận kết quả
/// - Được sử dụng để gửi yêu cầu tìm đường đến PathRequestManager
/// </remarks>
public struct PathRequest
{
    public Vector3 startNode;
    public Vector3 targetNode;
    public Action<Vector3[], bool> callBack;

    public PathRequest(Vector3 startNode, Vector3 targetNode, Action<Vector3[], bool> callBack)
    {
        this.startNode = startNode;
        this.targetNode = targetNode;
        this.callBack = callBack;
    }

}
