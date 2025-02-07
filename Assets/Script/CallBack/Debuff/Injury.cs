using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InjuryCreator : IPCBC
{
    //피해를 받을 피해자
    private readonly Character victim;
    //얼마나 지속할지
    private readonly int count;

    public InjuryCreator(Character victim, int count)
    {
        this.victim = victim;
        this.count = count;
    }

    //공격시에 대미지를 준다
    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Function() { }

    public void Hit(PL_AttackNHit pl)
    {
        Injury injury = Object.Instantiate(Resources.Load<Injury>("Debuff/Injury"), victim.StatusEffect.transform);
        injury.Init(victim, count);
        victim.PCB.Add(injury);
        victim.PCB.Remove(this);
        victim.StatusEffect.Add(injury.transform);
    }
}

public class Injury : MonoBehaviour, IPCBC, IDebuff, IInstruction, IPointerEnterHandler, IPointerExitHandler
{
    //피해를 받을 피해자
    private Character victim;
    public Character Victim { get { return victim; } set { victim = value; } }
    //대미지
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }
    //얼마나 지속할지
    private int count;
    public int Count { get { return count; } set { count = value; } }
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public void Init(Character victim, int count)
    {
        //횟수가 0회면 바로 사라지도록
        if (count == 0)
        {
            Delete();
        }
        damage = 0;
        this.victim = victim;
        this.count = count;
    }

    //공격시에 대미지를 준다
    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create()
    {
        Invocation();
    }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }

    public void Invocation()
    {
        Debug.Log($"{victim} 부상 {count} 턴동안");

        new IndividualBroken(victim);

        //횟수 차감
        count--;
        //횟수가 0이면 삭제
        if (count <= 0)
        {
            Delete();
        }
    }
    public void Delete()
    {
        victim.PCB.Remove(this);
        victim.StatusEffect.Remove(transform);
        Destroy(gameObject);
    }

    public string Instruction()
    {
        return new string($"부상 {count} 턴 남음");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Zoom.Insatnce.Instruction(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Zoom.Insatnce.InstructionOff();
    }
}
