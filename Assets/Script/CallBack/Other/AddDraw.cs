using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDraw : IPCBG
{
    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Draw()
    {
        //��ο� �ܰ踦 ã�� Ƚ���� �ø�
        DrawPipeLine pipeLine = GameManager.Instance.CyclePipeLine.Draw;

        if (pipeLine != null)
        {
            pipeLine.Count--;
        }

        //�ݹ� ����
        GameManager.Instance.PCB.Remove(this);
    }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }
    public void TurnEnd() { }
    public void TurnStart() { } 
}
