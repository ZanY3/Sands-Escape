using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    public List<GameObject> wearpons;
    public List<bool> canDrop;
    public List<float> chances;

    public GameObject GetRandomWearpon()
    {
        float totalChance = 0f;
        for (int i = 0; i < chances.Count; i++)
        {
            if (canDrop[i])
            {
                totalChance += chances[i];
            }
        }
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;
        for (int i = 0; i < chances.Count; i++)
        {
            if (canDrop[i])
            {
                cumulativeChance += chances[i];
                if (randomValue <= cumulativeChance)
                {
                    GameObject selectedWearpon = wearpons[i];
                    wearpons.RemoveAt(i);
                    canDrop.RemoveAt(i);
                    chances.RemoveAt(i);

                    return selectedWearpon;
                }
            }
        }
        return null;
    }
}
