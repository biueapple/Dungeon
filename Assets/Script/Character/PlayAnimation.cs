using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour
{
    //애니메이션
    protected Animator animator;
    //SpriteRenderer는 애니메이션이 SpriteRenderer의 sprite만을 변경해 주지만 캐릭터는 image기에 적용이 안됨 적용시켜 주기 위해 참조
    protected SpriteRenderer spriteRenderer;
    //자신의 이미지 참조 이유는 위에
    protected Image image;
    //자신만의 플레이트를 3개정도 가져야 함

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        //애니메이션 재생중 변경되는 sprite가 image에 적용되도록
        image.sprite = spriteRenderer.sprite;
    }

    public void SetTrigger(string str)
    {
        animator.SetTrigger(str);
    }
}
