using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �������� �������� ī������ ���� ����� on off ������ ���߿��� ī������ ������� �����ؼ� �����ֵ��� �ؾ������� ���ص� ������
public class Book : MonoBehaviour
{
    //���� �������� ����
    [SerializeField]
    private int page;

    //�������� �ش��ϴ� ��ü��
    [SerializeField]
    private List<Transform> pages = new ();

    //�ʱ⼳��
    private void Start()
    {
        //ù �������� Ȱ��ȭ �������� ��Ȱ��ȭ
        page = 0;
        pages[0].gameObject.SetActive(true);
        for(int i = 1; i < pages.Count; i++)
        {
            pages[i].gameObject.SetActive(false);    
        }
    }

    //���� �������� �ѱ��
    public void OnButtonNextPage()
    {
        //���� �������� ���ٸ� ����
        if (page + 1 >= pages.Count)
        {
            return;
        }
        //��ǥ �������� Ȱ��ȭ �������� ��Ȱ��ȭ
        page++;
        for(int i = 0; i < pages.Count; i++)
        {
            if(i == page)
                pages[i].gameObject.SetActive(true);
            else
                pages[i].gameObject.SetActive(false);
        }
    }

    //���� �������� �ѱ��
    public void OnButtonPrePage()
    {
        //���� �������� ���ٸ� ����
        if (page - 1 < 0)
        {
            return;
        }
        //��ǥ �������� Ȱ��ȭ �������� ��Ȱ��ȭ
        page--;
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == page)
                pages[i].gameObject.SetActive(true);
            else
                pages[i].gameObject.SetActive(false);
        }
    }
}
