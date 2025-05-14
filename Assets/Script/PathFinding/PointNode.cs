using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Đại diện cho một điểm trong lưới điểm, chứa thông tin về vị trí và các kết nối với các điểm khác.
/// Class này triển khai IComparable để hỗ trợ việc sắp xếp trong Heap.
/// </summary>
/// <remarks>
/// - Lưu trữ thông tin về chi phí di chuyển (gCost, hCost, fCost)
/// - Quản lý danh sách các điểm lân cận có thể di chuyển đến
/// - Hỗ trợ việc tìm đường bằng cách lưu trữ điểm cha trong quá trình tìm kiếm
/// - Triển khai CompareTo để so sánh các điểm dựa trên chi phí fCost
/// </remarks>
public class PointNode : IComparable<PointNode>
{
  
    public Vector3 position;

    public PointNode parent;

    public float gCost, hCost;

    public float fCost;

    public List<PointNode> neighbors;



    public PointNode(Vector3 positions)
    {
        this.position = positions;

        neighbors = new List<PointNode>();

        gCost = 0;
        hCost = 0;
        fCost = 0;

    }

    public void AddNeighbors(List<PointNode> neighbors)
    {
        this.neighbors = neighbors;
    }


    #region CompareTo Method
    public int CompareTo(PointNode nodeToCompare)
    {

        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
    #endregion

}
