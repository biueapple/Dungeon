using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazier : MonoBehaviour
{
    //��ġ��
    [SerializeField]
    [Range(0f, 1f)]
    private float value;
    public float Value { get { return value; } set { this.value = value; } }
    //� �������� �����Ұ��� (������ġ�� ������ ��ġ�� ����)
    [SerializeField]
    private List<Transform> list;
    //������ ��ü
    [SerializeField]
    private Transform target;
    public Transform Target { get { return target; } set { target = value; } }
    //target�� ������ ���� ȸ������
    [SerializeField]
    private bool dir;
    public bool Dir { get { return dir; } set { dir = value; } }

    // Update is called once per frame
    void Update()
    {
        //Ÿ���� �ִٸ� ������ ���� ��ġ�� �ּ� 2���� �־�� ��
        if(target != null && list != null && list.Count > 0)
        {
            //value��� ������Ʈ�� �̵���Ű��

            //��� ������ ���� value��ŭ�� ������ �ް� 
            Vector3 position = list[0].position;
            for(int i = 1; i < list.Count; i++)
            {
                position = Vector3.Lerp(position, list[i].position, value);
            }

            //�̵����� �������� ���� ȸ�����Ѿ� �ϸ� ȸ��
            if(dir)
            {
                Vector3 look = position - target.position.normalized;
                float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg - 90;
                target.eulerAngles = new Vector3(0, 0, -angle);
            }

            //��ġ �̵�
            target.position = position;
        }
    }
}
