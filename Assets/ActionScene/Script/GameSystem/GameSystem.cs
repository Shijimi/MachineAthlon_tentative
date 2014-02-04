using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private float fStartTime,fNowTime;
    private string strNowStatus,strPreStatus;
    private const int STAGE_NUM = 3;
    private const int STAGE_TIME = 20;
    private int m_nNowStageNum = 0;

    void Start()
    {
        strNowStatus = "LAND_START";                  //ステージのステータスを"開始前"に設定.
        strPreStatus = "LAND_START";                  //ステージの前のステータスを"開始前"に設定.
        fStartTime = Time.time;                       //アクションシーン開始時の時間.
    }

    void Update()
    {
        //変更前のステータスを保持.
        strPreStatus = strNowStatus;

        if (fNowTime < STAGE_TIME)
        {
			//	フェードアウト.
			Fade.FadeOut();
			
            if (strPreStatus == "LAND_START")
            {
                strNowStatus = "LAND";
            }
			
			if( fNowTime > STAGE_TIME - 1)
				//	フェードイン.
				Fade.FadeIn();
        }
        else if( fNowTime < STAGE_TIME * 2 && Fade.FadeFlg() == false )
        {
			//	フェードアウト.
			Fade.FadeOut();
			
            if(strPreStatus == "LAND")
            {
                strNowStatus = "LAND_END";
            }
            else if (strPreStatus == "LAND_END")
            {
                strNowStatus = "SEA_START";
                m_nNowStageNum++;
            }
            else if (strPreStatus == "SEA_START")
            {
                strNowStatus = "SEA";
            }
			
			if( fNowTime > STAGE_TIME * 2 -1){
				//	フェードイン.
				Fade.FadeIn();
			}
        }
        else if( fNowTime < STAGE_TIME * 3 && Fade.FadeFlg() == false )
        {
			//	フェードアウト.
			Fade.FadeOut();
			
            if (strPreStatus == "SEA")
            {
                strNowStatus = "SEA_END";
            }
            else if (strPreStatus == "SEA_END")
            {
                strNowStatus = "SKY_START";
                m_nNowStageNum++;
            }
            else if (strPreStatus == "SKY_START")
            {
                strNowStatus = "SKY";
            }
			
			if( fNowTime > STAGE_TIME * 3 - 1)
				//	フェードイン.
				Fade.FadeIn();
        }
        else if (fNowTime >= STAGE_TIME * 3)
        {
            if (strPreStatus == "SKY")
            {
                strNowStatus = "SKY_END";
            }
            else if (strPreStatus == "SKY_END")
            {
                strNowStatus = "RESULT";

                //リザルトシーンを開始する
                Application.LoadLevel("ResultScene");
            }
        }
    }

    void OnGUI()
    {
        //経過時間を算出.
        fNowTime = Time.time - fStartTime;
		
		//	残り時間を取得.
		int	time	=	60 - (int)fNowTime;
		
		Number.NumSet( (60 - (int)fNowTime) );

		TimeBar.TimeSet( fNowTime * 0.00833f );
		
		//	一桁目のタイムを反映.
		TimeNumber1.TimeSet1(time % 10 * 0.1f);

		//	二桁目のタイムを反映.
		TimeNumber2.TimeSet2(time / 10 * 0.1f);
    }

    //時間を返す.
    public float GetTime(){return fNowTime;}

    //ステージのステータスを返す.
    public string GetStatus(){return strNowStatus;}

    //現在のステージ番号を返す
    public int GetNowStageNum() { return m_nNowStageNum; }

    //ステージ数を返す
    public int GetStageNum() { return STAGE_NUM; }

    //1ステージの時間を返す
    public int GetStageTime() { return STAGE_TIME; }
}