using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    //���޵�ͼ����ʵ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))//�����ǰ��ǩ����Area�����
            return;
        
        Vector3 playerPos=GameManager.instance.player.transform.position;//��ȡ��ҵ�λ������
        Vector3 myPos=transform.position;//��ȡ��ͼλ��
        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;//��ȡ����ƶ��ķ���
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if (diffx > diffy)//������λ���ڵ�ͼ��ߣ����ͼִ����������
                {
                    transform.Translate(Vector3.right * dirX * 60);//�����ƶ�40����λ����
                }else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
            case "Enemy":
                if(coll.enabled)//������˵���ײ�������������Ұ��û�й����ô����ͻ����ת�Ƶ��������20����λ��λ�����³���
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
