using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Character
{
    private void Start()
    {
        foreach (var item in plates)
        {
            item.Foe = GameManager.Instance.Enemys;
            item.Ally = GameManager.Instance.Adventurer;
        }
    }
}
