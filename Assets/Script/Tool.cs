using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//판에 키워드에 무슨 카드가 소속되어 있는지 보여주는 툴
public class Tool : MonoBehaviour
{
    //정렬 (무슨 카드들이 소속되어 있는지 정렬해서 보여줌)
    [SerializeField]
    private Sorting sorting;
    public Sorting Sorting { get { return sorting; } }


    void Start()
    {
        //카드의 크기
        sorting.Size = 150;
        //그 사이의 간격
        sorting.Padding = 10;
    }
}
