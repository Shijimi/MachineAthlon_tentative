using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(20,Screen.height/2, 300, 40), "ゲームスタート"))
        {
           //アクションシーンを開始する
            Application.LoadLevel("ActionScene");
        }

        if (GUI.Button(new Rect(20, Screen.height / 2 + 60,300, 40), "操作説明"))
        {
           //操作説明を表示する
        }
    }
}
