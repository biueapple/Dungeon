using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǿ� Ű���忡 ���� ī�尡 �ҼӵǾ� �ִ��� �����ִ� ��
public class Tool : MonoBehaviour
{
    //���� (���� ī����� �ҼӵǾ� �ִ��� �����ؼ� ������)
    [SerializeField]
    private Sorting sorting;
    public Sorting Sorting { get { return sorting; } }


    void Start()
    {
        //ī���� ũ��
        sorting.Size = 150;
        //�� ������ ����
        sorting.Padding = 10;
    }
}
