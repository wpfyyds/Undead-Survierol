using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour//生成物体
{
    public GameObject[] prefabs;//存放预制体

    List<GameObject>[] pools;//对象池


    void Awake()
    {

        pools = new List<GameObject>[prefabs.Length];

        for(int index=0; index<pools.Length; index++)//设置对象池的长度
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])//遍历对象池里的对象
        {
            if (!item.activeSelf)//如果物体自身不存在,把当前对象池里的物体给select
            {
                select = item;
                select.SetActive(true);//状态设置为活动
                break;
            }
        }

        if (!select)//如果select不为空，那么就复制他自己
        {
            select=Instantiate(prefabs[index],transform);
            pools[index].Add(select);//把select加入到pool里
        }

        return select;
    }
}
