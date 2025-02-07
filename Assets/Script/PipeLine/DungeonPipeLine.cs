using UnityEngine;

public class DungeonPipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }
    //��� ������ �Դ°�
    private int count = 0;
    public int Count { get { return count; } }

    private readonly Character[] candidate;
 
    public DungeonPipeLine(IPipeLine next, Character[] candidate)
    {
        this.next = next;
        this.candidate = candidate;
    }
    public DungeonPipeLine(IPipeLine next, Character[] candidate, int count)
    {
        this.next = next;
        this.candidate = candidate;
        this.count = count;
    }

    public void End()
    {
        count++;
    }

    public void Start()
    {
        Character character;
        //���� ���� �� ���� �ܰ��
        // x-150 -450 -750, y-250
        switch (count)
        {
            case 0:
                //2��
                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-450, -250, 0);

                character = Object.Instantiate(candidate[0], GameManager.Instance.EnemyParent);
                GameManager.Instance.Enemys.Add(character);
                character.transform.localPosition = new Vector3(-150, -250, 0);
                break;
            case 1:
                //3��
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
                //3��
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
                //3��
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
