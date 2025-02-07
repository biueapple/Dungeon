using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷���Ʈ�� �����ϴ� �ܰ�
public class CreatePipeLine : IPipeLine
{
    //���� �ܰ�
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //��� �ߴ���
    private int count = 0;
    //��� �ؾ��ϴ���
    private int listCount;

    public CreatePipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void Start()
    {
        //��� �ؾ��ϴ��� �Է�
        listCount = GameManager.Instance.Characters.Count;
        //����
        PlateSetting();
    }

    public void Update()
    {
        //�Ϸ��� ������ �ؾ��ϴ� ������ ���ٸ�
        if(count == listCount)
        {
            //�����ܰ��
            count = 0;
            GameManager.Instance.PipeLine = next;
        }
    }

    public void End()
    {
        //�ݹ� ȣ�� (��� ĳ���Ͱ� ��ǥ)
        List<Character> list = GameManager.Instance.Characters;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            for (int p = list[i].PCB.Count - 1; p >= 0; p--)
            {
                list[i].PCB[p].Create();
            }
        }
    }

    //��� ĳ���͵��� ���� �����ϱ��
    private void PlateSetting()
    {
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.Instance.StartCoroutine(PlateCreate(list[i]));
        }
    }

    private IEnumerator PlateCreate(Character character)
    {
        //ĳ���Ͱ� �� ������ �Ϸ��ϸ�
        yield return GameManager.Instance.StartCoroutine(character.CreateC());
        //ī���� ����
        count++;
    }

}
