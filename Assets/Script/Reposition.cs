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
    //无限地图功能实现
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))//如果当前标签不是Area则结束
            return;
        
        Vector3 playerPos=GameManager.instance.player.transform.position;//获取玩家的位置坐标
        Vector3 myPos=transform.position;//获取地图位置
        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;//获取玩家移动的方向
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if (diffx > diffy)//如果玩家位置在地图左边，则地图执行下面的语句
                {
                    transform.Translate(Vector3.right * dirX * 60);//向右移动40个单位长度
                }else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
            case "Enemy":
                if(coll.enabled)//如果敌人的碰撞器存在且玩家视野里没有怪物，那么怪物就会随机转移到距离玩家20个单位的位置重新出现
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
