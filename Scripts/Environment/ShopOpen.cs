using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    public void OpenShop()
    {
        UIManagerTest.Instance.AddUI(UIPrefab.ShopUI);
    }
}
