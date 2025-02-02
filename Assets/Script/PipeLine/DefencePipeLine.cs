using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터의 방어를 수행하는 단계
public class DefencePipeLine : IPipeLine
{
    //다음 단계
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //방어를 수행한 캐릭터들
    private readonly List<PL_ShieldNHeal> fulfillment;
    public List<PL_ShieldNHeal> Fulfillment { get { return fulfillment; } }

    public DefencePipeLine(IPipeLine next)
    {
        this.next = next;
        fulfillment = new ();
    }

    public void Start()
    {
        //모든 캐릭터들이 방어를 수행하도록 호출하지만 진짜 수행하는 캐릭터는 Characters리스트에 추가
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
        //방어를 수행한 캐릭터만 콜백을 호출
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
        //1초의 텀을 두고 다음 단계 시작
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PipeLine = next;
    }
}

public class PL_ShieldNHeal
{
    //누가 하는지
    private readonly Character perpetrator;
    public Character Perpetrator { get { return perpetrator; } }
    //누가 보호받는지
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

    //회복 수치
    private float hp;
    public float HP { get { return hp; } set { hp = value; } }

    //보호막 수치
    private float shield;
    public float Shield { get { return shield; } set { shield = value; } }

    public DefenceInfo(Character victim, float hp, float shield)
    {
        this.victim = victim;
        this.hp = hp;
        this.shield = shield;
    }
}