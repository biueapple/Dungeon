using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPCBG
{
    //�Ͻ���
    public void TurnStart();
    //�� ������ ��ü�� �� create
    public void Create();
    //�Ǻμ��� �� broken
    public void Broken();
    //�����ϱ��� defence
    public void Defence(PL_ShieldNHeal pl);
    //�����ϱ��� attack
    public void Attack(PL_AttackNHit pl);
    //������ �� attack
    public void Hit(PL_AttackNHit pl);
    //�ʱ�ȭ�� �� function
    public void Function();
    //��ο� �� ��
    public void Draw();
    //������
    public void TurnEnd();
}

public interface IPCBC
{
    //�� ������ ��ü�� �� create
    public void Create();
    //�Ǻμ��� �� broken
    public void Broken();
    //�����ϱ��� defence
    public void Defence(PL_ShieldNHeal pl);
    //�����ϱ��� attack
    public void Attack(PL_AttackNHit pl);
    //������ �� attack
    public void Hit(PL_AttackNHit pl);
    //�ʱ�ȭ�� �� function
    public void Function();
}