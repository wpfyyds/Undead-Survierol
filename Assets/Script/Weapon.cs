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
                transform.Rotate(Vector3.back * speed * Time.deltaTime);//��ת
                break;
            default:
                timer += Time.deltaTime;
                //���ӵ��й�
                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;

        }
    }

    private void Fire()//�����ӵ����书��
    {
        if(!player.scanner.nearstTarget)
            return;

        Vector3 targetPos = player.scanner.nearstTarget.position;//��ȡ�����ɫ�����Ŀ���λ��
        Vector3 dir = targetPos - transform.position;//�õ��ӵ�����ķ���
        dir=dir.normalized; //��׼��

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;//��ȡ��ǰ����������
        bullet.position = transform.position;//����ҵ�λ������
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//����Ŀ��ķ�����
        bullet.GetComponent<Bullet>().Init(damage, count, dir);//�����ӵ�

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
                speed = 150;//������ת�ٶ�
                Batch();
                break;
            default:
                speed = 0.4f;//��������ʱ��
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

            //�Ȱ��Լ��е��������ˣ�������ȥ��ȡ
            if (index < transform.childCount)//�����������ʣ�µ�
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;//��ȡ��ǰ����������
                bullet.parent = transform;
            }

            //��������������δ��룬������ת�������
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //��ת����
            Vector3 rotVec = Vector3.forward * 360 * index / count;//ȷ������
            bullet.Rotate(rotVec);//��������ת
            bullet.Translate(bullet.up * 2f, Space.World);//�ƶ�

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);
        }
    }
}
