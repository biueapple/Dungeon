using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//장착한 모험가가 주는 쉴드량 증가
public class DefenseRelic : Relic, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //상점 가격 (카드 가치의 합)
    public override int Price => 4;

    public override void RelicCreate(Character owner)
    {
        DefenseRelic relic = Instantiate(Resources.Load<DefenseRelic>("DefenceRelic"), owner.StatusEffect.transform);
        relic.Equip(owner);
    }

    public override void Attack(PL_AttackNHit pl) { }
    public override void Broken() { }
    public override void Create() { }
    public override void Defence(PL_ShieldNHeal pl) 
    {
        for (int i = 0; i < pl.Victims.Count; i++)
        {
            //장착한 모험가의 콜백이라 보호막을 주는 캐릭터는 항상 owner
            if (pl.Victims[i].Shield > 0)
            {
                pl.Victims[i].Shield += 1;
            }
        }
    }
    public override void Function() { }
    public override void Hit(PL_AttackNHit pl) { }

    public override string Instruction()
    {
        return new string($"장착한 모험가가 보호막을 생성할때 1보호막 추가");
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
}
