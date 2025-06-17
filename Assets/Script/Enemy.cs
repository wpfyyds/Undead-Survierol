using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;//�ٶ�
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;//����ʱ���Ķ���������
    public Rigidbody2D target;//Ŀ��ĸ���

    bool isLive;

    Rigidbody2D rigid;//����ĸ���
    Collider2D coll;
    Animator anim;//����Ķ���
    SpriteRenderer spriter;//������Ⱦ��
    WaitForFixedUpdate wait;//�ȴ���ֱ����һ���̶�֡�ʸ��º���
    void Awake()
    {
        //��ȡ���Ӧ�����
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
            return;//����������� ��������Ͳ���ִ��

        Vector2 dirVec = target.position - rigid.position;//����������ȷ����ҵķ���
        Vector2 nextVec= dirVec.normalized * speed * Time.fixedDeltaTime;//��׼������    �ù���֪����һ��������
        rigid.MovePosition(rigid.position+nextVec);//�����ƶ����������
        rigid.velocity=Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive) 
            return;//����������� ��������Ͳ���ִ��    

        spriter.flipX = target.position.x < rigid.position.x;//��ҵ�xС�ڹ����x����ô�������Ҫ��ת
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();//��ȡ��ҵĸ���
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        anim.SetBool("Dead", false);
        spriter.sortingOrder = 2;
        health = maxHealth;
    }
    public void Init(SpawnData data)//���չ��������
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)//�����͹�����ײ
    {
        if (!collision.CompareTag("Bullet") || !isLive)//�����ǩ����bullet�����˳�
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());//Э��

        if(health > 0)
        {
            anim.SetTrigger("Hit");//���ű������Ķ���
        }
        else
        {
            isLive = false;
            coll.enabled = false;//��ײ��ʧЧ
            rigid.simulated = false;//����ʧЧ
            anim.SetBool("Dead",true);//���������Ķ���
            spriter.sortingOrder = 1;//����ͼ���е���Ⱦ��˳��
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()//�����ܵ������Ļ���Ч��
    {
        yield return wait;//�ȴ���һ��������
        Vector3 playerPos = GameManager.instance.player.transform.position;//��ȡ���λ��
        Vector3 dirVec = transform.position - playerPos;//����
        rigid.AddForce(dirVec.normalized * 3,ForceMode2D.Impulse);//�� force ʸ���ķ�������ʩ������
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
