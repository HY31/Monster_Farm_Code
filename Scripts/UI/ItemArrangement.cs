using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArrangement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Items items;

    [SerializeField] private Vector2 mousePos;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject TowerImage;

    [SerializeField] private GameObject arrangementPoint = null;

    [SerializeField] private bool isArrangement = false;

    [SerializeField] private GameObject Tower;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.instance;
        //items = GameObject.FindGameObjectWithTag("Item").GetComponent<Items>();
        TowerImage = gameObject.transform.parent.transform.GetChild(1).gameObject;
        spriteRenderer = TowerImage.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        gameObject.transform.position = mousePos;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            soundManager.PlayCancelEffect();
            gameManager.SetIsSystemEvent_false();
            Destroy(gameObject.transform.parent.gameObject);
            items.DeSelected();
        }

        if (isArrangement)
        {
            TowerImage.transform.position = arrangementPoint.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                soundManager.PlayClickEffect();
                gameManager.SetIsSystemEvent_false();
                items.IsItemEvent_False();
                items.UseItem();
                GameObject myInstance = Instantiate(Tower, arrangementPoint.transform);
                myInstance.transform.position = arrangementPoint.transform.position;
                arrangementPoint.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject.transform.parent.gameObject);
                items.DeSelected();
            }
        }
        else
        {
            TowerImage.transform.position = gameObject.transform.position;
        }
    }
    public void SetItems(Items m_items,item_data itemdata)
    {
        items = m_items;
        Tower.GetComponent<Tower>().SetTowerInfo(itemdata);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrangement"))
        {
            arrangementPoint = collision.gameObject;
            isArrangement = true;
            spriteRenderer.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrangement"))
        {
            arrangementPoint = null;
            isArrangement = false;
            spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 0.5f);
        }
    }
}
