using UnityEngine;
using System.Collections;

//スコア構造体
public struct SCORE
{
    public float fDistance;     //そのステージで進んだ距離
    public float fMaxSpeed;     //そのステージでの最高速度
    public int nUpItemNum;      //そのステージ取得したプラス効果のアイテム数
    public int nDownItemNum;    //そのステージ取得したマイナス効果のアイテム数
}

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject cPlayer;
    private float fStartTime,fNowTime;
    private string strNowStatus,strPreStatus;
    private SCORE sScore;

    void Start()
    {
        cPlayer = GameObject.Find("Player");          //プレイヤーのオブジェクトを取得
        strNowStatus = "LAND_START";                  //ステージのステータスを"開始前"に設定
        strPreStatus = "LAND_START";                  //ステージの前のステータスを"開始前"に設定
        fStartTime = Time.time;                       //アクションシーン開始時の時間
    }

    void Update()
    {
        //変更前のステータスを保持
        strPreStatus = strNowStatus;

        if (fNowTime < 20)
        {
            if (strPreStatus == "LAND_START")
            {
                strNowStatus = "LAND";
            }
        }
        else if(fNowTime < 40)
        {
            if(strPreStatus == "LAND")
            {
                strNowStatus = "LAND_END";
            }
            else if (strPreStatus == "LAND_END")
            {
                strNowStatus = "SEA_START";
            }
            else if (strPreStatus == "SEA_START")
            {
                strNowStatus = "SEA";
            }
        }
        else if(fNowTime < 60)
        {
            if (strPreStatus == "SEA")
            {
                strNowStatus = "SEA_END";
            }
            else if (strPreStatus == "SEA_END")
            {
                strNowStatus = "SKY_START";
            }
            else if (strPreStatus == "SKY_START")
            {
                strNowStatus = "SKY";
            }
        }
        else if (fNowTime >= 60)
        {
            strNowStatus = "RESULT";

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
        GUI.Box(new Rect(Screen.width - 80, 0, 60, 20), strNowStatus);

        //メニューボタンが押された時
        //if (GUI.Button(, ))
        //{
        //   //リトライなど
        //}
    }

    //時間を返す
    public float GetTime(){return fNowTime;}

    //ステージのステータスを返す
    public string GetStatus(){return strNowStatus;}
}