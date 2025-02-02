using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRelic
{
    public IRelicState State { get; set; }
    public Transform Transform { get; }
    public Vector3 Position { get; set; }
    public void RelicCreate();
    public void RelicDestroy();
}
