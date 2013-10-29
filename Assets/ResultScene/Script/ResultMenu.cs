using UnityEngine;
using System.Collections;

public class ResultMenu : MonoBehaviour
{
    void Start()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, Screen.height / 2, 300, 40), "リトライ"))
        {
            //アクションシーンを開始する
            Application.LoadLevel("ActionScene");
        }

        if (GUI.Button(new Rect(20, Screen.height / 2 + 60, 300, 40), "タイトルに戻る"))
        {
            //操作説明を表示する
            Application.LoadLevel("MainMenuScene"); 
        }
    }
}
