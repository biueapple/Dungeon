using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //�ִϸ��̼�
    [SerializeField]
    protected PlayAnimation animator;
    //�ڽŸ��� �÷���Ʈ�� 3������ ������ ��
    [SerializeField]
    protected Plate[] plates;
    public Plate[] Plates { get { return plates; } }
    //���� ������� �÷���Ʈ
    protected Plate plate;
    public Plate Plate { get { return plate; } }
    //������� �÷���Ʈ�� ���������� ��� localposition�� �����ؾ� �ϴ��� (��� ĳ���͵��� ���ϵ����� �ʾ� ��ġ�� ��������)
    [SerializeField]
    protected Vector3 platePosition;
    public Vector3 PlatePosition { get {  return platePosition; } }

    [SerializeField]
    private Sorting statusEffect;
    public Sorting StatusEffect { get { return statusEffect; } }

    //hp�� ǥ���� ui
    [SerializeField]
    protected Image hpImage;

    //���� ü��
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
                //ȸ��
                DamageView.Instance.View(this, Color.green, value - hp);
            }
            hp = value;
            hpImage.fillAmount = hp / hpMax;
        }
    }

    //�ִ� ü��
    [SerializeField]
    protected float hpMax;
    public float HPMax { get { return hpMax; } }

    //���� ��ȣ��
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
    //�ִϸ��̼��� ������� �ʴ� ���� �ʱ�ȭ
    public void ResetShield()
    {
        shield = 0;
    }
    //ĳ���� ������ �ݹ�
    private readonly List<IPCBC> pcb = new();
    public List<IPCBC> PCB { get { return pcb; } }

    [SerializeField]
    private int money;
    public int Money { get { return money; } }

    void Awake()
    {
        //ü������
        hp = hpMax;
        //��� �÷���Ʈ���� ������ �������� �־���� ��
        plates.ToList().ForEach(p => p.Self = this);
        //�����̻��� ������ ũ��
        statusEffect.Size = 50;
        //�� ���� ������ ���带 0���� �����
        pcb.Add(new ShieldReset(this));
    }
    //�����Ҷ� �ִϸ��̼��� �ҷ��� �ϴµ�
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    //�ڽ��� ���� �÷���Ʈ�� �������� �ϳ��� ����
    private void PlateSelect()
    {
        if(plates.Length == 0)
        {
            Debug.Log("ĳ���Ͱ� ������ �÷���Ʈ�� �ϳ��� ����");
            return;
        }
        plate = plates[Random.Range(0, plates.Length)];
    }

    public IEnumerator CreateC()
    {
        //�÷���Ʈ ����
        if(plate != null)
        {
            Debug.Log("�̹� ������� �÷���Ʈ�� �־ ���� �Ұ���");
            yield break;
        }
        PlateSelect();
        if(plate == null)
        {
            Debug.Log("���õ� �÷���Ʈ�� ���⿡ ���� �Ұ���");
            yield break;
        }
            
        //�÷���Ʈ
        RectTransform rectTransform = plate.GetComponent<RectTransform>();
        //��
        Vector3 velocity = new();
        //�÷���Ʈ�� �ʱ� ��ġ �����ɶ� ȭ�� ������ �������� �ϱ� ����
        rectTransform.position = new Vector3(rectTransform.position.x, Screen.height + 200, 0);
        //���� �ʱ�ȭ (�ı��ÿ� ������ ȸ���ؼ�)
        rectTransform.eulerAngles = Vector3.zero;
        //Ȱ��ȭ
        rectTransform.gameObject.SetActive(true);
        //���� ��ǥ ��ġ���� ���ٸ�
        while (rectTransform.localPosition.y > platePosition.y)
        {
            //�߷��� ������ �޾� ����������
            velocity += new Vector3(0, -9.8f, 0) * Time.deltaTime;
            rectTransform.position += velocity;
            yield return null;
        }
        //��ǥ ��ġ ����
        rectTransform.localPosition = platePosition;
    }

    public IEnumerator BrokenC()
    {
        if (plate == null)
        {
            Debug.Log("���õ� �÷���Ʈ�� ���⿡ �ı� �Ұ���");
            yield break;
        }
        RectTransform rectTransform = plate.GetComponent<RectTransform>();
        plate = null;
        //���ʿ��� ���� ���ϴ� ���� �־�� �� (��� ���� �ö󰡴� �������� �ϱ� ����)
        Vector3 velocity = new(0, 1.5f, 0);

        //��ġ + 200�� ȭ�� �Ʒ����� ũ�ٸ� (���� �÷���Ʈ�� ȭ�鿡 ���δٸ�)
        while (rectTransform.position.y + 200 >= 0)
        {
            //�߷��� ������ �޾� ����������
            velocity += new Vector3(0, -9.8f, 0) * Time.deltaTime;
            rectTransform.position += velocity;
            //ȸ���ϵ���
            rectTransform.eulerAngles += new Vector3(0, 0, Time.deltaTime * 25);
            yield return null;
        }
        //��ǥ��ġ�� ����
        //��Ȱ��ȭ
        rectTransform.gameObject.SetActive(false);
    }
}
