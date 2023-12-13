using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField]
    Image ShopItemIcon;
    [SerializeField]
    TMP_Text ShopItemName;
    [SerializeField]
    TMP_Text ShopItemContents;
    [SerializeField]
    TMP_Text ShopItemPrice;
    [SerializeField]
    Button Btn;
    private item_data Cur_ItemData;

    private void Start()
    {
        Btn.onClick.AddListener(OpenBuyPopUp);
    }
    public void InitializeShopItemSlot(item_data shop_itemData)
    {
        ShopItemIcon.sprite = shop_itemData.Item_Sprite;
        ShopItemName.text = shop_itemData.Item_name;
        ShopItemContents.text = shop_itemData.Item_contents;
        ShopItemPrice.text = "가격: "+shop_itemData.Price.ToString() + "G";
        Cur_ItemData = shop_itemData; 
    }
    public void OpenBuyPopUp()
    {
        UIManagerTest.Instance.AddUI(UIPrefab.BuyPopUp);
        BuyPopUp buyPopUp = UIManagerTest.Instance.GetLastUI<BuyPopUp>();
        buyPopUp.SetPopUp(Cur_ItemData.Item_name, "구매 하시겠습니까?", BuyCurItem);
    }
    void BuyCurItem()
    {
        InvenManager invenManager = InvenManager.Instance;
        if (invenManager.GetGold() >= Cur_ItemData.Price)
        {
            if(Cur_ItemData.IDX == 0)
            invenManager.AddMyFood(Cur_ItemData, Cur_ItemData.Price);
            else
            invenManager.AddMyTower(Cur_ItemData, Cur_ItemData.Price);
        }
        invenManager.ShopUI.UpdateGold();
        invenManager.ShopUI.InitializeInvenFood();
    }
}
