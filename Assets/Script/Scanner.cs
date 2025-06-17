using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask tagetlayer;
    public RaycastHit2D[] targets;
    public Transform nearstTarget;

    private void FixedUpdate()
    {
        //�ڽ�ɫ��Χ��һ��Բ��Ϊ���ҵ�Ҫ������Ŀ��
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, tagetlayer);
        nearstTarget = GetNearest();
    }

    Transform GetNearest()//�ҵ������Ŀ��
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)//������Χ�ڵ�Ŀ��
        {
            Vector3 myPos = transform.position;//��ɫ��λ��
            Vector3 targetPos=target.transform.position;//Ŀ���λ��
            float curDiff=Vector3.Distance(myPos, targetPos);//��ǰĿ��ͽ�ɫ�ľ���
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
