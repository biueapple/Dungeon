using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Relic : MonoBehaviour, IPCBG, IInstruction, IProduct
{
    protected IRelicState state;
    public IRelicState State { get => state; set => state = value; }
    public Transform Transform { get { return transform; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

    public abstract int Price { get; }

    public abstract void RelicCreate();
    public abstract void RelicDestroy();

    public abstract void Attack(PL_AttackNHit pl);
    public abstract void Broken();
    public abstract void Create();
    public abstract void Defence(PL_ShieldNHeal pl);
    public abstract void Draw();
    public abstract void Function();
    public abstract void Hit(PL_AttackNHit pl);
    public abstract void TurnEnd();
    public abstract void TurnStart();

    public abstract string Instruction();

    public abstract bool Buy();
}
