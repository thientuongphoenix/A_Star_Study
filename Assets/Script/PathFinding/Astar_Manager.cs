using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý và triển khai thuật toán A* để tìm đường đi ngắn nhất giữa hai điểm trong không gian 3D.
/// Class này sử dụng cấu trúc dữ liệu Heap để tối ưu hóa việc tìm kiếm và tính toán chi phí di chuyển.
/// </summary>
/// <remarks>
/// - Sử dụng Singleton pattern để đảm bảo chỉ có một instance duy nhất
/// - Tích hợp với PointGrid để quản lý lưới điểm trong không gian
/// - Hỗ trợ tính toán chi phí di chuyển dựa trên khoảng cách Euclidean
/// </remarks>
public class Astar_Manager : MonoBehaviour
{

    private PointGrid _pointGrid;

    public static Astar_Manager Singleton;

    private void Awake()
    {
        _pointGrid = GetComponent<PointGrid>();

        if(Singleton == null)
        {
            Singleton = this;
        }

    }
   

    public void FindingPath(PathRequest request ,Action<PathResponse> callBack)
    {

        if(_pointGrid.grid == null)
        {
            return ;
        }

      
        Heap<PointNode> openList = new Heap<PointNode>();
        List<PointNode> closeList = new List<PointNode>();

        PointNode agentNode = _pointGrid.GetPointNodeFromGridByPosition(request.startNode);
        PointNode targetNode = _pointGrid.GetPointNodeFromGridByPosition(request.targetNode);


        openList.Add(agentNode);
        
        
        while (openList.Count > 0)
        {

            PointNode currentNode = openList.Pop();

            

            //Check If Reach The Goal 
            if (_pointGrid.CheckIfPointsLinkedTogether(currentNode, targetNode))
            {
                targetNode.parent = currentNode;

                break;
            }

            
            closeList.Add(currentNode);

         

            List<PointNode> neighboursNode = currentNode.neighbors;

          
            for (int i=0; i < neighboursNode.Count ; i++)
            {
                if (closeList.Contains(neighboursNode[i]))
                {
                    continue;
                }
                else
                {
                    float gCost = currentNode.gCost + CalculateMovementCostToTargetNode(currentNode, neighboursNode[i]);

                    float hCost = CalculateMovementCostToTargetNode(neighboursNode[i], targetNode);
                   

                    if (openList.Contains(neighboursNode[i]) == false)
                    {
                        //Set fValue
                        neighboursNode[i].fCost = hCost + gCost;

                        neighboursNode[i].parent = currentNode;

                        openList.Add(neighboursNode[i]);

                    }
                    else
                    {
                        float new_fCost = gCost+hCost;

                        //Check if new fvalue is best than the older 
                        if (neighboursNode[i].fCost > new_fCost)
                        {
                            //Update fValue 
                            neighboursNode[i].fCost = new_fCost;
                            neighboursNode[i].parent = currentNode;
                        }

                    }

                }
       

            }


        }


        // Create The Path 
        Vector3[] path = CreatePath(agentNode, targetNode);

        


        //Create Response To Call Back it 
        PathResponse pathResponse = new PathResponse(path, true, request.callBack);

     

        //Call Back 
        callBack(pathResponse);


        return;

    }



    //Calculate The Cost from current node  To target node 
    float CalculateMovementCostToTargetNode(PointNode _node, PointNode _targetNode)
    {

        return Vector3.Distance(_node.position, _targetNode.position);

    }



    Vector3[] CreatePath(PointNode _agentNode, PointNode _targetNode)
    {

        List<PointNode> pathNode = new List<PointNode>();

        PointNode currentNode = _targetNode;

        while (currentNode != _agentNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            pathNode.Add(currentNode);

            currentNode = currentNode.parent;

        }

        pathNode.Reverse();

        Vector3[] path = new Vector3[pathNode.Count ];

        for(int i=0; i< pathNode.Count  ;i++)
        {
            path[i] = pathNode[i].position;
        }

        


        return path;

    }
    

}
