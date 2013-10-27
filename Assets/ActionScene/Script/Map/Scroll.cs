using UnityEngine;
using System.Collections;

/*
=====================================
スクロール
=====================================
このスクリプトを適用させると
プレイヤーの速さに応じて移動します。
=====================================
*/

public class Scroll : MonoBehaviour
{
    [SerializeField]
    private GameObject cPlayer;

	// Use this for initialization
	void Start ()
    {
        cPlayer = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position -= new Vector3(0.0f, 0.0f, cPlayer.GetComponent<Player>().GetSpeed());

        if (gameObject.transform.position.z < -50)
        {
            Destroy(gameObject);
        }
	}
}
