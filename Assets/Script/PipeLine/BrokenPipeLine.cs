using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

//�÷���Ʈ�� �μ����� �ܰ踦 ���
public class BrokenPipeLine : IPipeLine
{
    //���� �ܰ�
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //��� ĳ���Ͱ� �Ϸ��ߴ���
    private int count = 0;
    //��� ĳ���Ͱ� �Ϸ� �ؾ��ϴ���
    private int listCount;

    public BrokenPipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void Start()
    {
        //���� ĳ���Ͱ� ����� �־����
        listCount = GameManager.Instance.Characters.Count;
        //�÷���Ʈ�� �μ����� ��� ����
        PlateSetting();
    }

    public void Update()
    {
        //������ �Ϸ��� ���� ĳ������ ���� �Ȱ��ٸ� ���� �� �����Ѱ���
        if (count == listCount)
        {
            //���� �ܰ��
            count = 0;
            GameManager.Instance.PipeLine = next;
        }
    }

    public void End() 
    {
        //�ݹ��� ���� (��� ĳ���͵��� �÷���Ʈ�� �μ����״� ��� ĳ���͵��� ���� (���߿� �����ؾ� �ҵ� ))
        List<Character> list = GameManager.Instance.Characters;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            for (int p = list[i].PCB.Count - 1; p >= 0; p--)
            {
                list[i].PCB[p].Broken();
            }
        }
    }

    public void PlateSetting()
    {
        //��� ĳ���͵鿡�� �÷���Ʈ�� �μ���� ���
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.Instance.StartCoroutine(PlateBroken(list[i]));
        }
    }

    private IEnumerator PlateBroken(Character character)
    {
        //ĳ���Ͱ� �÷���Ʈ�� �νô°� �Ϸ��ϸ� 
        Plate plate = character.Plate;
        yield return GameManager.Instance.StartCoroutine(character.BrokenC());
        //�÷���Ʈ�� ����ִ� ī��� ȿ�� �ʱ�ȭ �� �ؽ���
        if (plate != null)
        {
            plate.KeywordInit();
        }
        //ī���� ����
        count++;
    }
}
