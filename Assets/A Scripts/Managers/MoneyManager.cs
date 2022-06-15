using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    private static int money;
    public static int Money { get => money; set => money = value; }

    private const string totalMoney = "totalMoney";
    public void ChangeTotalMoney()
    {
        int total = PlayerPrefs.GetInt(totalMoney);
        //UI_Manager.instance.ShowScoreAtWinWindow(score, totalScore01);

        PlayerPrefs.SetInt(totalMoney, total + money);
        print("total:" +  (money+total));
    }
    public static void ChangeMoney(int amount, Vector3 position)
    {
        money += amount;
        print("money:" + money);
        UIManager.instance.ShowMoneyChangeInGame(money,amount, position);

    }



}
