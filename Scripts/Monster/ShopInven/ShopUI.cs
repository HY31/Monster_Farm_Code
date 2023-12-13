using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;
//using UnityEditor.U2D.Aseprite;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private List<item_data> FoodList = new List<item_data>();
    [SerializeField]
    private List<item_data> TowerList = new List<item_data>();

    [SerializeField]
    private GameObject m_ShopList;
    [SerializeField]
    private GameObject m_InvenList;

    [SerializeField]
    private TMP_Text GoldTxt;
    [SerializeField]
    private Button ExitBtn;

    [SerializeField]
    GameObject ShopFoodSelect;
    [SerializeField]
    GameObject ShopTowerSelect;
    [SerializeField]
    GameObject InvenFoodSelect;
    [SerializeField]
    GameObject InvenTowerSelect;


    private List<ShopItemSlot> m_ShopSlots = new List<ShopItemSlot>();
    private List<InvenItemSlot> m_InvenSlots = new List<InvenItemSlot>();

    void Start()
    {
        if(InvenManager.Instance.ShopUI == null)
        InvenManager.Instance.ShopUI = this;
        UpdateGold();
        ExitBtn.onClick.AddListener(Exit);
        InitializeFoodList();
        InitializeInvenFood();
    }

    public void InitializeFoodList()
    {
        m_ShopSlots.Clear();
        foreach (Transform child in m_ShopList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (item_data foodData in FoodList)
        {
            GameObject shopSlot = Instantiate(Resources.Load("UI/ShopItemSlot") as GameObject);
            shopSlot.GetComponent<ShopItemSlot>().InitializeShopItemSlot(foodData);
            shopSlot.transform.SetParent(m_ShopList.transform);
            m_ShopSlots.Add(shopSlot.GetComponent<ShopItemSlot>());
        }
        ShopFoodSelect.SetActive(true);
        ShopTowerSelect.SetActive(false);
    }

    public void InitializeTowerList()
    {
        m_ShopSlots.Clear();
        foreach (Transform child in m_ShopList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (item_data towerData in TowerList)
        {
            GameObject shopSlot = Instantiate(Resources.Load("UI/ShopItemSlot") as GameObject);
            shopSlot.GetComponent<ShopItemSlot>().InitializeShopItemSlot(towerData);
            shopSlot.transform.SetParent(m_ShopList.transform);
            m_ShopSlots.Add(shopSlot.GetComponent<ShopItemSlot>());
        }
        ShopFoodSelect.SetActive(false);
        ShopTowerSelect.SetActive(true);
    }
    public void InitializeInvenFood()
    {
        m_InvenSlots.Clear();
        foreach(Transform child in m_InvenList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(item_data invenFoodData in InvenManager.Instance.GetMyFoodList())
        {
            GameObject shopSlot = Instantiate(Resources.Load("UI/InvenItemSlot") as GameObject);
            shopSlot.GetComponent<InvenItemSlot>().InitializeInvenItemSlot(invenFoodData);
            shopSlot.transform.SetParent(m_InvenList.transform);
            m_InvenSlots.Add(shopSlot.GetComponent<InvenItemSlot>());
        }
        InvenFoodSelect.SetActive(true);
        InvenTowerSelect.SetActive(false);
    }
    public void InitializeInvenTower()
    {
        m_InvenSlots.Clear();
        foreach (Transform child in m_InvenList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (item_data invenTowerData in InvenManager.Instance.GetMyTowerList())
        {
            GameObject shopSlot = Instantiate(Resources.Load("UI/InvenItemSlot") as GameObject);
            shopSlot.GetComponent<InvenItemSlot>().InitializeInvenItemSlot(invenTowerData);
            shopSlot.transform.SetParent(m_InvenList.transform);
            m_InvenSlots.Add(shopSlot.GetComponent<InvenItemSlot>());
        }
        InvenFoodSelect.SetActive(false);
        InvenTowerSelect.SetActive(true);
    }
    public void UpdateGold()
    {
        GoldTxt.text = InvenManager.Instance.GetGold().ToString()+"G";
    }

    public void Exit()
    {
        UIManagerTest.Instance.RemoveOneUI();
    }


}
