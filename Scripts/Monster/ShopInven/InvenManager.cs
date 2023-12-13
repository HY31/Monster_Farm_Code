using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InvenManager : MonoBehaviour
{
    private static InvenManager m_instance = null;
    public static InvenManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(InvenManager)) as InvenManager;
            }
            return m_instance;
        }
    }
    [SerializeField]
    private List<item_data> MyFoodList = new List<item_data>();
    [SerializeField]
    private List<int> MyFoodNums = new List<int>();
    [SerializeField]
    private List<item_data> MyTowerList = new List<item_data>();
    [SerializeField]
    private List<int> MyTowerNums = new List<int>();

    [SerializeField]
    private int Gold;
    public ShopUI ShopUI;

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int GetGold()
    {
        return Gold;
    }
    public List<item_data> GetMyFoodList()
    {
        return MyFoodList;
    }
    public List<item_data> GetMyTowerList()
    {
        return MyTowerList;
    }
    public void AddMyFood(item_data foodItem,int price)
    {
        for(int i = 0; i < MyFoodList.Count; i++)
        {
            if (MyFoodList[i] == foodItem)
            {
                MyFoodNums[i]++;
                Gold -= price;
                return;
            }
        }
        MyFoodNums.Add(1);
        MyFoodList.Add(foodItem);
        Gold -= price;
    }
    public void AddMyTower(item_data foodItem, int price)
    {
        for (int i = 0; i < MyTowerList.Count; i++)
        {
            if (MyTowerList[i] == foodItem)
            {
                MyTowerNums[i]++;
                Gold -= price;
                return;
            }
        }
        MyTowerNums.Add(1);
        MyTowerList.Add(foodItem);
        Gold -= price;
    }
    public void UseMyFood(item_data selectfoodItem)
    {
        if (selectfoodItem == null)
            return;

        for (int i = 0; i < MyFoodList.Count; i++)
        {
            if (MyFoodList[i] == selectfoodItem)
            {
                MyFoodNums[i]--;
                if (MyFoodNums[i] == 0)
                {
                    MyFoodList.RemoveAt(i);
                    MyFoodNums.RemoveAt(i);
                }
            }
        }
    }
    public void UseMyTower(item_data selectTowerItem)
    {
        if (selectTowerItem == null)
            return;

        for (int i = 0; i < MyTowerList.Count; i++)
        {
            if (MyTowerList[i] == selectTowerItem)
            {
                MyTowerNums[i]--;
                if (MyTowerNums[i] == 0)
                {
                    MyTowerList.RemoveAt(i);
                    MyTowerNums.RemoveAt(i);
                }
            }
        }
    }
    public int GetFoodAmount(item_data foodItem)
    {
        for (int i = 0; i < MyFoodList.Count; i++)
        {
            if (MyFoodList[i] == foodItem)
            {
                return MyFoodNums[i];
            }
        }
        return 0;
    }
    public int GetTowerAmount(item_data towerItem)
    {
        for (int i = 0; i < MyTowerList.Count; i++)
        {
            if (MyTowerList[i] == towerItem)
            {
                return MyTowerNums[i];
            }
        }
        return 0;
    }
    public void AddGold(int num) 
    {
        Gold += num;
    }
}
