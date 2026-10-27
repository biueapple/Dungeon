using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맵을 보여주고 고른 스테이지로 진행하는 단계
public class MapPipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }

    private readonly StageMap map;
    //전투를 시작하는 단계 (첫 전투는 StartPipeLine의 것, 그 다음부터는 CyclePipeLine의 것)
    private DungeonPipeLine dungeon;
    public DungeonPipeLine Dungeon { get => dungeon; set => dungeon = value; }
    //상점 단계 (CyclePipeLine이 만들어질때 들어옴)
    private StorePipeLine store;
    public StorePipeLine Store { get => store; set => store = value; }

    //지금 진행중인 스테이지가 보스인지
    public bool IsBoss { get { return map.Current != null && map.Current.Type == StageType.Boss; } }

    public MapPipeLine(DungeonPipeLine dungeon)
    {
        this.dungeon = dungeon;
        map = StageMap.Create();
    }

    public void Start()
    {
        map.Show(OnSelect);
    }

    private void OnSelect(StageNode node)
    {
        switch (node.Type)
        {
            case StageType.Battle:
            case StageType.Boss:
                dungeon.Level = node.Level;
                next = dungeon;
                break;
            case StageType.Store:
                next = store;
                break;
        }
        GameManager.Instance.PipeLine = next;
    }

    public void Update() { }

    public void End() { }

    //게임 초기화시 맵 삭제
    public void Destroy()
    {
        Object.Destroy(map.gameObject);
    }
}
