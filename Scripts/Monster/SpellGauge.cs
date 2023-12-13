using JetBrains.Annotations;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellGauge : MonoBehaviour
{
    public GameObject gaugePrefab;

    private int numGauge;
    public int NumGauge
    {
        get { return numGauge; }
        set { numGauge = value; }
    }
    GameObject[] gauges;


    public void CreateGauges(int maxSpell)
    {
        gauges = new GameObject[maxSpell];
        numGauge = maxSpell;

        if(gaugePrefab != null)
        {
            for(int i = 0; i< numGauge; i++) 
            {
                GameObject newGauge = Instantiate(gaugePrefab);
                newGauge.name = "Gauge_" + i;

                newGauge.transform.SetParent(gameObject.transform);
                gauges[i] = newGauge;
            }
        }
    }

    public void IncreaseGauage(int num)
    {
        gauges[num].gameObject.GetComponent<Image>().color = Color.green;
    }
    public void DecreaseGauge(int num)
    {
        gauges[num].gameObject.GetComponent<Image>().color = Color.red;
    }

    public void ClearGauge()
    {
        for(int i = 0;i< gauges.Length;i++)
        gauges[i].gameObject.GetComponent<Image>().color = Color.white;
    }

}
