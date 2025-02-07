using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//모든 모험가가 주는 쉴드량 증가
public class DefenseRelic : Relic, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public override int Price => 20;

    public override void RelicCreate()
    {                                                               
        DefenseRelic relic = Instantiate(Resources.Load<DefenseRelic>("DefenceRelic"), GameManager.Instance.Relic.transform);
        GameManager.Instance.PCB.Add(relic);
        GameManager.Instance.Relic.Add(relic.transform);
    }

    public override void RelicDestroy()
    {
        GameManager.Instance.PCB.Remove(this);
        GameManager.Instance.Relic.Remove(transform);
        Destroy(gameObject);
    }

    public override void Attack(PL_AttackNHit pl) { }
    public override void Broken() { }
    public override void Create() { }
    public override void Defence(PL_ShieldNHeal pl) 
    {
        for (int i = 0; i < pl.Victims.Count; i++)
        {
            if (GameManager.Instance.Adventurer.Contains(pl.Perpetrator))
            {
                if(pl.Victims[i].Shield > 0)
                {
                    pl.Victims[i].Shield += 1;
                }
            }
        }
    }
    public override void Draw() { }
    public override void Function() { }
    public override void Hit(PL_AttackNHit pl) { }
    public override void TurnEnd() { }
    public override void TurnStart() { }

    public override string Instruction()
    {
        return new string($"모든 모험가가 보호막을 생성할때 1보호막 추가");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        state?.OnMouseEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        state?.OnMouseExit();
    }
     public void OnPointerClick(PointerEventData eventData)
    {
        state?.OnMouseClick();
    }

    public override bool Buy()
    {
        if (GameManager.Instance.Money >= Price)
        {
            RelicCreate();
            GameManager.Instance.Money -= Price;
            return true;
        }
        return false;
    }
}
