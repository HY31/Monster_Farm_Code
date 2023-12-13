using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject[] items;

    [SerializeField] private bool isItemEvent;

    [SerializeField] private GameObject item = null;

    [SerializeField] private TMP_Text ItemAmount;

    [SerializeField] private Image ItemImage;

    [SerializeField] private GameObject Select;

    [SerializeField] private item_data ItemData;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.instance;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (item != null && isItemEvent)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) isItemEvent = false;
        }
    }

    public void ClickItem(int a)
    {
        if (!isItemEvent)
        {
            Selected();
            soundManager.PlayClickEffect();
            gameManager.SetIsSystemEvent_true();
            item = items[a];
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            item.transform.position = mousePos;
            item.transform.GetChild(0).GetComponent<ItemArrangement>().SetItems(this,ItemData);
            Instantiate(item);
            isItemEvent = true;
        }
        else return;
    }
    public void Selected()
    {
        Select.SetActive(true);
    }
    public void DeSelected()
    {
        Select.SetActive(false);
    }

    public void IsItemEvent_False()
    {
        isItemEvent = false;
    }
    public void InitializeItemIcon(item_data towerItemData)
    {
        ItemData = towerItemData;
        ItemAmount.text = InvenManager.Instance.GetTowerAmount(towerItemData).ToString();
        ItemImage.sprite = ItemData.Item_Sprite;
    }

    public void UseItem()
    {
        int temp;
        InvenManager.Instance.UseMyTower(ItemData);
        temp = InvenManager.Instance.GetTowerAmount(ItemData);
        ItemAmount.text = temp.ToString();

        if (temp<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
