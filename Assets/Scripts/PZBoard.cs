﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PZBoard : MonoBehaviour
{
    public PzPiece picePrefab;
    public Vector2 size;

    // Use this for initialization
    void Start ()
    {
        //setup();
    }
    [ContextMenu("Setup")]
    private void setup()
    {
        while (transform.childCount>0)
        {
            if(Application.isPlaying)
                Destroy(transform.GetChild(0).gameObject);
            else
                DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var piece = (PzPiece)PrefabUtility.InstantiatePrefab(picePrefab);
                piece.location=new Vector2(x,y);
                piece.transform.SetParent(transform);
                piece.Setup();
            }
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
