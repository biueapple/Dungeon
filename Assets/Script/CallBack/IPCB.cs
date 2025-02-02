using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPCBG
{
    //턴시작
    public void TurnStart();
    //새 판으로 교체한 후 create
    public void Create();
    //판부셔진 후 broken
    public void Broken();
    //수비하기전 defence
    public void Defence(PL_ShieldNHeal pl);
    //공격하기전 attack
    public void Attack(PL_AttackNHit pl);
    //공격한 후 attack
    public void Hit(PL_AttackNHit pl);
    //초기화한 후 function
    public void Function();
    //드로우 한 후
    public void Draw();
    //턴종료
    public void TurnEnd();
}

public interface IPCBC
{
    //새 판으로 교체한 후 create
    public void Create();
    //판부셔진 후 broken
    public void Broken();
    //수비하기전 defence
    public void Defence(PL_ShieldNHeal pl);
    //공격하기전 attack
    public void Attack(PL_AttackNHit pl);
    //공격한 후 attack
    public void Hit(PL_AttackNHit pl);
    //초기화한 후 function
    public void Function();
}