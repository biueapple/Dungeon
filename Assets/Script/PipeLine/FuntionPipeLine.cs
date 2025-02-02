using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ɴܰ踦 ���
public class FuntionPipeLine : IPipeLine
{
    //�����ܰ�
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //�� �ܰ踦 ������ ��� ĳ���͵�
    private readonly List<Character> characters;
    public List<Character> Characters { get { return characters; } }

    public FuntionPipeLine(IPipeLine next)
    {
        this.next = next;
        characters = new();
    }

    public void Start()
    {
        //����� ������ �ֵ��� ����Ʈ�� �߰�
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Plate != null)
                list[i].Plate.Function();
        }
        GameManager.Instance.StartCoroutine(Wait());
    }

    public void Update()
    {

    }

    public void End()
    {
        //�ݹ� ȣ��
        for (int i = 0; i < characters.Count; i++)
        {
            for (int p = characters[i].PCB.Count - 1; p >= 0; p--)
            {
                characters[i].PCB[p].Function();
            }
        }
        characters.Clear();
    }

    private IEnumerator Wait()
    {
        //1���� �����ΰ� ���� �ܰ��
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.PipeLine = next;
    }
}
