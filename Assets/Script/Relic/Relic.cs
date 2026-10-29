using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//렐릭(보석)은 모험가 한명에게 장착되어 그 모험가의 콜백으로 동작함
public abstract class Relic : MonoBehaviour, IPCBC, IInstruction, IProduct
{
    protected IRelicState state;
    public IRelicState State { get => state; set => state = value; }
    public Transform Transform { get { return transform; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

    //상점 가격 (비용으로 내야 하는 카드 가치의 합)
    public abstract int Price { get; }

    //장착한 모험가
    protected Character owner;
    public Character Owner { get { return owner; } }

    //owner에게 새 렐릭을 만들어 장착
    public abstract void RelicCreate(Character owner);

    //캐릭터에게 장착 (캐릭터의 콜백에 추가하고 상태이상 칸에 아이콘 표시)
    protected void Equip(Character owner)
    {
        this.owner = owner;
        state = new RelicBag(this);
        owner.PCB.Add(this);
        //상태이상 아이콘 크기에 맞추기
        RectTransform rect = (RectTransform)transform;
        if (rect.rect.width > 0)
            transform.localScale = Vector3.one * (owner.StatusEffect.Size / rect.rect.width);
        owner.StatusEffect.Add(transform);
    }

    //장착 해제 후 삭제
    public void RelicDestroy()
    {
        if (owner != null)
        {
            owner.PCB.Remove(this);
            owner.StatusEffect.Remove(transform);
        }
        Destroy(gameObject);
    }

    public abstract void Attack(PL_AttackNHit pl);
    public abstract void Broken();
    public abstract void Create();
    public abstract void Defence(PL_ShieldNHeal pl);
    public abstract void Function();
    public abstract void Hit(PL_AttackNHit pl);

    public abstract string Instruction();
}
