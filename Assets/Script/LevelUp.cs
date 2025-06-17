using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items=GetComponentsInChildren<Item>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()//升级时，显示升级UI
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide() //隐藏UI
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)//初始武器
    {
        items[index].OnClick();
    }

    void Next()
    {
        //禁用所有升级项目
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }
        //随机激活升级物品
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0,items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) 
                break;
        }

        for (int index = 0; index < ran.Length; index++) { 
            Item ranitem = items[ran[index]];
            //如果武器满级就不会再出现,用消耗品代替
            if (ranitem.level == ranitem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranitem.gameObject.SetActive(true);

            }
        }
      
    }
}
