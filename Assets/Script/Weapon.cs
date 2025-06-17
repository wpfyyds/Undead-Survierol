using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    void Update()
    {
        WeaponMove();

        textLevelUp();
    }

    private void WeaponMove()
    {

        if (!GameManager.instance.isLive)
        {
            return;
        }

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);//旋转
                break;
            default:
                timer += Time.deltaTime;
                //和子弹有关
                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;

        }
    }

    private void Fire()//负责子弹发射功能
    {
        if(!player.scanner.nearstTarget)
            return;

        Vector3 targetPos = player.scanner.nearstTarget.position;//获取距离角色最近的目标的位置
        Vector3 dir = targetPos - transform.position;//得到子弹发射的方向
        dir=dir.normalized; //标准化

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;//获取当前武器的坐标
        bullet.position = transform.position;//在玩家的位置生成
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//朝向目标的方向发射
        bullet.GetComponent<Bullet>().Init(damage, count, dir);//发射子弹

    }

    private void textLevelUp()
    {
        if (Input.GetButtonDown("Jump"))
        {
            levelUp(10, 1);
        }
    }

    public void levelUp(float damage,int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) 
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

    }

    public void Init(ItemData data)
    {
        //Basic  Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        switch(id)
        {
            case 0:
                speed = 150;//控制旋转速度
                Batch();
                break;
            default:
                speed = 0.4f;//控制生成时间
                break;

        }

        //Hand Set
       // Hand hand = player.hands[(int)data.itemType];
       // hand.spriter.sprite = data.hand;
       // hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int index = 0; index < count; index++) 
        {
            Transform bullet;

            //先把自己有的武器用了，不够再去获取
            if (index < transform.childCount)//如果数量还有剩下的
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;//获取当前武器的坐标
                bullet.parent = transform;
            }

            //如果不加下面两段代码，武器旋转会出问题
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //旋转武器
            Vector3 rotVec = Vector3.forward * 360 * index / count;//确定方向
            bullet.Rotate(rotVec);//按方向旋转
            bullet.Translate(bullet.up * 2f, Space.World);//移动

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);
        }
    }
}
