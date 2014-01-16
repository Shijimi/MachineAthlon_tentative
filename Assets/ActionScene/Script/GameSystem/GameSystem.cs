using UnityEngine;
using System.Collections;

//スコア構造体.
public struct SCORE
{
    public float fDistance;     //そのステージで進んだ距離.
    public float fMaxSpeed;     //そのステージでの最高速度.
    public int nUpItemNum;      //そのステージ取得したプラス効果のアイテム数.
    public int nDownItemNum;    //そのステージ取得したマイナス効果のアイテム数.
}

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private float fStartTime,fNowTime;
    private string strNowStatus,strPreStatus;
    private SCORE sScore;

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

        if (fNowTime < 20)
        {
			//	フェードアウト.
			Fade.FadeOut();
			
            if (strPreStatus == "LAND_START")
            {
                strNowStatus = "LAND";
            }
			
			if( fNowTime > 19 )
				//	フェードイン.
				Fade.FadeIn();
        }
        else if( fNowTime < 40 && Fade.FadeFlg() == false )
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
            }
            else if (strPreStatus == "SEA_START")
            {
                strNowStatus = "SEA";
            }
			
			if( fNowTime > 39 ){
				//	フェードイン.
				Fade.FadeIn();
			}
        }
        else if( fNowTime < 60 && Fade.FadeFlg() == false )
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
            }
            else if (strPreStatus == "SKY_START")
            {
                strNowStatus = "SKY";
            }
			
			if( fNowTime > 59 )
				//	フェードイン.
				Fade.FadeIn();
        }
        else if (fNowTime >= 60)
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
}