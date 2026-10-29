using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Character
{
    public override string JobName { get { return "마법사"; } }
    //시작 덱
    public override string[] StartingCards { get { return new[] { "Adventurer/OnePlus", "Adventurer/TwoPlus", "Adventurer/TwoPlus", "Adventurer/Refresh" }; } }
}
