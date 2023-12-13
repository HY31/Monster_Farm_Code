using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseItemIcon : MonoBehaviour
{
    [SerializeField]
    Image ItemImage;
    [SerializeField]
    TMP_Text ItemAmount;
    [SerializeField]
    Button Btn;
    [SerializeField]
    GameObject Select;
    Action Deselect;

    item_data ItemData;
    bool isSelect;
    void Start()
    {
        Btn.onClick.AddListener(UseItem);
    }
    public void InitializeUseItemIcon(item_data useitemData, Action deselect)
    {
        ItemImage.sprite = useitemData.Item_Sprite;
        ItemAmount.text = InvenManager.Instance.GetFoodAmount(useitemData).ToString();
        ItemData = useitemData;
        Deselect = deselect;
    }

    void UseItem()
    {
        if (isSelect == true)
        {
            DeSelect();
            RoomManager.Instance.SetSelectItem(null);
            isSelect = false;
        }
        else
        {
            Deselect();//µ®∏Æ∞‘¿Ã∆Æ
            Select.gameObject.SetActive(true);
            RoomManager.Instance.SetSelectItem(ItemData);
            isSelect = true;
        }
    }
    public void DeSelect()
    {
        Select.gameObject.SetActive(false);
    }


}
