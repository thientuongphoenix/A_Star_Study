using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cấu trúc dữ liệu Heap tối thiểu, được sử dụng để tối ưu hóa thuật toán A*.
/// Class này triển khai các thao tác cơ bản của heap và hỗ trợ sắp xếp các phần tử.
/// </summary>
/// <remarks>
/// - Sử dụng generic type T phải triển khai IComparable
/// - Hỗ trợ các thao tác: Add, Pop, Min, Contains
/// - Tự động duy trì tính chất heap sau mỗi thao tác
/// - Sử dụng Min_Heapify để đảm bảo tính chất heap
/// </remarks>
/// <typeparam name="T">Kiểu dữ liệu của các phần tử trong heap, phải triển khai IComparable</typeparam>
public class Heap<T> where T : IComparable<T>
{

    List<T> items;

    public Heap()
    {
        items = new List<T>();
    }

    #region Private Method
    int leftNode(int _index)
    {
        return ((_index+1) * 2)-1;
    }

    int RightNode(int _index)
    {
        return (_index + 1) * 2;
    }

    int parentNode(int _index)
    {
        return (_index + 1) / 2 - 1;
    }

    void Swap(int _index1, int _index2)
    {
        T Temp = items[_index1];
        items[_index1] = items[_index2];
        items[_index2] = Temp;
    }

    void Build_Min_Heap()
    {
        for(int i=items.Count/2 ; i>=0 ; i--)
        {
            Min_Heapify(i);
        }
    }

    void Min_Heapify(int _index)
    {

        int left = leftNode(_index);
        int right = RightNode(_index);
 

        int smallest = 0;

        if (left < items.Count  && items[left].CompareTo(items[_index]) > -1)
        {     
            smallest = left;
        }
        else
        {      
            smallest = _index;
        }


        if(right < items.Count  && items[right].CompareTo(items[smallest]) > -1)
        {
            smallest = right;
        }

        if(smallest != _index)
        {         
            Swap(_index, smallest);
            Min_Heapify(smallest);
        }
   
    }
    #endregion

    #region Public Method
    public void Add(T item)
    {
        items.Add(item);
        
        Build_Min_Heap();
    }


    public T Min()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Heap is empty");


        return items[0];
    }


    public T Pop()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        T Temp = items[0];       
        items[0] = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        Build_Min_Heap();
      
        return Temp;
    }


    public int Count
    {
        get
        {
            return items.Count;
        }
    }


    public bool Contains(T item)
    {
        return items.Contains(item);
    }
    #endregion

}

