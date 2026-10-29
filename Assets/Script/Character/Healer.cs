using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Character
{
    public override string JobName { get { return "힐러"; } }
    //시작 덱
    public override string[] StartingCards { get { return new[] { "Adventurer/OnePlus", "Adventurer/OnePlus", "Adventurer/Rest", "Adventurer/Rebuild" }; } }
}
