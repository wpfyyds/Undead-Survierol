using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour//������ɹ���
{
    public Transform[] spawnerPoint;
    public SpawnData[] spawnData;

    int level;//�ȼ�
    float timer;

    private void Awake()
    {
        spawnerPoint=GetComponentsInChildren<Transform>();
    }
    void Update()
    {

        if (!GameManager.instance.isLive)
        {
            return;
        }

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length-1);//��ȥС��

        if (timer > spawnData[level].spawnTime) 
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);//���ݵȼ�����ȡ���������
        //�����λ�������������ҵ��Աߵ�λ��
        enemy.transform.position = spawnerPoint[Random.Range(1, spawnerPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);//��ȡ���������
    }
}

[System.Serializable]
public class SpawnData//���ڴ�Ź��������
{
    public float spawnTime;//�������ɵ�ʱ��
    public float speed;//������ٶ�
    public int health;//���������ֵ
    public int spriteType;//���������
}
