using UnityEngine;

public class DungeonPipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }
    //어떤 몬스터 구성으로 할지 (맵에서 고른 스테이지에 따라 정해짐)
    //0 ~ 2 : 일반 전투, 3 : 보스
    private int level = 0;
    public int Level { get { return level; } set { level = value; } }

    private readonly Character[] candidate;
 
    public DungeonPipeLine(IPipeLine next, Character[] candidate)
    {
        this.next = next;
        this.candidate = candidate;
    }

    public void End() { }

    public void Start()
    {
        Character character;
        //몬스터 스폰 후 다음 단계로
        // x-150 -450 -750, y-250
        switch (level)
        {
            case 0:
                //2명만
                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-450, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-150, -250, 0);
                break;
            case 1:
                //3명
                character = Object.Instantiate(candidate[1], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-750, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-450, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-150, -250, 0);
                break;
            case 2:
                //3명
                character = Object.Instantiate(candidate[1], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-750, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-450, -250, 0);

                character = Object.Instantiate(candidate[4], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-150, -250, 0);
                break;
            case 3:
                //3명
                character = Object.Instantiate(candidate[1], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-750, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-450, -250, 0);

                character = Object.Instantiate(candidate[4], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-150, -250, 0);
                break;
        }
        
        GameManager.Instance.PipeLine = next;
    }

    public void Update() { }
}
