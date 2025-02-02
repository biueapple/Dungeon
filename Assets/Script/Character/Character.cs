using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //애니메이션
    [SerializeField]
    protected PlayAnimation animator;
    //자신만의 플레이트를 3개정도 가져야 함
    [SerializeField]
    protected Plate[] plates;
    public Plate[] Plates { get { return plates; } }
    //지금 사용중인 플레이트
    protected Plate plate;
    public Plate Plate { get { return plate; } }
    //사용중인 플레이트가 최종적으로 어느 localposition에 도착해야 하는지 (모든 캐릭터들이 통일되있지 않아 위치가 제각각임)
    [SerializeField]
    protected Vector3 platePosition;
    public Vector3 PlatePosition { get {  return platePosition; } }

    [SerializeField]
    private Sorting statusEffect;
    public Sorting StatusEffect { get { return statusEffect; } }

    //hp를 표시할 ui
    [SerializeField]
    protected Image hpImage;

    //남은 체력
    [SerializeField]
    protected float hp;
    public float HP 
    { get { return hp; }
        set
        {
            Debug.Log(value);
            if (value < hp)
            {
                if (value <= 0)
                    animator.SetTrigger("Dead");
                else
                    animator.SetTrigger("Hit");
                DamageView.Instance.View(this, Color.red, hp - value);
            }
            else if(value > hp)
            {
                //회복
                DamageView.Instance.View(this, Color.green, value - hp);
            }
            hp = value;
            hpImage.fillAmount = hp / hpMax;
        }
    }

    //최대 체력
    [SerializeField]
    protected float hpMax;
    public float HPMax { get { return hpMax; } }

    //남은 보호막
    [SerializeField]
    private float shield;
    public float Shield 
    { get { return shield; } 
        set 
        {
            if (value < shield)
            {
                animator.SetTrigger("Hit");
                DamageView.Instance.View(this, Color.gray, shield - value);
            }
                
            shield = value; 
        }
    }
    //애니메이션을 재생하지 않는 쉴드 초기화
    public void ResetShield()
    {
        shield = 0;
    }
    //캐릭터 개인의 콜백
    private readonly List<IPCBC> pcb = new();
    public List<IPCBC> PCB { get { return pcb; } }

    [SerializeField]
    private int money;
    public int Money { get { return money; } }

    void Awake()
    {
        //체력조정
        hp = hpMax;
        //모든 플레이트들은 주인이 누구인지 넣어줘야 함
        plates.ToList().ForEach(p => p.Self = this);
        //상태이상의 아이콘 크기
        statusEffect.Size = 50;
        //매 턴이 끝날때 쉴드를 0으로 만들기
        pcb.Add(new ShieldReset(this));
    }
    //공격할때 애니메이션을 불러야 하는데
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    //자신이 가진 플레이트중 랜덤으로 하나를 선택
    private void PlateSelect()
    {
        if(plates.Length == 0)
        {
            Debug.Log("캐릭터가 소지한 플레이트가 하나도 없음");
            return;
        }
        plate = plates[Random.Range(0, plates.Length)];
    }

    public IEnumerator CreateC()
    {
        //플레이트 선택
        if(plate != null)
        {
            Debug.Log("이미 만들어진 플레이트가 있어서 생성 불가능");
            yield break;
        }
        PlateSelect();
        if(plate == null)
        {
            Debug.Log("선택된 플레이트가 없기에 생성 불가능");
            yield break;
        }
            
        //플레이트
        RectTransform rectTransform = plate.GetComponent<RectTransform>();
        //힘
        Vector3 velocity = new();
        //플레이트의 초기 위치 생성될때 화면 위에서 내려오게 하기 위해
        rectTransform.position = new Vector3(rectTransform.position.x, Screen.height + 200, 0);
        //각도 초기화 (파괴시에 각도가 회전해서)
        rectTransform.eulerAngles = Vector3.zero;
        //활성화
        rectTransform.gameObject.SetActive(true);
        //아직 목표 위치보다 높다면
        while (rectTransform.localPosition.y > platePosition.y)
        {
            //중력의 영향을 받아 떨어지도록
            velocity += new Vector3(0, -9.8f, 0) * Time.deltaTime;
            rectTransform.position += velocity;
            yield return null;
        }
        //목표 위치 도달
        rectTransform.localPosition = platePosition;
    }

    public IEnumerator BrokenC()
    {
        if (plate == null)
        {
            Debug.Log("선택된 플레이트가 없기에 파괴 불가능");
            yield break;
        }
        RectTransform rectTransform = plate.GetComponent<RectTransform>();
        plate = null;
        //최초에는 위로 향하는 힘도 있어야 함 (잠깐 위로 올라가다 떨어지게 하기 위함)
        Vector3 velocity = new(0, 1.5f, 0);

        //위치 + 200이 화면 아래보다 크다면 (아직 플레이트가 화면에 보인다면)
        while (rectTransform.position.y + 200 >= 0)
        {
            //중력의 영향을 받아 떨어지도록
            velocity += new Vector3(0, -9.8f, 0) * Time.deltaTime;
            rectTransform.position += velocity;
            //회전하도록
            rectTransform.eulerAngles += new Vector3(0, 0, Time.deltaTime * 25);
            yield return null;
        }
        //목표위치에 도달
        //비활성화
        rectTransform.gameObject.SetActive(false);
    }
}
