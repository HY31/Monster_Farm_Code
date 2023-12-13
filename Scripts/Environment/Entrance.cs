using UnityEngine;

public class Entrance : MonoBehaviour
{
    public void EnterTheManaRoom()
    {
        if (GameManager.instance != null)
        {
            GameManager gameManager = GameManager.instance;
            if (gameManager.GetGameOver() == true)
            {
                SceneLoad.LoadScene("MainScene");
                gameManager.SetGameOver(false);
            }
            else
                SceneLoad.LoadScene("MonsterSelectScene");
        }
        else
        {
            SceneLoad.LoadScene("MonsterSelectScene");

        }

    }
}
