using UnityEngine;
using System.Collections;

public class EffectSpin : MonoBehaviour {

	//	レンダラー設定用テクスチャ.
    public	Texture Spin_01;
    public	Texture Spin_02;
	
	public	int		CntSpeed;		//	レンダラー変更スピード.
	public	int		EffectTime;		//	エフェクト持続時間.
	
	int	m_iTimeCnt;					//	カウント.

    void Start()
    {
        //	レンダラーを変更.
        renderer.material.mainTexture	=	Spin_01;

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
			if( renderer.material.mainTexture == Spin_01 )
				renderer.material.mainTexture	=	Spin_02;
			else
				renderer.material.mainTexture	=	Spin_01;
		}
		
		//	時間でオブジェクトを削除.
		if( m_iTimeCnt >= EffectTime )
			Destroy(gameObject); 
    }
}
