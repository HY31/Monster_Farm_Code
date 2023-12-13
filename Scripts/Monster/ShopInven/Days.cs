using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Days : MonoBehaviour
{
    public TMP_Text Day;
    void Start()
    {
        if(GameManager.instance == null)
        {
            Day.text = "0일차";
        }
        else
        {
            Day.text = GameManager.instance.GetDays().ToString() + "일차";
        }
    }

}
