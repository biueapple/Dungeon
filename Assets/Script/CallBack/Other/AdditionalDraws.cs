using System.Collections.Generic;
using UnityEngine;

//�߰������� ��ο츦 �ϴ� �ݹ�
public class AdditionalDraws : IPCBG
{
    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Draw() { }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }
    public void TurnEnd() { }
    public void TurnStart()
    {
        //��ο� �ܰ踦 ã�� Ƚ���� �ø�
        DrawPipeLine pipeLine = GameManager.Instance.CyclePipeLine.Draw;
        
        if (pipeLine != null)
        {
            pipeLine.Count++;
        }

        //�ݹ� ����
        GameManager.Instance.PCB.Remove(this);
    }
}


