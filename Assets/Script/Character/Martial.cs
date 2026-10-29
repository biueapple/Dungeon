using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martial : Character
{
    public override string JobName { get { return "무도가"; } }
    //시작 덱
    public override string[] StartingCards { get { return new[] { "Adventurer/OnePlus", "Adventurer/OnePlus", "Adventurer/TwoPlus", "Adventurer/OneMinus", "Adventurer/Refresh" }; } }
}
