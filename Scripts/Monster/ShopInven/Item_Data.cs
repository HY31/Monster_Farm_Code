using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_Data", menuName = "Scriptable Object/item_Data", order = int.MaxValue)]
//생성메뉴바에 item_Data라는 scriptableobject를 생성하는 메뉴를 생성
public class item_data : ScriptableObject
{
    [SerializeField]
    private Sprite item_sprite;
    public Sprite Item_Sprite { get { return item_sprite; }}
    [SerializeField]
    private int idx;//아이템의 인덱스
    public int IDX { get { return idx; } }
    [SerializeField]
    private string item_name;//아이템이름
    public string Item_name { get { return item_name; } }
    [SerializeField]
    private string item_contents;//아이템설명
    public string Item_contents { get { return item_contents; } }
    [SerializeField]
    private int price;//가격
    public int Price { get { return price; } }
    [SerializeField]
    private float upRatio;//높여줄 확률
    public float UpRatio { get { return upRatio; } }
    [SerializeField]
    private float damage;//높여줄 확률
    public float Damage { get { return damage; } }
    [SerializeField]
    private float hp;//높여줄 확률
    public float HP { get { return hp; } }
    [SerializeField]
    private string type;//
    public string Type { get { return type; } }

}
