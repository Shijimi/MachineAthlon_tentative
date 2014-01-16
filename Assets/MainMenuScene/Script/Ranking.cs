using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {

	//	レンダラー設定用テクスチャ
	public	Texture	Ranking_Off;
	public	Texture	Ranking_On;
	
	void	Start()
	{
		//	レンダラーをOffに変更.
		renderer.material.mainTexture	=	Ranking_Off;
	}
	
	void	Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			//	光線が当たった時の情報を取得.
			RaycastHit ray_hit;
			//	光線(カメラのヒット情報を取得).
			Ray	ray	=	Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//	タッチ情報を処理.
			if( Physics.Raycast(ray, out ray_hit, 100 ) )
			{
				//	光線がヒットしたオブジェクトを取得
			    GameObject	game_object	=	ray_hit.collider.gameObject;
	
				//	ヒットしたオブジェクトがスタートボタンか？.
				if( game_object == gameObject )
				{
					//	クリックされたらレンダラーをOnに変更.
					renderer.material.mainTexture	=	Ranking_On;
					
				}
			}
		}
	}
}
