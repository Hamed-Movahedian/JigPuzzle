using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PzPuzzleManager : MonoBehaviour
{
    public static PzPuzzleManager instance;
    public Vector2 size;
    public Material[] materials;
    public Material[] shadowMaterials;

    private Dictionary<Vector2, int> _posToIndex = new Dictionary<Vector2, int>
    {
        {new Vector2(0,0),0 },
        {new Vector2(1,0),1 },
        {new Vector2(0,1),2 },
        {new Vector2(1,1),3 },
    };

    public float snapDistance;

    private void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Material GetPieceMaterial(Vector2 location)
    {
        Vector2 pos = new Vector2(location.x % 2, location.y % 2);
        return materials[_posToIndex[pos]];
    }
    public Material GetShadowMaterial(Vector2 location)
    {
        Vector2 pos = new Vector2(location.x % 2, location.y % 2);
        return shadowMaterials[_posToIndex[pos]];
    }
}
