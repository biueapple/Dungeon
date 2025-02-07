using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InjuryCreator : IPCBC
{
    //���ظ� ���� ������
    private readonly Character victim;
    //�󸶳� ��������
    private readonly int count;

    public InjuryCreator(Character victim, int count)
    {
        this.victim = victim;
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
        Injury injury = Object.Instantiate(Resources.Load<Injury>("Debuff/Injury"), victim.StatusEffect.transform);
        injury.Init(victim, count);
        victim.PCB.Add(injury);
        victim.PCB.Remove(this);
        victim.StatusEffect.Add(injury.transform);
    }
}

public class Injury : MonoBehaviour, IPCBC, IDebuff, IInstruction, IPointerEnterHandler, IPointerExitHandler
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

    public void Init(Character victim, int count)
    {
        //Ƚ���� 0ȸ�� �ٷ� ���������
        if (count == 0)
        {
            Delete();
        }
        damage = 0;
        this.victim = victim;
        this.count = count;
    }

    //���ݽÿ� ������� �ش�
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
        Debug.Log($"{victim} �λ� {count} �ϵ���");

        new IndividualBroken(victim);

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
        return new string($"�λ� {count} �� ����");
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
