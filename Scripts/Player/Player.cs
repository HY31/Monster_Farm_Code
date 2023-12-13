using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance = null;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<Player>();
            }
            return _instance;
        }
    }


}
