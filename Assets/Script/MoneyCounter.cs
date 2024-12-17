using Assets.Build.Castle;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter
{
    private static int _moneyAmount = 10000;
    private static List<int> _decreaseHistory = new();
    public static int Money()
    {
        return _moneyAmount;
    }
    public static int Money(int newMoney)
    {
        if (newMoney < _moneyAmount) _moneyAmount = newMoney;
        _decreaseHistory.Clear();
        return _moneyAmount;
    }
    public static int Money(BarStorage bar)
    {
        int decrease = DecreaseAmount(bar);
        _moneyAmount -= decrease;
        if (decrease > 0) _decreaseHistory.Add(decrease);

        return _moneyAmount;
    }

    public static void ResetMoneyAmount() {
        _decreaseHistory.Clear();
        _moneyAmount = 10000;
    }
    public static int DecreaseAmount(BarStorage barInfo)
    {
        if (barInfo == null) return 0;
        int decrease = 500;

        switch(barInfo._barType)
        {
            case "Wood":
                decrease = 250;
                break;
            case "Glass":
                decrease = 100;
                break;
            case "Iron":
                decrease = 500;
                break;
        }
        decrease  = Mathf.FloorToInt(decrease * barInfo._barScale.x);
        if ((_moneyAmount - decrease) < 0)
        {
            throw new Exception("Not Enough Money");
        }
        return decrease;
    }
    public static void ReverseMoney()
    {
        if (_decreaseHistory.Count < 1) return;
        int lastDecrease = _decreaseHistory[^1];
        Debug.Log(lastDecrease);
        _moneyAmount += lastDecrease;
        _decreaseHistory.Remove(_decreaseHistory[^1]);
    }
}
