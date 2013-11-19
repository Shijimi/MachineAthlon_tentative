using UnityEngine;
using System.Collections;

//=========================================
//擬似2DでPlaneオブジェクトを画像のように
//扱うためのスクリプトです。
//=========================================
public class SetPos2D : MonoBehaviour {

    public Vector2 m_vPos;                      //画像の左上座標
	public Vector2 m_vSize;                     //画像のサイズ
    public GameObject m_cStandardObj;           //基準とするオブジェクト
    public float m_fHeight;                     //基準とするオブジェクトからの距離

    // Use this for initialization
    void Start()
    {
        //基準のオブジェクトが空の場合自分自身を使用する
        if (m_cStandardObj == null) { m_cStandardObj = gameObject; }

        //基準のオブジェクトから距離
        float fDistance = System.Math.Abs(m_cStandardObj.transform.position.y - Camera.main.transform.position.y) - m_fHeight;

        Vector2 vPos,vSize,vView;
        vView.x = Camera.main.GetComponent<CameraSetting>().m_fViewWidth;
        vView.y = Camera.main.GetComponent<CameraSetting>().m_fViewHeight;

        //1:X = V : size
        vSize.x = m_vSize.x / vView.x;
        vSize.y = m_vSize.y / vView.y;

        vPos.x = m_vPos.x / vView.x;
        vPos.y = m_vPos.y / vView.y;


        //変換後の左上・右下の3D座標を取得する
        Vector3 vTargetPos1 = Camera.main.ViewportToWorldPoint(new Vector3(vPos.x, vPos.y, fDistance));
        Vector3 vTargetPos2 = Camera.main.ViewportToWorldPoint(new Vector3(vPos.x + vSize.x, vPos.y + vSize.y, fDistance));

        Vector3 vScale;
        vScale.x = (vTargetPos2.x - vTargetPos1.x);
        vScale.y = 1;
        vScale.z = (vTargetPos2.z - vTargetPos1.z);

        //スケール値を設定
        gameObject.transform.localScale = vScale / 10;

        //オブジェクトを置くべき座標を取得
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(vPos.x + (vSize.x / 2.0f), 1.0f - vPos.y - (vSize.y / 2.0f), fDistance));
    }
}
