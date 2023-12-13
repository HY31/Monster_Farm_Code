using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeSpellUI : MonoBehaviour
{
    RoomManager roomManager;

    [SerializeField]
    Image MonsterThumbnail;
    [SerializeField]
    TMP_Text MonsterName;
    [SerializeField]
    TMP_Text MonsterGrade;
    [SerializeField]
    TMP_Text MonsterMana;
    [SerializeField]
    TMP_Text MonsterHP;
    [SerializeField]
    TMP_Text MonsterDamage;

    [SerializeField]
    TMP_Text ManagementRatio;
    [SerializeField]
    GameObject MonsterStatHide;
    [SerializeField]
    GameObject m_ItemList;
    [SerializeField]
    Button StartBtn;
    [SerializeField]
    Button ExitBtn;
    [SerializeField]
    GameObject FireSelect;
    [SerializeField]
    GameObject WaterSelect;
    [SerializeField]
    GameObject WindSelect;
    [SerializeField]
    GameObject ThunderSelect;
    [SerializeField]
    GameObject LightSelect;
    [SerializeField]
    GameObject DarknessSelect;

    private List<item_data> ItemList = new List<item_data>();
    private List<UseItemIcon> m_ItemIcons = new List<UseItemIcon>();
    MonsterData CurMonsterData;
    RoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        StartBtn.interactable = false;
        roomManager = RoomManager.Instance;
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        roomManager.SetSelectItem(null);
        CurMonsterData = roomManager.GetCurSelectMonsterData();
        StartBtn.onClick.AddListener(StartTakeSpell);
        ExitBtn.onClick.AddListener(CloseUI);

        InitializeTakeSpellUI();
        InitializeItemList();
    }
    void InitializeTakeSpellUI()
    {
        MonsterThumbnail.sprite = Resources.Load(CurMonsterData.thumbnailPath, typeof(Sprite)) as Sprite;
        MonsterGrade.text = "위험 등급: "+CurMonsterData.grade.ToString() + " 급";
        MonsterMana.text = "생산 마나: "+CurMonsterData.maxSpell.ToString() + " 마나";
        MonsterDamage.text = "공격력: " + CurMonsterData.grade.ToString() + " DMG";
        MonsterHP.text = "HP: " + CurMonsterData.maxSpell.ToString() + " hp";
        MonsterName.text = CurMonsterData.name;
        MonsterStatHide.gameObject.SetActive(roomData.MonsterStatHide);

        ItemList = InvenManager.Instance.GetMyFoodList();
    }
    void InitializeItemList()
    {
        m_ItemIcons.Clear();
        foreach (Transform child in m_ItemList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (item_data itemData in ItemList)
        {
            GameObject itemIcon = Instantiate(Resources.Load("UI/UseItemIcon") as GameObject);
            itemIcon.GetComponent<UseItemIcon>().InitializeUseItemIcon(itemData,DeSelectItems);
            itemIcon.transform.SetParent(m_ItemList.transform);
            m_ItemIcons.Add(itemIcon.GetComponent<UseItemIcon>());
        }
    }
    string SetRatioToString(float ratio)
    {
        if (ratio >= 0 && ratio < 15)
        {
            ManagementRatio.color = Color.red;
            return "매우낮음";
        }
        else if (ratio >= 15 && ratio < 40)
        {
            ManagementRatio.color = Color.red;
            return "낮음";
        }
        else if (ratio >= 40 && ratio < 60)
        {
            ManagementRatio.color = Color.black;
            return "보통";
        }
        else if (ratio >= 60 && ratio < 85)
        {
            ManagementRatio.color = Color.green;
            return "높음";
        }
        else if (ratio >= 85 && ratio <= 100)
        {
            ManagementRatio.color = Color.green;
            return "매우높음";
        }
        else
        {
            return "오류";
        }

    }

    public void SetFire()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(true);
        WaterSelect.SetActive(false);
        WindSelect.SetActive(false);
        ThunderSelect.SetActive(false);
        LightSelect.SetActive(false);
        DarknessSelect.SetActive(false);
        roomManager.SetFire();
        if(roomData.FireHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.fire);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    public void SetWater()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(false);
        WaterSelect.SetActive(true);
        WindSelect.SetActive(false);
        ThunderSelect.SetActive(false);
        LightSelect.SetActive(false);
        DarknessSelect.SetActive(false);
        roomManager.SetWater();
        if (roomData.WaterHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.water);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    public void SetWind()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(false);
        WaterSelect.SetActive(false);
        WindSelect.SetActive(true);
        ThunderSelect.SetActive(false);
        LightSelect.SetActive(false);
        DarknessSelect.SetActive(false);
        roomManager.SetWind();
        if (roomData.WindHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.wind);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    public void SetThunder()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(false);
        WaterSelect.SetActive(false);
        WindSelect.SetActive(false);
        ThunderSelect.SetActive(true);
        LightSelect.SetActive(false);
        DarknessSelect.SetActive(false);
        roomManager.SetThunder();
        if (roomData.ThunderHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.thunder);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    public void SetLight()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(false);
        WaterSelect.SetActive(false);
        WindSelect.SetActive(false);
        ThunderSelect.SetActive(false);
        LightSelect.SetActive(true);
        DarknessSelect.SetActive(false);
        roomManager.SetLight();
        if (roomData.LightHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.light);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    public void SetDarkness()
    {
        StartBtn.interactable = true;
        FireSelect.SetActive(false);
        WaterSelect.SetActive(false);
        WindSelect.SetActive(false);
        ThunderSelect.SetActive(false);
        LightSelect.SetActive(false);
        DarknessSelect.SetActive(true);
        roomManager.SetDarkness();
        if (roomData.DarknessHide == false)
        {
            ManagementRatio.text = SetRatioToString(CurMonsterData.darkness);
        }
        else
        {
            ManagementRatio.color = Color.black;
            ManagementRatio.text = "???";
        }
    }
    void DeSelectItems()
    {
        for (int i = 0; i < m_ItemIcons.Count; i++ )
        {
            m_ItemIcons[i].DeSelect();
        }
    }

    void StartTakeSpell()
    {
        roomManager.TakeSpell();
        UIManagerTest.Instance.RemoveOneUI();
    }
    void CloseUI()
    {
        UIManagerTest.Instance.RemoveOneUI();
    }
}
