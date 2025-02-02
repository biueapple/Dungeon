using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : Character
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
