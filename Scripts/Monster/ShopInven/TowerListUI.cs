using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerListUI : MonoBehaviour
{
    [SerializeField]
    GameObject towerListUI;

    private List<item_data> TowerList = new List<item_data>();
    private List<Items> TowerIcons = new List<Items>();
    void Start()
    {
        TowerList = InvenManager.Instance.GetMyTowerList();
        InitializeTowerListUI();
    }
    void InitializeTowerListUI()
    {
        TowerIcons.Clear();
        foreach (Transform child in towerListUI.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (item_data itemData in TowerList)
        {
            GameObject itemIcon = Instantiate(Resources.Load("UI/ItemSlot") as GameObject);
            itemIcon.GetComponent<Items>().InitializeItemIcon(itemData);
            itemIcon.transform.SetParent(towerListUI.transform);
            TowerIcons.Add(itemIcon.GetComponent<Items>());
        }
    }

}
