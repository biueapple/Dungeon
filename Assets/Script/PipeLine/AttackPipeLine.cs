using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

//��� ĳ���͵��� ������ �����ϴ� �ܰ�
public class AttackPipeLine : IPipeLine
{
    //���� �ܰ�
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //������ ������ ĳ���͵�
    private readonly List<PL_AttackNHit> fulfillment;
    public List<PL_AttackNHit> Fulfillment { get { return fulfillment; } }

    public AttackPipeLine(IPipeLine next)
    {
        this.next = next;
        fulfillment = new();
    }

    public void Start()
    {
        //��� ĳ���͵���
        List<Character> list = GameManager.Instance.Characters;
        //������ ���������� ������ ������ �ϴ� �ֵ鸸�� characters����Ʈ�� �߰���
        //������ ���� �ϴ°� �ƴ϶� ���ݿ� ���� ������ characters����Ʈ�� �߰��ϴ°�
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
        //������ ������ ����Ʈ �ʱ�ȭ
        fulfillment.Clear();
    }

    private IEnumerator NextPipe()
    {
        //���������� �ݹ鸮��Ʈ�� ����ִ� ��� �ݹ��� ������ ������ ĳ���͸� �־� ����

        //���� ����������
        //������ (�ִϸ��̼�)
        fulfillment.ForEach(f => f.Perpetrator.AttackAnimation());
        //���� �Ҹ�
        fulfillment.Select(f => f.Clip).Distinct().ToList()
        .ForEach(clip => SoundManager.Instance.Play(clip));
        //���� �� 1�� (���� �ִϸ��̼��� ����� �ð�)

        //�ݹ�
        //������ ������ ��� �ο��� �ڽ��� �ݹ��� ȣ���Ѵ�
        for (int f = fulfillment.Count - 1; f >= 0; f--)
        {
            for (int p = fulfillment[f].Perpetrator.PCB.Count - 1; p >= 0; p--)
            {
                fulfillment[f].Perpetrator.PCB[p].Attack(fulfillment[f]);
            }
        }
        

        yield return new WaitForSeconds(0.5f);
        //�±�
        fulfillment.ForEach(f => f.SC());
        fulfillment.ForEach(f => f.HC());

        //�´� �Ҹ� (����)

        //�ݹ�
        //������ ������ ��� �ο��߿� ������ ���� ��� �ο��� �ڽ��� �ݹ��� ȣ���Ѵ�
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

        //1���� ���� �� ���߿� ª���� �� ����
        yield return new WaitForSeconds(0.5f);
        //���� �ܰ�� �̵�
        GameManager.Instance.PipeLine = next;
    }
}

//attack�� hit �ݹ��� � ���ڸ� �ʿ�� �ұ�
// �����Ҷ����� �߰� 1�����
// �����ϴ°� �����ΰ�
// ������ �������ΰ�
// ������ ��ȸ�ΰ�

//���ο��� ����Ǵ� ȿ���� ��ü���� ����Ǵ� ȿ��
//�ΰ��� �ݹ��� �ʿ������� ���������� �ݹ鿡�Դ� ��ü�� ��︮�µ�
//�ϴ� �ݹ��� ȣ���ϴ°� ������������ �˸����� �������� ȿ���� ������ ���� �޴�������
//ȣ���� ������������ �ϴ°� ������
//����� ������������ ���� ������ �ҵ�
//������� � ���������� ��ġ���� 


//���ݽÿ� ������� ��� ���ϴ��� 
//���ݽÿ� ���忡 ������� �ִ� �ܰ�
//���ݽÿ� ü�¿� ������� �ִ� �ܰ�

//���� ���� �ÿ� ������� ��� ���ϴ���
//���� ���� �ÿ� ���忡 ������� �޴� �ܰ�
//���� �޸� �ÿ� ü�¿� ������� �޴� �ܰ�


//hit before -> attack before -> attack after -> hit after ���̰ڴ�
//�׷��ٸ� ���������ε� 4���� ������ �־�� �ϰ���
//1 �±� �� ������ ��ȭ�ܰ�
//2 ������ �� ������ ��ȭ�ܰ�
//����� ó�� 2�ܰ� ��ȣ��, ü��
//3 ���� �� ������ ��ȭ�ܰ�
//4 ���� �� ������ ��ȭ�ܰ�
//�� 6���� �⺻ ������������ �ʿ��� ���� 4���� null ����

//�� ������������ ����� ������
public class PL_AttackNHit
{
    //���� �����ϴ���
    private readonly Character perpetrator;
    public Character Perpetrator { get { return perpetrator; } }
    //���� ���� ���ϴ���
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

    //�����
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    //��ȣ������ ���� ��ġ
    private float defence;
    public float Defence { get { return defence; } set { defence = value; } }

    //���� ��ȣ�� ��ġ
    private float shield;
    public float Shield { get { return shield; } set { shield = value; } }

    //��ȣ���� ������ ��ġ
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

//�⺻ ���������� 2��
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

//��ȣ������ ������ ������� �ֱ�
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