using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalPipeLine : IPipeLine
{
    //���� ����ִ� ���� �ִ�
    private readonly IPipeLine yet;
    //���� ���� ����
    private readonly StorePipeLine store;
    private readonly DungeonPipeLine dungeon;
    private IPipeLine next;
    //���谡�� ���� ����
    private readonly IPipeLine end;

    public IPipeLine Next { get => next; set => next = value; }

    public DisposalPipeLine(IPipeLine yet, IPipeLine end, StorePipeLine store, DungeonPipeLine dungeon)
    {
        this.yet = yet;
        this.end = end;
        this.store = store;
        this.dungeon = dungeon;
        next = store;
    }

    public void End()
    {

    }

    public void Start()
    {
        //�Ϻθ� �׾����� ���� ĳ������ ó���� ���⼭ ���ִ°ɷ�
        //����ְ� �׾��ִ� ��� ĳ���͸� üũ
        for (int i = GameManager.Instance.Adventurer.Count - 1; i >= 0; i--)
        {
            if(GameManager.Instance.Adventurer[i].HP <= 0)
            {
                Character character = GameManager.Instance.Adventurer[i];
                GameManager.Instance.Adventurer.RemoveAt(i);
                Object.Destroy(character.gameObject);
            }
        }
        for (int i = GameManager.Instance.Enemys.Count - 1; i >= 0; i--)
        {
            if (GameManager.Instance.Enemys[i].HP <= 0)
            {
                Character character = GameManager.Instance.Enemys[i];
                GameManager.Instance.Money += character.Money;
                GameManager.Instance.Enemys.RemoveAt(i);
                SoundManager.Instance.Play(SoundManager.Instance.Skull);
                Object.Destroy(character.gameObject);
            }
        }

        //�ϴ� ���谡�� ��� �׾��ٸ� ���ӿ���
        if (GameManager.Instance.Adventurer.Count <= 0)
        {
            GameManager.Instance.PipeLine = end;
        }
        //��� ���� �׾��ٸ� �����ܰ�
        else if (GameManager.Instance.Enemys.Count <= 0)
        {
            GameManager.Instance.PipeLine = next;
            if (next == store)
                next = dungeon;
            else if(next == dungeon)
                next = store;
        }
        //���� ������
        else
        {
            GameManager.Instance.PipeLine = yet;
        }
    }

    public void Update()
    {

    }
}
