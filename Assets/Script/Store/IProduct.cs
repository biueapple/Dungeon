using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduct
{
    public int Price { get; }
    public bool Buy();
}
