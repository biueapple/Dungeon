using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReset : IPCBC
{
    private Character character;
    public ShieldReset(Character character)
    {
        this.character = character;
    }

    public void Attack(PL_AttackNHit pl) { }
    public void Broken() { }
    public void Create(){}
    public void Defence(PL_ShieldNHeal pl){}
    public void Function()
    {
        character.ResetShield();
    }
    public void Hit(PL_AttackNHit pl){}
}
