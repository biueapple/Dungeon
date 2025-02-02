using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martial : Character
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
