using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BleedingCreator : IPCBC
{
    //피해를 받을 피해자
    private readonly Character victim;
    //대미지
    private readonly float damage;
    //얼마나 지속할지
    private readonly int count;

    public BleedingCreator(Character victim, float damage, int count)
    {
        this.victim = victim;
        this.damage = damage;
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
        Bleeding bleeding = Object.Instantiate(Resources.Load<Bleeding>("Bleeding"), victim.StatusEffect.transform);
        bleeding.Init(victim, damage, count);
        victim.PCB.Add(bleeding);
        victim.PCB.Remove(this);
        victim.StatusEffect.Add(bleeding.transform);
    }
}


//캐릭터의 출혈 (캐릭터가 공격할때 피해를 입도록 함)
public class Bleeding : MonoBehaviour, IPCBC, IDebuff, IInstruction, IPointerEnterHandler, IPointerExitHandler
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

    public void Init(Character victim, float damage, int count)
    {
        //횟수가 0회면 바로 사라지도록
        if(count == 0)
        {
            Delete();
        }
            
        this.victim = victim;
        this.damage = damage;
        this.count = count;
    }

    //공격시에 대미지를 준다
    public void Attack(PL_AttackNHit pl)
    {
        Invocation();
    }

    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }

    public void Invocation()
    {
        Debug.Log($"{victim} 출혈 {damage} 대미지");
        //대미지
        victim.HP -= damage;
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
        return new string($"출혈 {damage} 대미지, {count} 턴 남음");
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
