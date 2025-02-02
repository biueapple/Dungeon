using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState_Card
{
    public void Enter();
    public void Exit();
    public void OnPointerEnter();
    public void OnPointerExit();
    public void OnPointerUp();
    public void OnPointerDown();
}
