using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�带 �̴� �ܰ�
public class DrawPipeLine : IPipeLine
{
    //���� ����� ����������
    private  IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //��
    private readonly Dummy dummy;
    //������ ��������
    private int count;
    public int Count { get { return count; } set { count = value; } }

    public DrawPipeLine(IPipeLine next, Dummy dummy, int count)
    {
        this.next = next;
        this.dummy = dummy;
        this.count = count;
    }

    public void Start()
    {
        //��ο� ����
        GameManager.Instance.StartCoroutine(Draw(count));
    }

    public void Update() { }

    private IEnumerator Draw(int count)
    {
        //Ƚ����ŭ
        for(int i = 0; i < count; i++)
        {
            //��ο츦 �Ҷ� �������� �Ϸ�� �Ŀ� �۵��ϵ���
            yield return dummy.Draw();
        }
        //�����ܰ��
        GameManager.Instance.PipeLine = next;
    }

    public void End()
    {
        //draw�ܰ� �ݹ� ȣ�� (��ο�� ĳ���Ϳʹ� ������� )
        for (int i = GameManager.Instance.PCB.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.PCB[i].Draw();
        }
        //GameManager.Instance.Characters.ForEach(c => c.PCB.ForEach(p => p.Draw()));
    }
}
