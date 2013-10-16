using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject cPlayer;
    private float fStartTime,fNowTime;
    private string sNowStatus,sPreStatus;

    void Start()
    {
        cPlayer = GameObject.Find("Player");          //プレイヤーのオブジェクトを取得
        sNowStatus = "LAND_START";                    //ステージのステータスを"開始前"に設定
        sPreStatus = "LAND_START";                    //ステージの前のステータスを"開始前"に設定
        fStartTime = Time.time;                       //アクションシーン開始時の時間
    }

    void Update()
    {
        //変更前のステータスを保持
        sPreStatus = sNowStatus;

        if (fNowTime < 20)
        {
            if (sPreStatus == "LAND_START")
            {
                sNowStatus = "LAND";
            }
        }
        else if(fNowTime < 40)
        {
            if(sPreStatus == "LAND")
            {
                sNowStatus = "LAND_END";
            }
            else if (sPreStatus == "LAND_END")
            {
                sNowStatus = "SEA_START";
            }
            else if (sPreStatus == "SEA_START")
            {
                sNowStatus = "SEA";
            }
        }
        else if(fNowTime < 60)
        {
            if (sPreStatus == "SEA")
            {
                sNowStatus = "SEA_END";
            }
            else if (sPreStatus == "SEA_END")
            {
                sNowStatus = "SKY_START";
            }
            else if (sPreStatus == "SKY_START")
            {
                sNowStatus = "SKY";
            }
        }
        else if (fNowTime >= 60)
        {
            sNowStatus = "RESULT";

            //リザルトシーンを開始する
            Application.LoadLevel("ResultScene");
        }
    }

    void OnGUI()
    {

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //GUIのサイズ関連はとりあえずで設定しているので
        //画面サイズは考慮していません
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //経過時間を算出
        fNowTime = Time.time - fStartTime;

        //時間を表示
        GUI.Box(new Rect(0,0,200,20),"Time:" + fNowTime.ToString("00.00"));

        //距離を表示
        GUI.Box(new Rect(0, 30, 200, 20), "Distance:" + cPlayer.GetComponent<Player>().GetDistance().ToString("000000"));

        //ステージの進行状況(陸海空など)を表示
        GUI.Box(new Rect(Screen.width - 80, 0, 60, 20), sNowStatus);

        //メニューボタンが押された時
        //if (GUI.Button(, ))
        //{
        //   //リトライなど
        //}
    }

    //時間を返す
    public float GetTime(){return fNowTime;}

    //ステージのステータスを返す
    public string GetStatus(){return sNowStatus;}
}
