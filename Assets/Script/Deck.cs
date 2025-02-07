using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

//å�� �ִ� ī���ѵ��� �ڽ��� ������ ����� �͵��� �����ϴ� ��� (�Ͻ������� �����鶧 �����ʿ� �ִ� �� ����Ʈ������)
public class Deck : MonoBehaviour
{
    //ī�尡 2�� ����ִ� ī��ڽ�
    [SerializeField]
    private CardBoxTwo prefab2;
    //ī�尡 3�� ����ִ� ī��ڽ� (������� �������� ����)
    [SerializeField]
    private CardBoxThree prefab3;

    //ī������ ������ ��ġ
    [SerializeField]
    private RectTransform contant;
    //���ӽ��۹�ư (Ȱ��ȭ�� ������ ����)
    [SerializeField]
    private GameObject startButton;
    //���� ���� �־����� �����ֱ� ���� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI count;

    //���ĵǾ� �ִ� ī��ڽ���
    private List<CardBox> cardBoxes;
    public List<CardBox> CardBoxes { get { return cardBoxes; } }
    //������ ��ġ
    private float y;

    private void Start()
    {
        cardBoxes = new ();
        y = 0;
    }

    //ī������ �ֱ�
    public bool InputCard(CardPack pack)
    {
        //ī���ѿ� ī�尡 �¥������
        switch(pack.Cards.Count)
        {
            //2��¥���� ���
            case 2:
                //�ΰ�¥�� ī���� �ڽ��� �����
                CardBoxTwo two = Instantiate(prefab2, contant);
                //�ʱ�ȭ (���� ī�带 ������ �ִ���, �ٽ� �������� deck�� ��������� ��)
                two.Init(pack.Cards[0], pack.Cards[1], this);
                //��ġ ����
                two.transform.localPosition = new Vector3(0, y + contant.rect.yMax - two.Size, 0);
                //�����ؾ� �ϴ� ��ġ �ٽ� �����ְ�
                y -= two.Size * 2;
                //����Ʈ�� �߰�
                cardBoxes.Add(two);
                break;
            case 3:
                //�ΰ�¥�� ī���� �ڽ��� �����
                CardBoxThree three = Instantiate(prefab3, contant);
                //�ʱ�ȭ (���� ī�带 ������ �ִ���, �ٽ� �������� deck�� ��������� ��)
                three.Init(pack.Cards[0], pack.Cards[1], pack.Cards[2], this);
                //��ġ ����
                three.transform.localPosition = new Vector3(0, y + contant.rect.yMax - three.Size, 0);
                //�����ؾ� �ϴ� ��ġ �ٽ� �����ְ�
                y -= three.Size * 2;
                //����Ʈ�� �߰�
                cardBoxes.Add(three);
                break;
            default:
                break;
        }

        //���� ���� �������� üũ
        StartCheck();
        return true;
    }

    //ī��ڽ��� deck���� ������
    public void OutputCard(CardBox box)
    {
        //���° ����Ʈ����
        int index = cardBoxes.IndexOf(box);
        //�ڽ��� ũ�⸸ŭ �ٽ� ������ ��� ���� �ؾ��ϴ����� ���� ���̸� �����ϰ�
        y += box.Size * 2;
        //����Ʈ���� ���ְ�
        cardBoxes.RemoveAt(index);
        //�ڽ� ����
        Destroy(box.gameObject);
        //����Ʈ ���� ������� ä��� �ٽ� ����
        for(int i = index; i < cardBoxes.Count; i++)
        {
            cardBoxes[i].transform.position += new Vector3(0, box.Size * 2, 0);
        }
        //���� �������� üũ
        StartCheck();
    }

    //��� ī���� ����
    public void InitDeck()
    {
        for (int i = 0; i < cardBoxes.Count; ++i)
        {
            OutputCard(cardBoxes[i]);
        }
    }

    //���� �������� Ȯ��
    private void StartCheck()
    {
        //��� ī��ڽ� ���� ī����� ���ڸ� ���ļ�
        int sum = cardBoxes.Sum(x => x.Card.Length);
        count.text = sum + " / 10";
        //10���� ���ų� ������ ���ӽ��� ����
        if (sum >= 10)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
}
