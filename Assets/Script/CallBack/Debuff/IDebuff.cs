using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuff
{
    public Character Victim { get; set; }
    public float Damage { get; set; }
    public int Count { get; set; }

    public void Delete();
    public void Invocation();
    //public void Create();
}
