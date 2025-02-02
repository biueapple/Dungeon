using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class TextLinkClickHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // �ؽ�Ʈ ������Ʈ

    public void Texting(string text)
    {
        textComponent.text = text;
    }

    //������ ���� �̺�Ʈ�� ȣ��Ǵ� �Լ� ������ ȣ�� ��� ���콺 ��ġ�� ���� link text�� ã�� out����
    public bool OnPointerClick(out string word, out Vector3 position)
    {
        //���콺 ��ġ�� ���� ��ũ�� �ε����� ����
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        //��ũ�� �־��ٸ�
        if (linkIndex != -1)
        {
            //��ũ�� �ؽ�Ʈ�� ������
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
            string linkText = linkInfo.GetLinkText();
            //word�� �Է��ؼ� out����
            word = linkText;
            //��ġ�� �����ͼ� out����
            Rect rect = GetLinkBounds(linkInfo);
            position = rect.center;
            //������
            return true;
        }
        //���콺 ��ġ�� � ��ũ�� �����ٸ�
        else
        {
            word = null;
            position = Vector3.zero;
            //����
            return false;
        }
    }

    //������ ���� �̺�Ʈ�� ȣ��Ǵ� �Լ� ������ ȣ�� ��� ���콺 ��ġ�� ���� link index ã�� out����
    public bool OnPointerClick(out int index, out Vector3 position)
    {
        //���콺 ��ġ�� ���� ��ũ�� �ε����� ����
        index = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        //��ũ�� �־��ٸ�
        if (index != -1)
        { 
            //��ġ�� �����ͼ� out����
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[index];
            Rect rect = GetLinkBounds(linkInfo);
            position = rect.center;
            //������
            return true;
        }
        else
        {
            position = Vector3.zero;
            //����
            return false;
        }
    }

    //link�� ��ġ�� �������ִ� �Լ�
    private Rect GetLinkBounds(TMP_LinkInfo linkInfo)
    {
        TMP_CharacterInfo firstChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex];
        TMP_CharacterInfo lastChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength - 1];

        Vector3 bottomLeft = textComponent.transform.TransformPoint(firstChar.bottomLeft);
        Vector3 topRight = textComponent.transform.TransformPoint(lastChar.topRight);

        Vector2 size = topRight - bottomLeft;
        return new Rect(bottomLeft, size);
    }
}