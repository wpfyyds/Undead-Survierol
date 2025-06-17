using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp,Level,Kill,Time,Health }
    public InfoType type;

    Text mytext;
    Slider myslider;

    void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type) {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;//获取当前经验值
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];//获取最大经验值
                myslider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                mytext.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.Kill:
                mytext.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                mytext.text = string.Format("{0:D2}:{1:D2}", min,sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                myslider.value = curHealth / maxHealth;
                break;
        }
    }
}
