using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Zoom : MonoBehaviour
{
    //�̱���
    private static Zoom instance;
    public static Zoom Insatnce { get { return instance; } }
    //� ui�� ĵ������ ������ �Ѿ���� Ȯ�ΰ� ���� ������ �̵������ִ� Ŭ����
    [SerializeField]
    private CanvasRect canvasRect;
    //ī���� ū���� (�� �����ٷ���)
    [SerializeField]
    private Card big;
    [SerializeField]
    private Card ebig;
    [SerializeField]
    private Card volatilityCard;
    [SerializeField]
    private Card evolatilityCard;
    [SerializeField]
    private TextMeshProUGUI  textMeshPro;

    //true��� ViewON ������ �����ְ� false��� ViewON�ص� �������� ����
    private bool active = true;
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    //ī�带 ũ�� �����ַ���
    //4���� ��� 
    public void ViewON(Card card)
    {
        if(active)
        {
            if(card.Volatility)
            {
                if (card.Friendly)
                {
                    //��ġ �Ű��ְ�
                    volatilityCard.transform.position = card.transform.position;
                    //big�� card���� Ŀ�� ��ġ�� ������ �ϸ� ĵ���� ������ ������ ��찡 �־ �ٽ� ������ �ǵ����� ����
                    canvasRect.InSide(volatilityCard.GetComponent<RectTransform>());
                    //���� �����ؼ� �����ַ���
                    volatilityCard.Icon = card.Icon;
                    volatilityCard.Description = card.Description;
                    //Ȱ��ȭ
                    volatilityCard.gameObject.SetActive(true);
                }
                else
                {
                    //��ġ �Ű��ְ�
                    evolatilityCard.transform.position = card.transform.position;
                    //big�� card���� Ŀ�� ��ġ�� ������ �ϸ� ĵ���� ������ ������ ��찡 �־ �ٽ� ������ �ǵ����� ����
                    canvasRect.InSide(evolatilityCard.GetComponent<RectTransform>());
                    //���� �����ؼ� �����ַ���
                    evolatilityCard.Icon = card.Icon;
                    evolatilityCard.Description = card.Description;
                    //Ȱ��ȭ
                    evolatilityCard.gameObject.SetActive(true);
                }
            }
            else 
            {
                if (card.Friendly)
                {
                    //��ġ �Ű��ְ�
                    big.transform.position = card.transform.position;
                    //big�� card���� Ŀ�� ��ġ�� ������ �ϸ� ĵ���� ������ ������ ��찡 �־ �ٽ� ������ �ǵ����� ����
                    canvasRect.InSide(big.GetComponent<RectTransform>());
                    //���� �����ؼ� �����ַ���
                    big.Icon = card.Icon;
                    big.Description = card.Description;
                    //Ȱ��ȭ
                    big.gameObject.SetActive(true);
                }
                else
                {
                    //��ġ �Ű��ְ�
                    ebig.transform.position = card.transform.position;
                    //big�� card���� Ŀ�� ��ġ�� ������ �ϸ� ĵ���� ������ ������ ��찡 �־ �ٽ� ������ �ǵ����� ����
                    canvasRect.InSide(ebig.GetComponent<RectTransform>());
                    //���� �����ؼ� �����ַ���
                    ebig.Icon = card.Icon;
                    ebig.Description = card.Description;
                    //Ȱ��ȭ
                    ebig.gameObject.SetActive(true);
                }
            }
        }
    }

    //��Ȱ��ȭ
    public void ViewOFF()
    {
        big.gameObject.SetActive(false);
        ebig.gameObject.SetActive(false);
        volatilityCard.gameObject.SetActive(false);
        evolatilityCard.gameObject.SetActive(false);
    }

    Keyword ex;
    public void ViewTool(Plate plate, int index, Vector3 position)
    {
        if(ex != null)
        {
            ex.Tool.gameObject.SetActive(false);
            ex.Space.ForEach(x => x.gameObject.SetActive(false));
        }
        if (plate == null || index < 0 || index > plate.Keyword.Length - 1)
            return;

        plate.Keyword[index].Tool.gameObject.SetActive(true);
        plate.Keyword[index].Space.ForEach(x => x.gameObject.SetActive(true));
        plate.Keyword[index].Tool.transform.position = position + new Vector3(0, 150, 0);
        canvasRect.InSideChild(plate.Keyword[index].Tool.GetComponent<RectTransform>(), plate.Keyword[index].Tool.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>());
        ex = plate.Keyword[index];
    }

    public void Instruction(IInstruction instruction)
    {
        if(active)
        {
            textMeshPro.gameObject.SetActive (true);
            textMeshPro.transform.position = instruction.Position;
            textMeshPro.text = instruction.Instruction();
            canvasRect.InSide(textMeshPro.GetComponent<RectTransform>());
        }
    }
    public void InstructionOff()
    {
        textMeshPro.gameObject.SetActive(false);
    }
}

public interface IInstruction
{
    public string Instruction();
    public Vector3 Position { get; set; }
}