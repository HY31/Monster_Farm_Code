using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextDay : MonoBehaviour
{
    [SerializeField] private GameObject nextDay;
    [SerializeField] private TMP_Text nextDayText;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void NextDayStart(int days)
    {
        nextDay.SetActive(true);
        animator.SetTrigger("NextDayStart");
        nextDayText.text = $"Days {days} 마력 추출 완료";
    }

    public void SetNextDayOff()
    {
        nextDay.SetActive(false);
    }
}
