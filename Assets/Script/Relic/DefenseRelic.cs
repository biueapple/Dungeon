using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//모든 모험가가 주는 쉴드량 증가
public class DefenseRelic : MonoBehaviour, IRelic, IPCBG, IInstruction, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private IRelicState state;
    public IRelicState State { get { return state; } set { state = value; } }

    public Transform Transform { get { return transform; } }
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public void RelicCreate()
    {                                                               
        DefenseRelic relic = Instantiate(Resources.Load<DefenseRelic>("DefenceRelic"), GameManager.Instance.Relic.transform);
        GameManager.Instance.PCB.Add(relic);
        GameManager.Instance.Relic.Add(relic.transform);
    }

    public void RelicDestroy()
    {
        GameManager.Instance.PCB.Remove(this);
        GameManager.Instance.Relic.Remove(transform);
        Destroy(gameObject);
    }

    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create() { }
    public void Defence(PL_ShieldNHeal pl) 
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
    public void Draw() { }
    public void Function() { }
    public void Hit(PL_AttackNHit pl) { }
    public void TurnEnd() { }
    public void TurnStart() { }

    public string Instruction()
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

}
