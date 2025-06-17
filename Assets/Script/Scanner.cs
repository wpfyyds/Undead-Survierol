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
        //在角色周围画一个圆，为了找到要攻击的目标
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, tagetlayer);
        nearstTarget = GetNearest();
    }

    Transform GetNearest()//找到最近的目标
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)//遍历范围内的目标
        {
            Vector3 myPos = transform.position;//角色的位置
            Vector3 targetPos=target.transform.position;//目标的位置
            float curDiff=Vector3.Distance(myPos, targetPos);//当前目标和角色的距离
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
