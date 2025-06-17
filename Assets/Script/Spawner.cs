using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour//随机生成怪物
{
    public Transform[] spawnerPoint;
    public SpawnData[] spawnData;

    int level;//等级
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
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length-1);//舍去小数

        if (timer > spawnData[level].spawnTime) 
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);//根据等级，获取怪物的类型
        //怪物的位置随机生成在玩家的旁边的位置
        enemy.transform.position = spawnerPoint[Random.Range(1, spawnerPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);//获取怪物的属性
    }
}

[System.Serializable]
public class SpawnData//用于存放怪物的属性
{
    public float spawnTime;//怪物生成的时间
    public float speed;//怪物的速度
    public int health;//怪物的生命值
    public int spriteType;//怪物的类型
}
