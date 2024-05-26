using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float mana;
    public float maxMana;
    public int manaInSecMultiply;

    public RectTransform manaBar;

    private float startBarSize;

    private void Start()
    {
        startBarSize = manaBar.sizeDelta.y;
        manaBar.sizeDelta = new Vector2(manaBar.sizeDelta.x, startBarSize * mana / maxMana);
    }
    private void Update()
    {
        if(mana < maxMana)
            mana += Time.deltaTime * manaInSecMultiply;

        manaBar.sizeDelta = new Vector2(manaBar.sizeDelta.x, startBarSize * mana / maxMana);
    }
    public void ManaMinus(float count)
    {
        mana -= count;
    }
}
