using UnityEngine;
using System.Collections;

public class SpeedUpItem : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //点滅アニメーション
        float fColor = 1.5f - Mathf.Sin(Time.time * 10.0f);
        renderer.material.color = new Color(fColor, fColor, fColor);
	}
}
