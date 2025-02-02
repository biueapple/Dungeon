using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ĵǾ� �ִ� ������Ʈ�� ����� ũ�⸦ �������ִ� Ŭ����
public class SortingGround : MonoBehaviour
{
    //���� Ŭ����
    [SerializeField]
    private Sorting sorting;
    //��� ������Ʈ
    [SerializeField]
    private RectTransform ground;
    //����
    [SerializeField]
    private float spacing;
    //�ּڰ� (������ ������Ʈ�� �ϳ��� ��� ����� �ּ� ũ�⸦ ��������)
    [SerializeField]
    private Vector2 min;
    public Vector2 Min { get { return min; } set {  min = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //������ �����Ҷ� ����� ũ�� ������ �����ϵ���
        sorting.Action += Adjustment;
        //���� ��� ������Ʈ�� �����Ǿ� ���� �ʴٸ� �� ������Ʈ�� �� �ִ� ������Ʈ�� ������� ������
        if(ground == null)
            ground = GetComponent<RectTransform>();
    }

    //��� ũ�� ����
    public void Adjustment(Transform tf)
    {
        //sorting �� ������� �е���ŭ���ٰ� spacing��ŭ�� ũ���� gorund ũ������
        //���� gorund�� null�̶�� transform����
        if (ground != null)
        {
            //tf.SetParent(ground);
            ground.sizeDelta = Vector2.Max(min, new Vector2((sorting.Size + sorting.Padding) * sorting.Count + spacing * 2, ground.rect.height));
           
        }
    }
}
