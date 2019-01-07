using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PzPiece : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public MeshRenderer pieceRenderer;
    public MeshRenderer shadowRenderer;

    public Vector2 size=>PzPuzzleManager.instance.size;
    public Material[] materials=>PzPuzzleManager.instance.materials;

    public Vector2 location;

    public List<PzNeiborPiece> neibors=new List<PzNeiborPiece>();
    private Animator animator;

    // Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	}

    [ContextMenu("Setup")]
    public void Setup()
    {
        pieceRenderer.material = PzPuzzleManager.instance.GetPieceMaterial(location);
        shadowRenderer.material = PzPuzzleManager.instance.GetShadowMaterial(location);

        Vector4 tile= new Vector4(2f/size.x,2f/size.y,1);
        Vector4 offset= new Vector4(
            location.x/size.x-1f/(size.x*2),
            location.y/size.y-1f/(size.y*2));

        pieceRenderer.material.SetVector("_Tile",tile);
        pieceRenderer.material.SetVector("_Offset",offset);

        shadowRenderer.material.mainTextureScale = tile;
        shadowRenderer.material.mainTextureOffset = offset;

        transform.localScale = tile;
        transform.localPosition = offset;
        //snap();
    }

    // Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("IsSelected",true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetBool("IsSelected", false);
            checkNeibors();
        //snap();
    }

    private void snap()
    {
        var position = transform.localPosition;
        position.x = position.x - position.x % (1f / (PzPuzzleManager.instance.size.x * 10));
        position.y = position.y - position.y % (1f / (PzPuzzleManager.instance.size.y * 10));
        transform.localPosition = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.IsPointerMoving())
        {
            var start = Camera.allCameras[0].ScreenToWorldPoint(eventData.position - eventData.delta);
            var end = Camera.allCameras[0].ScreenToWorldPoint(eventData.position);
            transform.position += end - start;
        }
    }

    private void checkNeibors()
    {
        foreach (var neibor in neibors)
        {
            if (neibor.distance(transform.position) < PzPuzzleManager.instance.snapDistance)
            {
                transform.position = neibor.GetRelativePos();
            }
        }
    }

    public void SetNeibor(PzPiece piece)
    {
        if(neibors.Any(n=>n.piece==piece))
            return;
        neibors.Add(new PzNeiborPiece(){piece=piece,delta = piece.transform.position-transform.position});
        piece.SetNeibor(this);
    }
}
[Serializable]
public class PzNeiborPiece
{
    public PzPiece piece;
    public Vector3 delta;
    public bool isLinked = false;

    public float distance(Vector3 position)
    {
        return (piece.transform.position - (position + delta)).magnitude;
    }

    public Vector3 GetRelativePos()
    {
        return piece.transform.position - delta;
    }
}