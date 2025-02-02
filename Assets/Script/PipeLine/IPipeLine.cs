using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPipeLine
{
    public IPipeLine Next { get; set; }
    public void Start();
    public void Update();
    public void End();
}
