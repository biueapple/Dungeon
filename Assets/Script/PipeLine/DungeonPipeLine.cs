using UnityEngine;

public class DungeonPipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }
    //몇번 던전에 왔는가
    private int count;
    public int Count { get { return count; } }

    private readonly Character[] candidate;
 
    public DungeonPipeLine(IPipeLine next, Character[] candidate)
    {
        this.next = next;
        this.candidate = candidate;
    }

    public void End()
    {
        count++;
    }

    public void Start()
    {
        //몬스터 스폰 후 다음 단계로
        Character character = candidate[Random.Range(0, candidate.Length)];
        int count = Random.Range(1, 3);
        // x-200 -500 -800, y-250
        for (int i = 0; i < count; i++)
        {
            Character c = Object.Instantiate(character, GameManager.Instance.EnemyParent);
            GameManager.Instance.Enemys.Add(c);
            c.transform.localPosition = new Vector3(-500 + i * 300, -250, 0);
        }
        GameManager.Instance.PipeLine = next;
    }

    public void Update() { }
}
