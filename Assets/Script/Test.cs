using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    //�׽�Ʈ���
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

        ////�� ����ְ�
        //StartPipeLine startPipeLine = new(dummy, GameManager.Instance.EnemyResources);
        //startPipeLine.Start();
    }
}
