using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ĳ������ �� �����ϴ� �ܰ�
public class DefencePipeLine : IPipeLine
{
    //���� �ܰ�
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //�� ������ ĳ���͵�
    private readonly List<PL_ShieldNHeal> fulfillment;
    public List<PL_ShieldNHeal> Fulfillment { get { return fulfillment; } }

    public DefencePipeLine(IPipeLine next)
    {
        this.next = next;
        fulfillment = new ();
    }

    public void Start()
    {
        //��� ĳ���͵��� �� �����ϵ��� ȣ�������� ��¥ �����ϴ� ĳ���ʹ� Characters����Ʈ�� �߰�
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Plate != null)
            {
                list[i].Plate.Defense();
                list[i].Plate.ToolDeactive();
            }
        }
        GameManager.Instance.StartCoroutine(Wait());
    }

    public void Update()
    {

    }

    public void End()
    {
        //�� ������ ĳ���͸� �ݹ��� ȣ��
        for (int i = 0; i < fulfillment.Count ; i++)
        {
            for (int p = fulfillment[i].Perpetrator.PCB.Count - 1; p >= 0; p--)
            {
                fulfillment[i].Perpetrator.PCB[p].Defence(fulfillment[i]);
            }
        }

        for (int i = 0; i < fulfillment.Count; i++)
        {
            for (int v = 0; v < fulfillment[i].Victims.Count; v++)
            {
                fulfillment[i].Victims[v].Victim.Shield += fulfillment[i].Victims[v].Shield;
                fulfillment[i].Victims[v].Victim.HP += fulfillment[i].Victims[v].HP;
            } 
        }

        fulfillment.Clear();
    }

    private IEnumerator Wait()
    {
        //1���� ���� �ΰ� ���� �ܰ� ����
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PipeLine = next;
    }
}

public class PL_ShieldNHeal
{
    //���� �ϴ���
    private readonly Character perpetrator;
    public Character Perpetrator { get { return perpetrator; } }
    //���� ��ȣ�޴���
    private readonly List<DefenceInfo> victims;
    public List<DefenceInfo> Victims { get { return victims; } }

    private readonly AudioClip clip;
    public AudioClip Clip { get { return clip; } }

    public PL_ShieldNHeal(Character perpetrator, List<DefenceInfo> victims, AudioClip clip)
    {
        this.perpetrator = perpetrator;
        this.victims = victims;
        this.clip = clip;
    }
}

public class DefenceInfo
{
    private Character victim;
    public Character Victim { get { return victim; } set { victim = value; } }

    //ȸ�� ��ġ
    private float hp;
    public float HP { get { return hp; } set { hp = value; } }

    //��ȣ�� ��ġ
    private float shield;
    public float Shield { get { return shield; } set { shield = value; } }

    public DefenceInfo(Character victim, float hp, float shield)
    {
        this.victim = victim;
        this.hp = hp;
        this.shield = shield;
    }
}