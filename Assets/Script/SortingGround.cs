using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//정렬되어 있는 오브젝트의 배경의 크기를 조절해주는 클래스
public class SortingGround : MonoBehaviour
{
    //정렬 클래스
    [SerializeField]
    private Sorting sorting;
    //배경 오브젝트
    [SerializeField]
    private RectTransform ground;
    //여백
    [SerializeField]
    private float spacing;
    //최솟값 (정렬한 오브젝트가 하나도 없어도 배경의 최소 크기를 보장해줌)
    [SerializeField]
    private Vector2 min;
    public Vector2 Min { get { return min; } set {  min = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //정렬을 수행할때 배경의 크기 조절도 수행하도록
        sorting.Action += Adjustment;
        //만약 배경 오브젝트가 설정되어 있지 않다면 이 오브젝트가 들어가 있는 오브젝트를 디폴드로 설정함
        if(ground == null)
            ground = GetComponent<RectTransform>();
    }

    //배경 크기 조절
    public void Adjustment(Transform tf)
    {
        //sorting 의 사이즈와 패딩만큼에다가 spacing만큼의 크기의 gorund 크기조절
        //만약 gorund가 null이라면 transform으로
        if (ground != null)
        {
            //tf.SetParent(ground);
            ground.sizeDelta = Vector2.Max(min, new Vector2((sorting.Size + sorting.Padding) * sorting.Count + spacing * 2, ground.rect.height));
           
        }
    }
}
