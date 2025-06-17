using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;//速度
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;//运行时更改动画控制器
    public Rigidbody2D target;//目标的刚体

    bool isLive;

    Rigidbody2D rigid;//怪物的刚体
    Collider2D coll;
    Animator anim;//怪物的动画
    SpriteRenderer spriter;//精灵渲染器
    WaitForFixedUpdate wait;//等待，直到下一个固定帧率更新函数
    void Awake()
    {
        //获取相对应的组件
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))  
            return;//如果怪物死亡 后续代码就不会执行

        Vector2 dirVec = target.position - rigid.position;//计算向量，确定玩家的方向
        Vector2 nextVec= dirVec.normalized * speed * Time.fixedDeltaTime;//标准化向量    让怪物知道下一步往哪走
        rigid.MovePosition(rigid.position+nextVec);//怪物移动到主角身边
        rigid.velocity=Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive) 
            return;//如果怪物死亡 后续代码就不会执行    

        spriter.flipX = target.position.x < rigid.position.x;//玩家的x小于怪物的x，那么怪物就需要翻转
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();//获取玩家的刚体
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        anim.SetBool("Dead", false);
        spriter.sortingOrder = 2;
        health = maxHealth;
    }
    public void Init(SpawnData data)//接收怪物的属性
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)//武器和怪物碰撞
    {
        if (!collision.CompareTag("Bullet") || !isLive)//如果标签不是bullet，就退出
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());//协程

        if(health > 0)
        {
            anim.SetTrigger("Hit");//播放被攻击的动画
        }
        else
        {
            isLive = false;
            coll.enabled = false;//碰撞器失效
            rigid.simulated = false;//刚体失效
            anim.SetBool("Dead",true);//播放死亡的动画
            spriter.sortingOrder = 1;//排序图层中的渲染器顺序
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()//敌人受到攻击的击退效果
    {
        yield return wait;//等待下一个物理框架
        Vector3 playerPos = GameManager.instance.player.transform.position;//获取玩家位置
        Vector3 dirVec = transform.position - playerPos;//方向
        rigid.AddForce(dirVec.normalized * 3,ForceMode2D.Impulse);//沿 force 矢量的方向连续施加力。
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
