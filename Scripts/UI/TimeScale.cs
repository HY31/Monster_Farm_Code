using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeScale : MonoBehaviour
{
    public enum TimeScaleValue
    {
        PAUSE = 0,
        ONE = 1,
        TWO = 2,
        THREE = 3
    }

    [SerializeField] private GameObject timeScaleON0;
    [SerializeField] private GameObject timeScaleON1;
    [SerializeField] private GameObject timeScaleON2;
    [SerializeField] private GameObject timeScaleON3;


    public void ChangeTimeScale(TimeScaleValue timeScaleValue)
    {
        switch (timeScaleValue)
        {
            case TimeScaleValue.PAUSE:
                timeScaleON0.SetActive(true);
                timeScaleON1.SetActive(false);
                timeScaleON2.SetActive(false);
                timeScaleON3.SetActive(false);
                break;
            case TimeScaleValue.ONE:
                timeScaleON0.SetActive(false);
                timeScaleON1.SetActive(true);
                timeScaleON2.SetActive(false);
                timeScaleON3.SetActive(false);
                break;
            case TimeScaleValue.TWO:
                timeScaleON0.SetActive(false);
                timeScaleON1.SetActive(false);
                timeScaleON2.SetActive(true);
                timeScaleON3.SetActive(false);
                break;
            case TimeScaleValue.THREE:
                timeScaleON0.SetActive(false);
                timeScaleON1.SetActive(false);
                timeScaleON2.SetActive(false);
                timeScaleON3.SetActive(true);
                break;
            default:
                timeScaleON0.SetActive(false);
                timeScaleON1.SetActive(true);
                timeScaleON2.SetActive(false);
                timeScaleON3.SetActive(false);
                break;
        }
    }

}
