using UnityEngine;
using System.Collections;

//=========================================
//擬似2DでPlaneオブジェクトを画像のように
//座標・サイズを指定できるスクリプトです。
//=========================================
public class SetPos2D : MonoBehaviour
{
    public Vector2 m_vPos;                      //画像の左上座標
	public Vector2 m_vSize;                     //画像のサイズ
    public Vector2 m_vScale = Vector2.one;      //拡大率

    public bool m_bPosCenterX = false;          //X座標を中央よせするかどうか
    public bool m_bPosCenterY = false;          //Y座標を中央よせするかどうか
    public bool m_bSizeMaxX = false;            //幅を画面サイズにあわせる
    public bool m_bSizeMaxY = false;            //高さを画面サイズにあわせるかどうか

    public bool m_bPosKeep = false;             //座標の設定を行わない

    public GameObject m_cBaseObj;               //基準とするオブジェクト(親オブジェクト)
    public float m_fHeight;                     //基準とするオブジェクトからの距離(親オブジェクトの高さ+1 のような指定が可能)

    // Use this for initialization
    void Start()
    {
        //基準のオブジェクトが空の場合自分自身を使用する
        if (m_cBaseObj == null) { m_cBaseObj = gameObject; }

        //基準のオブジェクトから距離
        float fDistance = System.Math.Abs(m_cBaseObj.transform.position.y - Camera.main.transform.position.y) - m_fHeight;

        Vector2 vPos,vSize,vView;
        vView.x = Camera.main.GetComponent<CameraSetting>().m_fViewWidth;
        vView.y = Camera.main.GetComponent<CameraSetting>().m_fViewHeight;

        //画像の幅を設定
        if (m_bSizeMaxX)
        {
            vSize.x = 1.0f * m_vScale.x;
        }
        else
        {
            vSize.x = (m_vSize.x / vView.x) * m_vScale.x;
        }

        //画像の高さを設定
        if (m_bSizeMaxY)
        {
            vSize.y = 1.0f * m_vScale.y;
        }
        else
        {
            vSize.y = (m_vSize.y / vView.y) * m_vScale.y;
        }

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

        Vector3 vTransPos = new Vector3(0,0,0);

        //X座標を中央寄せする場合
        if (m_bPosCenterX)
        {
            //中央に配置
            vTransPos.x = 0.5f; 
        }
        //X座標を中央寄せしない場合
        else
        {
            //設定した座標に配置
            vTransPos.x = vPos.x + (vSize.x / 2.0f);
        }

        //Y座標を中央寄せする場合
        if (m_bPosCenterY)
        {
            //中央に配置
            vTransPos.y = 0.5f;
        }
        //Y座標を中央寄せしない場合
        else
        {
            //設定した座標に配置
            vTransPos.y = 1.0f - vPos.y - (vSize.y / 2.0f);
        }

        //Z座標を設定
        vTransPos.z = fDistance;

        //座標の設定を行う場合は座標を設定
        if (!m_bPosKeep)
        {
            gameObject.transform.position = Camera.main.ViewportToWorldPoint(vTransPos);
        }
    }
}
