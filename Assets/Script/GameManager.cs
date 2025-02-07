using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //싱글턴
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

    //덱
    [SerializeField]
    private Dummy dummy;
    public Dummy Dummy { get { return dummy; } }
    //카드팩이 모여있는 책
    [SerializeField]
    private GameObject book;
    //게임을 시작하게 해주는 버튼
    [SerializeField]
    private GameObject startButton;
    //게임을 어떤 카드팩들로 할건지 정하는 공간
    [SerializeField]
    private Deck deck;

    [SerializeField]
    private Character[] enemyResources;
    public Character[] EnemyResources { get { return enemyResources; } }

    //모험가들 x : 850 550 250, y :-250
    [SerializeField]
    private List<Character> adventurer;
    public List<Character> Adventurer { get { return adventurer; } }
    //적들
    [SerializeField]
    private List<Character> enemys;
    public List<Character> Enemys { get {  return enemys; } }
    //모든 캐릭터들
    public List<Character> Characters { get { return adventurer.Concat(enemys).ToList(); } }

    //손
    [SerializeField]
    private Hand hand;
    public Hand Hand { get { return hand; } }
    [SerializeField]
    //턴 종료 버튼
    private Image turnEndButton;
    //버튼이 활성화 상태면 [0] 비활성화 상태면 [1]
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
        //진행중인 파이프 라인
        pipeLine?.Update();

        //플레이어가 모든 카드를 썻는지 확인해야 함
        //다 썻다면 그리고 진행중인 pipeLine 또한 없다면 턴 종료가 가능함
        if (hand.Cards.Count == 0 && pipeLine == null)
        {
            turnEndButton.raycastTarget = true;
            turnEndButton.sprite = buttonSprite[0];
        }
        //아직 손패가 남아있거나 진행중인 pipeLine 있다면 턴 종료 불가능
        else
        {
            turnEndButton.raycastTarget = false;
            turnEndButton.sprite = buttonSprite[1];
        }
    }

    //설정창 키기
    public void OnButtonSetting()
    {
        setting.SetActive(true);
    }
    //그래픽설정창 키기
    public void OnButtonGraphic()
    {
        setting.SetActive(false);
        graphic.SetActive(true);
    }
    //소리설정창 키기
    public void OnButtonSound()
    {
        setting.SetActive(false);
        sound.SetActive(true);
    }
    //메인메뉴로 돌아가기
    public void OnButtonMenu()
    {
        GameInit();
    }
    //닫기
    public void OnButtonClose()
    {
        setting.SetActive(false);
    }
    //게임종료
    public void OnButtonExit()
    {
        Application.Quit();
    }

    //아직 캐릭터 생성은 안함
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
        //책 열고
        book.SetActive(true);
        deck.gameObject.SetActive(true);
    }

    //이거 하기 전에 캐릭터와 적을 생성해야 함
    public void GameStart()
    {
        //돈이 얼마 있는지 보여주는 텍스트
        textMoney.gameObject.SetActive(true);
        //book 자신 startbutton 끄기
        //책 끄고
        book.SetActive(false);
        deck.gameObject.SetActive(false);
        //시작버튼 끄고
        startButton.SetActive(false);
        //골라던 카드팩들을 덱에 넣어주고
        List<Card> list = new();
        for (int i = 0; i < deck.CardBoxes.Count; i++)
        {
            list.AddRange(deck.CardBoxes[i].Card);
        }
        dummy.Init(list);

        //모든 캐릭터들을 활성화
        //모험가들 x : 850 550 250, y :-250
        for (int i = 0; i < adventurer.Count; i++)
        {
            adventurer[i] = Instantiate(adventurer[i], adventurerParent);
            adventurer[i].transform.localPosition = new Vector3(850 - i * 300, -250, 0);
        }

        turnEndButton.gameObject.SetActive(true);
        //시작 파이프라인 진행
        StartPipeLine startPipeLine = new (dummy, enemyResources);
        startPipeLine.Start();
    }

    //어떤 콜백들이 만들어 졌는지
    //턴시작
    //드로우 o
    //새 판으로 교체할때 create o
    //판부셔질떄 broken o
    //수비할때 defence o
    //공격할때 attack o
    //피해받을때 attack
    //초기화할때 funtion o
    //턴종료

    //전투 단계의 사이클 파이프라인
    CyclePipeLine cyclePipeLine;
    public CyclePipeLine CyclePipeLine { get { return cyclePipeLine; } }
    public void Cycle()
    {
        cyclePipeLine ??= new(dummy, enemyResources, store, gameClear, gameOver);
        cyclePipeLine.Play.TurnEndButtonPush();
    }
    
    public void GameInit()
    {
        //게임 초기화 (초기 상태로 돌아가기)
        //돈 초기화
        Money = 0;
        //돈 얼마인지 텍스트 끄기
        textMoney.gameObject.SetActive (false);
        //캐릭터 선택창 초기화 후 보이게
        manager.Init();
        sel.SetActive(true);
        can.SetActive (true);
        //모든 손패 삭제
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
        //모든 캐릭터 삭제
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
        //덱 초기화
        deck.InitDeck();
        //모든 보석 삭제
        for(int i = relic.List.Count - 1; i >= 0; i--)
        {
            Relic r = relic.List[i].GetComponent<Relic>();
            r.RelicDestroy();
        }
        //파이프 라인 초기화
        cyclePipeLine = null;
    }
}

/// <summary>
/// 게임 시작시의 단계를 가지고 있는 통합 파이프라인
/// </summary>
public class StartPipeLine
{
    private readonly DungeonPipeLine dungeon;
    private readonly CreatePipeLine plate;
    private readonly DrawPipeLine draw;
    //모든 캐릭터의 plate를 세팅하고 전부 되었다면 드로우

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
/// 전투 단계를 전부 가지고 있는 통합 파이프라인
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

    //사망한 캐릭터가 있는지 만약 적이라면 다음 스테이지로
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
        //여기서 play에 defense의 값이 들어가지 않음 defence는 null인 상태라
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
        //그래서 여기서 다시 넣어주기
        play.Next = defense;

        gameClearPipeLine = new GameClearPipeLine(clear);
    }

    public void Start()
    {
        GameManager.Instance.PipeLine = defense;
        if(dungeon.Count >= 1)
        {
            //게임 클리어
            disposal.Next = gameClearPipeLine;
        }
    }
}