using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazier : MonoBehaviour
{
    //위치값
    [SerializeField]
    [Range(0f, 1f)]
    private float value;
    public float Value { get { return value; } set { this.value = value; } }
    //어떤 지점들을 참조할건지 (시작위치와 마지막 위치를 포함)
    [SerializeField]
    private List<Transform> list;
    //움직일 객체
    [SerializeField]
    private Transform target;
    public Transform Target { get { return target; } set { target = value; } }
    //target이 방향대로 몸을 회전할지
    [SerializeField]
    private bool dir;
    public bool Dir { get { return dir; } set { dir = value; } }

    // Update is called once per frame
    void Update()
    {
        //타겟이 있다면 영향을 받을 위치도 최소 2개가 있어야 함
        if(target != null && list != null && list.Count > 0)
        {
            //value대로 오브젝트를 이동시키기

            //모든 지점에 대해 value만큼의 영향을 받고 
            Vector3 position = list[0].position;
            for(int i = 1; i < list.Count; i++)
            {
                position = Vector3.Lerp(position, list[i].position, value);
            }

            //이동중인 방향으로 몸을 회전시켜야 하면 회전
            if(dir)
            {
                Vector3 look = position - target.position.normalized;
                float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg - 90;
                target.eulerAngles = new Vector3(0, 0, -angle);
            }

            //위치 이동
            target.position = position;
        }
    }
}
