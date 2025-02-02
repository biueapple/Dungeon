using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

//모든 캐릭터들이 공격을 진행하는 단계
public class AttackPipeLine : IPipeLine
{
    //다음 단계
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //공격을 수행한 캐릭터들
    private readonly List<PL_AttackNHit> fulfillment;
    public List<PL_AttackNHit> Fulfillment { get { return fulfillment; } }

    public AttackPipeLine(IPipeLine next)
    {
        this.next = next;
        fulfillment = new();
    }

    public void Start()
    {
        //모든 캐릭터들이
        List<Character> list = GameManager.Instance.Characters;
        //공격을 수행하지만 실제로 공격을 하는 애들만이 characters리스트에 추가됨
        //공격을 직접 하는게 아니라 공격에 대한 정보를 characters리스트에 추가하는것
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Plate != null)
                list[i].Plate.Attack();
        }

        GameManager.Instance.StartCoroutine(NextPipe());
    }

    public void Update()
    {

    }
    
    public void End()
    {
        //공격을 수행한 리스트 초기화
        fulfillment.Clear();
    }

    private IEnumerator NextPipe()
    {
        //파이프라인 콜백리스트에 들어있는 모든 콜백을 공격을 수행한 캐릭터를 넣어 실행

        //공격 파이프라인
        //때리기 (애니메이션)
        fulfillment.ForEach(f => f.Perpetrator.AttackAnimation());
        //공격 소리
        fulfillment.Select(f => f.Clip).Distinct().ToList()
        .ForEach(clip => SoundManager.Instance.Play(clip));
        //사이 텀 1초 (공격 애니메이션을 재생할 시간)

        //콜백
        //공격을 수행한 모든 인원은 자신의 콜백을 호출한다
        for (int f = fulfillment.Count - 1; f >= 0; f--)
        {
            for (int p = fulfillment[f].Perpetrator.PCB.Count - 1; p >= 0; p--)
            {
                fulfillment[f].Perpetrator.PCB[p].Attack(fulfillment[f]);
            }
        }
        

        yield return new WaitForSeconds(0.5f);
        //맞기
        fulfillment.ForEach(f => f.SC());
        fulfillment.ForEach(f => f.HC());

        //맞는 소리 (없음)

        //콜백
        //공격을 수행한 모든 인원중에 공격을 당한 모든 인원은 자신의 콜백을 호출한다
        for (int f = fulfillment.Count - 1; f >= 0; f--)
        {
            for (int v = fulfillment[f].Victims.Count - 1; v >= 0; v--)
            {
                for (int p = fulfillment[f].Victims[v].Victim.PCB.Count - 1; p >= 0; p--)
                {
                    fulfillment[f].Victims[v].Victim.PCB[p].Hit(fulfillment[f]);
                }
            }
        }

        //1초의 텀을 줌 나중에 짧아질 수 있음
        yield return new WaitForSeconds(0.5f);
        //다음 단계로 이동
        GameManager.Instance.PipeLine = next;
    }
}

//attack과 hit 콜백은 어떤 인자를 필요로 할까
// 공격할때마다 추가 1대미지
// 공격하는게 누구인가
// 공격이 몇대미지인가
// 공격이 몇회인가

//개인에게 적용되는 효과와 전체에게 적용되는 효과
//두개다 콜백이 필요하지만 파이프라인 콜백에게는 전체가 어울리는데
//일단 콜백을 호출하는건 파이프라인이 알맞으니 개인적인 효과는 개인이 따로 휴대하지만
//호출은 파이프라인이 하는게 맞을듯
//대미지 파이프라인을 따로 만들어야 할듯
//대미지가 어떤 가공과정을 거치는지 


//공격시에 대미지가 어떻게 변하는지 
//공격시에 쉴드에 대미지를 주는 단계
//공격시에 체력에 대미지를 주는 단계

//공격 받을 시에 대미지가 어떻게 변하는지
//공격 받을 시에 쉴드에 대미지를 받는 단계
//공격 받를 시에 체력에 대미지를 받는 단계


//hit before -> attack before -> attack after -> hit after 순이겠다
//그렇다면 파이프라인도 4개로 나눠져 있어야 하겠지
//1 맞기 전 피해자 변화단계
//2 때리기 전 가해자 변화단계
//대미지 처리 2단계 보호막, 체력
//3 때린 후 가해자 변화단계
//4 맞은 후 피해자 변화단계
//총 6개의 기본 파이프라인이 필요함 그중 4개는 null 가능

//그 파이프라인을 통과할 데이터
public class PL_AttackNHit
{
    //누가 공격하는지
    private readonly Character perpetrator;
    public Character Perpetrator { get { return perpetrator; } }
    //누가 공격 당하는지
    private readonly List<AttackInfo> victims;
    public List<AttackInfo> Victims { get { return victims; } }

    private readonly AudioClip clip;
    public AudioClip Clip { get { return clip; } }

    private IShieldChange shieldChange;
    public IShieldChange ShieldChange { get { return shieldChange; } set { shieldChange = value; } }
    public void SC()
    {
        for (int i = 0; i < victims.Count; i++)
            shieldChange?.ShieldChange(victims[i]);
    }
    private IHPChange hpChange;
    public IHPChange HPChange { get { return hpChange; } set { hpChange = value; } }
    public void HC()
    {
        for (int i = 0; i < victims.Count; i++)
            hpChange?.HPChange(victims[i]);
    }

    public PL_AttackNHit(Character perpetrator, List<AttackInfo> victims, AudioClip clip)
    {
        this.perpetrator = perpetrator;
        this.victims = victims;
        this.clip = clip;
        shieldChange = new DefaultShieldChange();
        hpChange = new DefaultHPChange();
    }
}

public class AttackInfo
{
    private Character victim;
    public Character Victim { get { return victim; } set { victim = value; } }

    //대미지
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    //보호막으로 막은 수치
    private float defence;
    public float Defence { get { return defence; } set { defence = value; } }

    //남은 보호막 수치
    private float shield;
    public float Shield { get { return shield; } set { shield = value; } }

    //보호막을 관통한 수치
    private float through;
    public float Through { get { return through; } set { through = value; } }

    public AttackInfo(Character victim, float damage)
    {
        this.victim = victim;
        this.damage = damage;
    }
}

//public interface IHitBefore
//{
//    public void HitBefore(AttackInfo pL_AttackNHit);
//}
//public interface IAttackBefore
//{
//    public bool AttackBefore(AttackInfo pL_AttackNHit);
//}

public interface IShieldChange
{
    public void ShieldChange(AttackInfo pL_AttackNHit);
}
public interface IHPChange
{
    public void HPChange(AttackInfo pL_AttackNHit);
}

//public interface IAttackAfter
//{
//    public void AttackAfter(AttackInfo pL_AttackNHit);
//}
//public interface IHitAfter
//{
//    public void HitAfter(AttackInfo pL_AttackNHit);
//}

//기본 파이프라인 2개
public class DefaultShieldChange : IShieldChange
{
    public void ShieldChange(AttackInfo attackInfo)
    {
        if (attackInfo.Damage > attackInfo.Victim.Shield)
        {
            attackInfo.Defence = attackInfo.Victim.Shield;
            attackInfo.Shield = 0;
            attackInfo.Through = attackInfo.Damage - attackInfo.Victim.Shield;
        }
        else
        {
            attackInfo.Defence = attackInfo.Damage;
            attackInfo.Shield = attackInfo.Victim.Shield - attackInfo.Damage;
            attackInfo.Through = 0;
        }
        attackInfo.Victim.Shield = attackInfo.Shield;
    }
}
public class DefaultHPChange : IHPChange
{
    public void HPChange(AttackInfo attackInfo)
    {
        attackInfo.Victim.HP -= attackInfo.Through;
    }
}

//보호막에게 절반의 대미지를 주기
public class ShieldHalfChange : IShieldChange
{
    public void ShieldChange(AttackInfo attackInfo)
    {
        if (attackInfo.Damage * 0.5f > attackInfo.Victim.Shield)
        {
            attackInfo.Defence = attackInfo.Victim.Shield * 2;
            attackInfo.Shield = 0;
            attackInfo.Through = attackInfo.Damage - attackInfo.Victim.Shield * 2;
        }
        else
        {
            attackInfo.Defence = attackInfo.Damage * 0.5f;
            attackInfo.Shield = attackInfo.Victim.Shield - attackInfo.Damage * 0.5f;
            attackInfo.Through = 0;
        }
    }
}