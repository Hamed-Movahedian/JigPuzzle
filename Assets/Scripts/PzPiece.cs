using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PzPiece : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public MeshRenderer pieceRenderer;
    public MeshRenderer shadowRenderer;

    public Vector2 size=>PzPuzzleManager.instance.size;
    public Material[] materials=>PzPuzzleManager.instance.materials;

    public Vector2 location;

    private Animator animator;

    // Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	    Setup();
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
        animator.SetBool("IsSelected",false);
        
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
}
