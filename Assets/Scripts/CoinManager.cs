using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int NumberCoin = 0;
    [SerializeField] TextMeshProUGUI _textMP;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Coins"))
            LoadProgress();
        _textMP.text = NumberCoin.ToString();
    }
    public void AddCoinToCollect()
    {
        NumberCoin++;
        _textMP.text = NumberCoin.ToString();
        SaveProgress();
    }

    //Потом заменить на JSON
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("Coins", NumberCoin);
    }
    public void LoadProgress()
    {
        NumberCoin = PlayerPrefs.GetInt("Coins");
    }
    /*public void SpendMoney(int value)
    {
        NumberCoin -= value;
        _textMP.text = NumberCoin.ToString();
    }
    */
}
