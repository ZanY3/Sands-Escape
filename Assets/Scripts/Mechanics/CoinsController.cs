using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public int coins;
    public TMP_Text coinsText;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void TakeCoins(int count)
    {
        coins += count;
        coinsText.text = coins.ToString();
    }
}
