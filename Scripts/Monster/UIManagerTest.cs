using System.Collections.Generic;
using UnityEngine;

public enum UIPrefab
{
    TakeManaUI,
    MonsterInfoUI,
    ShopUI,
    BuyPopUp
}
public class UIManagerTest : MonoBehaviour
{
    private static UIManagerTest m_instance = null;

    List<GameObject> m_ui = new List<GameObject>();
    List<string> m_uiPrefabPath = new List<string>();

    public static UIManagerTest Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(UIManagerTest)) as UIManagerTest;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
            SetUIPrefabPath();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetUIPrefabPath()
    {
        m_uiPrefabPath.Add("UI/TakeManaUI");
        m_uiPrefabPath.Add("UI/MonsterInfoUI");
        m_uiPrefabPath.Add("UI/ShopUI");
        m_uiPrefabPath.Add("UI/BuyPopUp");
    }

    public void AddUI(UIPrefab uiPrefab)//제일위에 ui추가
    {
        m_ui.Add(Instantiate(Resources.Load(m_uiPrefabPath[(int)uiPrefab])) as GameObject);
        m_ui[m_ui.Count - 1].GetComponent<Canvas>().sortingOrder = m_ui.Count+10;
        m_ui[m_ui.Count - 1].transform.SetParent(this.gameObject.transform);
    }

    public void RemoveOneUI()//제일 위에 ui삭제
    {
        if (m_ui.Count > 0)
        {
            Destroy(m_ui[m_ui.Count - 1]);
            m_ui.RemoveAt(m_ui.Count - 1);
        }
    }
    public T GetLastUI<T>()
    {
        return m_ui[m_ui.Count-1].GetComponent<T>();
    }

}
