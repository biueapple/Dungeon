using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalPipeLine : IPipeLine
{
    //아직 살아있는 적이 있다
    private readonly IPipeLine yet;
    //적이 전부 죽음
    private readonly StorePipeLine store;
    private readonly DungeonPipeLine dungeon;
    private IPipeLine next;
    //모험가가 전부 죽음
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
        //일부만 죽었더라도 죽은 캐릭터의 처리는 여기서 해주는걸로
        //살아있고 죽어있는 모든 캐릭터를 체크
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

        //일단 모험가가 모두 죽었다면 게임오버
        if (GameManager.Instance.Adventurer.Count <= 0)
        {
            GameManager.Instance.PipeLine = end;
        }
        //모든 적이 죽었다면 다음단계
        else if (GameManager.Instance.Enemys.Count <= 0)
        {
            GameManager.Instance.PipeLine = next;
            if (next == store)
                next = dungeon;
            else if(next == dungeon)
                next = store;
        }
        //아직 전투중
        else
        {
            GameManager.Instance.PipeLine = yet;
        }
    }

    public void Update()
    {

    }
}
