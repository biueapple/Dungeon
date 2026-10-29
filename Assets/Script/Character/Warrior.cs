using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : Character
{
    public override string JobName { get { return "전사"; } }
    //시작 덱
    public override string[] StartingCards { get { return new[] { "Adventurer/OnePlus", "Adventurer/OnePlus", "Adventurer/TwoPlus", "Adventurer/Rebuild" }; } }
}
