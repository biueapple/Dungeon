using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Zoom : MonoBehaviour
{
    //싱글턴
    private static Zoom instance;
    public static Zoom Insatnce { get { return instance; } }
    //어떤 ui가 캔버스의 범위를 넘어갔는지 확인과 범위 안으로 이동시켜주는 클래스
    [SerializeField]
    private CanvasRect canvasRect;
    //카드의 큰버젼 (잘 보여줄려고)
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

    //true라면 ViewON 했을때 보여주고 false라면 ViewON해도 보여주지 않음
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

    //카드를 크게 보여주려고
    //4가지 경우 
    public void ViewON(Card card)
    {
        if(active)
        {
            if(card.Volatility)
            {
                if (card.Friendly)
                {
                    //위치 옮겨주고
                    volatilityCard.transform.position = card.transform.position;
                    //big이 card보다 커서 위치를 같도록 하면 캔버스 밖으로 나가는 경우가 있어서 다시 안으로 되돌리기 위해
                    canvasRect.InSide(volatilityCard.GetComponent<RectTransform>());
                    //정보 복사해서 보여주려고
                    volatilityCard.Icon = card.Icon;
                    volatilityCard.Description = card.Description;
                    //활성화
                    volatilityCard.gameObject.SetActive(true);
                }
                else
                {
                    //위치 옮겨주고
                    evolatilityCard.transform.position = card.transform.position;
                    //big이 card보다 커서 위치를 같도록 하면 캔버스 밖으로 나가는 경우가 있어서 다시 안으로 되돌리기 위해
                    canvasRect.InSide(evolatilityCard.GetComponent<RectTransform>());
                    //정보 복사해서 보여주려고
                    evolatilityCard.Icon = card.Icon;
                    evolatilityCard.Description = card.Description;
                    //활성화
                    evolatilityCard.gameObject.SetActive(true);
                }
            }
            else 
            {
                if (card.Friendly)
                {
                    //위치 옮겨주고
                    big.transform.position = card.transform.position;
                    //big이 card보다 커서 위치를 같도록 하면 캔버스 밖으로 나가는 경우가 있어서 다시 안으로 되돌리기 위해
                    canvasRect.InSide(big.GetComponent<RectTransform>());
                    //정보 복사해서 보여주려고
                    big.Icon = card.Icon;
                    big.Description = card.Description;
                    //활성화
                    big.gameObject.SetActive(true);
                }
                else
                {
                    //위치 옮겨주고
                    ebig.transform.position = card.transform.position;
                    //big이 card보다 커서 위치를 같도록 하면 캔버스 밖으로 나가는 경우가 있어서 다시 안으로 되돌리기 위해
                    canvasRect.InSide(ebig.GetComponent<RectTransform>());
                    //정보 복사해서 보여주려고
                    ebig.Icon = card.Icon;
                    ebig.Description = card.Description;
                    //활성화
                    ebig.gameObject.SetActive(true);
                }
            }
        }
    }

    //비활성화
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