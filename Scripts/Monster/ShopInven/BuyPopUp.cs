using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopUp : MonoBehaviour
{
    [SerializeField]
    TMP_Text Title;
    [SerializeField]
    TMP_Text Contents;
    [SerializeField]
    Button BtnBack;
    [SerializeField]
    Button BtnConfirm;
    [SerializeField]
    Button BtnExit;

    private Action onConfirm;
    private void Start()
    {
        BtnBack.onClick.AddListener(Close);
        BtnConfirm.onClick.AddListener(Confirm);
        BtnExit.onClick.AddListener(Close);
    }
    public void SetPopUp(string title, string content, Action onconirm)
    {
        Title.text = title;
        Contents.text = content;
        onConfirm = onconirm;
    }
    void Confirm()
    {
        onConfirm();

        Close();
    }

    void Close()
    {
        UIManagerTest.Instance.RemoveOneUI();
    }
}
