﻿using UnityEngine;
using System.Collections;

//============================
//アス比を固定するスクリプト
//============================

public class CameraSetting : MonoBehaviour {

    public float m_fViewHeight;
    public float m_fViewWidth;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Awake()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        float baseAspect = m_fViewHeight / m_fViewWidth;
        float nowAspect = (float)Screen.height / (float)Screen.width;
        float changeAspect;

        if (baseAspect > nowAspect)
        {
            changeAspect = nowAspect / baseAspect;
            cam.rect = new Rect((1 - changeAspect) * 0.5f, 0, changeAspect, 1);
        }
        else
        {
            changeAspect = baseAspect / nowAspect;
            cam.rect = new Rect(0, (1 - changeAspect) * 0.5f, 1, changeAspect);
        }
    }
}
