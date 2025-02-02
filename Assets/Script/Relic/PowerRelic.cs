using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//모든 모험가가 하는 공격에 1 대미지 추가
public class PowerRelic : MonoBehaviour, IRelic, IPCBG, IInstruction, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private IRelicState state;
    public IRelicState State { get { return state; } set { state = value; } }

    public Transform Transform { get { return transform; } }
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public void RelicCreate()
    {
        PowerRelic relic = Instantiate(Resources.Load<PowerRelic>("PowerRelic"), GameManager.Instance.Relic.transform);
        GameManager.Instance.PCB.Add(relic);
        GameManager.Instance.Relic.Add(relic.transform);
    }

    public void RelicDestroy()
    {
        GameManager.Instance.PCB.Remove(this);
        GameManager.Instance.Relic.Remove(transform);
        Destroy(gameObject);
    }

    public void Attack(PL_AttackNHit pl)
    {
        for (int i = 0; i < pl.Victims.Count; i++)
        {
            if(GameManager.Instance.Adventurer.Contains(pl.Perpetrator))
                pl.Victims[i].Damage += 1;
        }
    }

    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) { }
    public void Draw() { }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }
    public void TurnEnd() { }
    public void TurnStart(){ }

    public string Instruction()
    {
        return new string($"모든 모험가의 공격에 1대미지 추가");
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
