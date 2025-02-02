using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    //테스트용들
    public Card[] card;

    public Dummy dummy;

    public Character[] character;

    private void Start()
    {
        //dummy.Init(card.ToList());
        //for (int i = 0; i < character.Length; i++)
        //{
        //    GameManager.Instance.Adventurer.Add(character[i]);
        //}

        ////판 깔아주고
        //StartPipeLine startPipeLine = new(dummy, GameManager.Instance.EnemyResources);
        //startPipeLine.Start();
    }
}
