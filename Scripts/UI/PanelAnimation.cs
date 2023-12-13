using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        Panel = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
    }
}
