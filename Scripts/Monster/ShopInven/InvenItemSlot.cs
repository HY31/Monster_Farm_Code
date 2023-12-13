using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenItemSlot : MonoBehaviour
{
    [SerializeField]
    Image InvenItemIcon;
    [SerializeField]
    TMP_Text InvenItemName;
    [SerializeField]
    TMP_Text InvenItemContents;
    [SerializeField]
    TMP_Text InvenItemAmount;

    private item_data Cur_ItemData;

    public void InitializeInvenItemSlot(item_data inven_itemData)
    {
        InvenItemIcon.sprite = inven_itemData.Item_Sprite;
        InvenItemName.text = inven_itemData.Item_name;
        InvenItemContents.text = inven_itemData.Item_contents;

        if(inven_itemData.IDX ==0)
        InvenItemAmount.text = "보유개수: " + InvenManager.Instance.GetFoodAmount(inven_itemData).ToString() + "개";
        else
        InvenItemAmount.text = "보유개수: " + InvenManager.Instance.GetTowerAmount(inven_itemData).ToString() + "개";

        Cur_ItemData = inven_itemData;
    }
}
