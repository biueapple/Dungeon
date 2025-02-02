using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour
{
    //�ִϸ��̼�
    protected Animator animator;
    //SpriteRenderer�� �ִϸ��̼��� SpriteRenderer�� sprite���� ������ ������ ĳ���ʹ� image�⿡ ������ �ȵ� ������� �ֱ� ���� ����
    protected SpriteRenderer spriteRenderer;
    //�ڽ��� �̹��� ���� ������ ����
    protected Image image;
    //�ڽŸ��� �÷���Ʈ�� 3������ ������ ��

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        //�ִϸ��̼� ����� ����Ǵ� sprite�� image�� ����ǵ���
        image.sprite = spriteRenderer.sprite;
    }

    public void SetTrigger(string str)
    {
        animator.SetTrigger(str);
    }
}
