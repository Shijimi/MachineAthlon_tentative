using UnityEngine;
using System.Collections;

public class EffectKirakira : MonoBehaviour {

	//	レンダラー設定用テクスチャ.
    public	Texture Kirakira_01;
    public	Texture Kirakira_02;
	
	public	int		CntSpeed;		//	レンダラー変更スピード.
	public	int		EffectTime;		//	エフェクト持続時間.
	
	int	m_iTimeCnt;					//	カウント.

    void Start()
    {
		//	座標修正.
		transform.position	+=	new Vector3( 0.0f, 4.0f, 0.0f );
		
        //	レンダラーを変更.
        renderer.material.mainTexture	=	Kirakira_01;

		//	カウントを０に.
		m_iTimeCnt	=	0;
    }
	
	void Update()
    {
		//	カウントを進める.
		m_iTimeCnt++;
		
		//	カウントが一定値を超えたらレンダラー変更.
		if( m_iTimeCnt % CntSpeed == 0 )
		{
			if( renderer.material.mainTexture == Kirakira_01 )
				renderer.material.mainTexture	=	Kirakira_02;
			else
				renderer.material.mainTexture	=	Kirakira_01;
		}
		
		//	時間でオブジェクトを削除.
		if( m_iTimeCnt >= EffectTime )
			Destroy(gameObject); 
    }
}
