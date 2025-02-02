using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BleedingCreator : IPCBC
{
    //���ظ� ���� ������
    private readonly Character victim;
    //�����
    private readonly float damage;
    //�󸶳� ��������
    private readonly int count;

    public BleedingCreator(Character victim, float damage, int count)
    {
        this.victim = victim;
        this.damage = damage;
        this.count = count;
    }

    //���ݽÿ� ������� �ش�
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


//ĳ������ ���� (ĳ���Ͱ� �����Ҷ� ���ظ� �Ե��� ��)
public class Bleeding : MonoBehaviour, IPCBC, IDebuff, IInstruction, IPointerEnterHandler, IPointerExitHandler
{
    //���ظ� ���� ������
    private Character victim;
    public Character Victim { get { return victim; } set { victim = value; } }
    //�����
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }
    //�󸶳� ��������
    private int count;
    public int Count { get { return count; } set { count = value; } }

    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public void Init(Character victim, float damage, int count)
    {
        //Ƚ���� 0ȸ�� �ٷ� ���������
        if(count == 0)
        {
            Delete();
        }
            
        this.victim = victim;
        this.damage = damage;
        this.count = count;
    }

    //���ݽÿ� ������� �ش�
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
        Debug.Log($"{victim} ���� {damage} �����");
        //�����
        victim.HP -= damage;
        //Ƚ�� ����
        count--;
        //Ƚ���� 0�̸� ����
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
        return new string($"���� {damage} �����, {count} �� ����");
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
