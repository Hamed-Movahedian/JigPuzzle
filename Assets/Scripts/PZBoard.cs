using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PZBoard : MonoBehaviour
{
    public PzPiece picePrefab;
    public Vector2 size;

    // Use this for initialization
    void Start()
    {
        //setup();
    }
    [ContextMenu("Setup")]
    private void setup()
    {
        while (transform.childCount > 0)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(0).gameObject);
            else
                DestroyImmediate(transform.GetChild(0).gameObject);
        }
        PzPiece[,] pieceMat = new PzPiece[(int)size.x, (int)size.y];

        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                var piece = (PzPiece)PrefabUtility.InstantiatePrefab(picePrefab);
                piece.location = new Vector2(x, y);
                piece.transform.SetParent(transform);
                piece.Setup();
                pieceMat[x, y] = piece;
            }

        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                if (x + 1 < size.x)
                    pieceMat[x, y].SetNeibor(pieceMat[x + 1, y]);
                if (y + 1 < size.y)
                    pieceMat[x, y].SetNeibor(pieceMat[x, y + 1]);
            }

        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                pieceMat[x, y].transform.localPosition=new Vector3(
                    Random.Range(-0.5f,1f),
                    Random.Range(-0.5f,1f),0);
                
            }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
