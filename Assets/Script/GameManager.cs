using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //�̱���
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    private GameObject can;
    [SerializeField]
    private GameObject sel;
    [SerializeField]
    private CharacterSelectManager manager;

    private Character select;
    public Character Select { get { return select; } set { select = value; } }

    [SerializeField]
    private GameObject[] selectStep;

    [SerializeField]
    private Transform adventurerParent;
    public Transform AdventurerParent { get { return adventurerParent; } }

    [SerializeField]
    private Transform enemyParent;
    public Transform EnemyParent { get { return enemyParent; } }

    [SerializeField]
    private Store store;
    //[SerializeField]
    //private PowerRelic power;
    //[SerializeField]
    //private DefenseRelic defense;

    //��
    [SerializeField]
    private Dummy dummy;
    public Dummy Dummy { get { return dummy; } }
    //ī������ ���ִ� å
    [SerializeField]
    private GameObject book;
    //������ �����ϰ� ���ִ� ��ư
    [SerializeField]
    private GameObject startButton;
    //������ � ī���ѵ�� �Ұ��� ���ϴ� ����
    [SerializeField]
    private Deck deck;

    [SerializeField]
    private Character[] enemyResources;
    public Character[] EnemyResources { get { return enemyResources; } }

    //���谡�� x : 850 550 250, y :-250
    [SerializeField]
    private List<Character> adventurer;
    public List<Character> Adventurer { get { return adventurer; } }
    //����
    [SerializeField]
    private List<Character> enemys;
    public List<Character> Enemys { get {  return enemys; } }
    //��� ĳ���͵�
    public List<Character> Characters { get { return adventurer.Concat(enemys).ToList(); } }

    //��
    [SerializeField]
    private Hand hand;
    public Hand Hand { get { return hand; } }
    [SerializeField]
    //�� ���� ��ư
    private Image turnEndButton;
    //��ư�� Ȱ��ȭ ���¸� [0] ��Ȱ��ȭ ���¸� [1]
    [SerializeField]
    private Sprite[] buttonSprite;

    private IPipeLine pipeLine;
    public IPipeLine PipeLine 
    { get { return pipeLine; }
        set
        {
            pipeLine?.End();
            pipeLine = value;
            pipeLine?.Start();
        } 
    }

    [SerializeField]
    private Sorting relic;
    public Sorting Relic { get { return relic; } }

    private readonly List<IPCBG> pcb = new ();
    public List<IPCBG> PCB { get { return pcb; } }

    [SerializeField]
    private TextMeshProUGUI textMoney;
    [SerializeField]
    private int money;
    public int Money { get { return money; } set { money = value; textMoney.text = money.ToString(); } }

    [SerializeField]
    private GameObject gameClear;
    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private GameObject setting;
    [SerializeField]
    private GameObject graphic;
    [SerializeField]
    private GameObject sound;

    private void Awake()
    {
        instance = this;
        relic.Size = 100;
        Money = 100;
    }

    private void Update()
    {
        //�������� ������ ����
        pipeLine?.Update();

        //�÷��̾ ��� ī�带 ������ Ȯ���ؾ� ��
        //�� ���ٸ� �׸��� �������� pipeLine ���� ���ٸ� �� ���ᰡ ������
        if (hand.Cards.Count == 0 && pipeLine == null)
        {
            turnEndButton.raycastTarget = true;
            turnEndButton.sprite = buttonSprite[0];
        }
        //���� ���а� �����ְų� �������� pipeLine �ִٸ� �� ���� �Ұ���
        else
        {
            turnEndButton.raycastTarget = false;
            turnEndButton.sprite = buttonSprite[1];
        }
    }

    //����â Ű��
    public void OnButtonSetting()
    {
        setting.SetActive(true);
    }
    //�׷��ȼ���â Ű��
    public void OnButtonGraphic()
    {
        setting.SetActive(false);
        graphic.SetActive(true);
    }
    //�Ҹ�����â Ű��
    public void OnButtonSound()
    {
        setting.SetActive(false);
        sound.SetActive(true);
    }
    //���θ޴��� ���ư���
    public void OnButtonMenu()
    {
        GameInit();
    }
    //�ݱ�
    public void OnButtonClose()
    {
        setting.SetActive(false);
    }
    //��������
    public void OnButtonExit()
    {
        Application.Quit();
    }

    //���� ĳ���� ������ ����
    public void CharacterSelectFinish(CharacterSelect[] characters)
    {
        for (int i = 0; i < selectStep.Length; i++)
        {
            selectStep[i].SetActive(false);
        }
        for (int i = 0; i < characters.Length; i++)
        {
            adventurer.Add(characters[i].Character);
        }
        //å ����
        book.SetActive(true);
        deck.gameObject.SetActive(true);
    }

    //�̰� �ϱ� ���� ĳ���Ϳ� ���� �����ؾ� ��
    public void GameStart()
    {
        //���� �� �ִ��� �����ִ� �ؽ�Ʈ
        textMoney.gameObject.SetActive(true);
        //book �ڽ� startbutton ����
        //å ����
        book.SetActive(false);
        deck.gameObject.SetActive(false);
        //���۹�ư ����
        startButton.SetActive(false);
        //���� ī���ѵ��� ���� �־��ְ�
        List<Card> list = new();
        for (int i = 0; i < deck.CardBoxes.Count; i++)
        {
            list.AddRange(deck.CardBoxes[i].Card);
        }
        dummy.Init(list);

        //��� ĳ���͵��� Ȱ��ȭ
        //���谡�� x : 850 550 250, y :-250
        for (int i = 0; i < adventurer.Count; i++)
        {
            adventurer[i] = Instantiate(adventurer[i], adventurerParent);
            adventurer[i].transform.localPosition = new Vector3(850 - i * 300, -250, 0);
        }

        turnEndButton.gameObject.SetActive(true);
        //���� ���������� ����
        StartPipeLine startPipeLine = new (dummy, enemyResources);
        startPipeLine.Start();
    }

    //� �ݹ���� ����� ������
    //�Ͻ���
    //��ο� o
    //�� ������ ��ü�Ҷ� create o
    //�Ǻμ����� broken o
    //�����Ҷ� defence o
    //�����Ҷ� attack o
    //���ع����� attack
    //�ʱ�ȭ�Ҷ� funtion o
    //������

    //���� �ܰ��� ����Ŭ ����������
    CyclePipeLine cyclePipeLine;
    public CyclePipeLine CyclePipeLine { get { return cyclePipeLine; } }
    public void Cycle()
    {
        cyclePipeLine ??= new(dummy, enemyResources, store, gameClear, gameOver);
        cyclePipeLine.Play.TurnEndButtonPush();
    }
    
    public void GameInit()
    {
        //���� �ʱ�ȭ (�ʱ� ���·� ���ư���)
        //�� �ʱ�ȭ
        Money = 0;
        //�� ������ �ؽ�Ʈ ����
        textMoney.gameObject.SetActive (false);
        //ĳ���� ����â �ʱ�ȭ �� ���̰�
        manager.Init();
        sel.SetActive(true);
        can.SetActive (true);
        //��� ���� ����
        for(int i = 0; i < dummy.Before.Count; i++)
        {
            Destroy(dummy.Before[i].gameObject);
        }
        dummy.Before.Clear();
        for (int i = 0; i < dummy.After.Count; i++)
        {
            Destroy(dummy.After[i].gameObject);
        }
        dummy.After.Clear();
        hand.Cards.Clear();
        //��� ĳ���� ����
        for(int i = 0; i < adventurer.Count; i++)
        {
            Destroy(adventurer[i].gameObject);
        }
        adventurer.Clear();
        for (int i = 0; i < enemys.Count; i++)
        {
            Destroy(enemys[i].gameObject);
        }
        enemys.Clear();
        //�� �ʱ�ȭ
        deck.InitDeck();
        //��� ���� ����
        for(int i = relic.List.Count - 1; i >= 0; i--)
        {
            Relic r = relic.List[i].GetComponent<Relic>();
            r.RelicDestroy();
        }
        //������ ���� �ʱ�ȭ
        cyclePipeLine = null;
    }
}

/// <summary>
/// ���� ���۽��� �ܰ踦 ������ �ִ� ���� ����������
/// </summary>
public class StartPipeLine
{
    private readonly DungeonPipeLine dungeon;
    private readonly CreatePipeLine plate;
    private readonly DrawPipeLine draw;
    //��� ĳ������ plate�� �����ϰ� ���� �Ǿ��ٸ� ��ο�

    public StartPipeLine(Dummy dummy, Character[] enemys)
    {
        draw = new DrawPipeLine(null, dummy, 5);
        plate = new CreatePipeLine(draw);
        dungeon = new DungeonPipeLine(plate, enemys);
    }

    public void Start()
    {
        GameManager.Instance.PipeLine = dungeon;  
    }
}

/// <summary>
/// ���� �ܰ踦 ���� ������ �ִ� ���� ����������
/// </summary>
public class CyclePipeLine
{
    private readonly DefencePipeLine defense;
    public DefencePipeLine Defence { get { return defense; } }
    private readonly AttackPipeLine attack;
    public AttackPipeLine Attack { get { return attack; } }
    private readonly FuntionPipeLine funtion;
    public FuntionPipeLine Function { get { return funtion; } }
    private readonly BrokenPipeLine broken;
    public BrokenPipeLine Broken { get { return broken; } }

    //����� ĳ���Ͱ� �ִ��� ���� ���̶�� ���� ����������
    private readonly DisposalPipeLine disposal;
    public DisposalPipeLine DisposalPipeLine { get { return disposal; } }

    private readonly CreatePipeLine create;
    public CreatePipeLine Create { get { return create; } }
    private readonly DrawPipeLine draw;
    public DrawPipeLine Draw { get { return draw; } }

    private readonly PlayPipeLine play;
    public PlayPipeLine Play { get { return play; } }

    private readonly DungeonPipeLine dungeon;
    private readonly StorePipeLine store;
    private readonly GameClearPipeLine gameClearPipeLine;
    public CyclePipeLine(Dummy dummy, Character[] enemys, Store store, GameObject clear, GameObject over)
    {
        //���⼭ play�� defense�� ���� ���� ���� defence�� null�� ���¶�
        play = new PlayPipeLine(defense);
        draw = new DrawPipeLine(play, dummy, 5);
        create = new CreatePipeLine(draw);

        dungeon = new DungeonPipeLine(create, enemys, 1);
        this.store = new StorePipeLine(dungeon, store);
        disposal = new DisposalPipeLine(create, new GameOverPipeLine(over), this.store, dungeon);

        broken = new BrokenPipeLine(disposal);
        funtion = new FuntionPipeLine(broken);
        attack = new AttackPipeLine(funtion);
        defense = new DefencePipeLine(attack);
        //�׷��� ���⼭ �ٽ� �־��ֱ�
        play.Next = defense;

        gameClearPipeLine = new GameClearPipeLine(clear);
    }

    public void Start()
    {
        GameManager.Instance.PipeLine = defense;
        if(dungeon.Count >= 1)
        {
            //���� Ŭ����
            disposal.Next = gameClearPipeLine;
        }
    }
}