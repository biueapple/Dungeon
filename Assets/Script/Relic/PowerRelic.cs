using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//장착한 모험가가 하는 공격에 1 대미지 추가
public class PowerRelic : Relic, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //상점 가격 (카드 가치의 합)
    public override int Price => 5;

    public override void RelicCreate(Character owner)
    {
        PowerRelic relic = Instantiate(Resources.Load<PowerRelic>("PowerRelic"), owner.StatusEffect.transform);
        relic.Equip(owner);
    }

    public override void Attack(PL_AttackNHit pl)
    {
        for (int i = 0; i < pl.Victims.Count; i++)
        {
            //장착한 모험가의 콜백이라 공격하는 캐릭터는 항상 owner
            pl.Victims[i].Damage += 1;
        }
    }

    public override void Broken() { }
    public override void Create() { }
    public override void Defence(PL_ShieldNHeal pl) { }
    public override void Function() { }
    public override void Hit(PL_AttackNHit pl) { }

    public override string Instruction()
    {
        return new string($"장착한 모험가의 공격에 1대미지 추가");
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
