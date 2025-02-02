using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Character
{

    private void Start()
    {
        foreach (var item in plates)
        {
            item.Foe = GameManager.Instance.Adventurer;
            item.Ally = GameManager.Instance.Enemys;
        }
    }
}
