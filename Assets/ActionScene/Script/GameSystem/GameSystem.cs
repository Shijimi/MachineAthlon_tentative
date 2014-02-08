using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private float fStartTime,fNowTime;
    private string strNowStatus,strPreStatus;
    private const int STAGE_NUM = 3;
    private const float STAGE_TIME = 20;
    private int m_nNowStageNum = 0;
    private int m_iWaitTime = 0;

    void Start()
    {
		//	準備エフェクトを生成.
		GetComponent<EffectCreator>().Create("READY");
		
        strNowStatus = "LAND_START";                  //ステージのステータスを"開始前"に設定.
        strPreStatus = "LAND_START";                  //ステージの前のステータスを"開始前"に設定.
        fStartTime = Time.time;                       //アクションシーン開始時の時間.
    }

    void Update()
    {
        //変更前のステータスを保持.
        strPreStatus = strNowStatus;
		
		if( m_iWaitTime <= 140 )
		{
			//	フェードアウト.
			Fade.FadeOut();
			
			m_iWaitTime++;
			
			fStartTime = Time.time;
			
			if( m_iWaitTime == 100 )
				//	準備エフェクトを生成.
				GetComponent<EffectCreator>().Create("START");
			
			return;
		}

        fNowTime = Time.time - fStartTime;
        TimeBar.TimeSet(fNowTime * 0.00833f);

        if (fNowTime < STAGE_TIME)
        {			
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

                //フェードSEを再生
                audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
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

                //フェードSEを再生
                audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
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
        }
        else if (fNowTime >= STAGE_TIME * 3)
        {
            if (strPreStatus == "SKY")
            {
                strNowStatus = "SKY_END";

                //フェードSEを再生
                audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            }
            else if (strPreStatus == "SKY_END")
            {
				//	終了エフェクトを生成.
				GetComponent<EffectCreator>().Create("FINISH");

                strNowStatus = "RESULT";
            }
        }
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
    public float GetStageTime() { return STAGE_TIME; }
}