using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;

    List<GameObject>[] pools;

    private static PoolManager m_Instance = null;
    public static PoolManager Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType(typeof(PoolManager)) as PoolManager;
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (null == m_Instance)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        pools = new List<GameObject>[Prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject> ();
        }
    }
   
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                select.GetComponent<Bullet>().DestroyBulletInvoke();
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(Prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }


        
}
