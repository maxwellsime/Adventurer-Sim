using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Node n;
    public Vector2 pos;
    public RectTransform rect;
    bool updating;
    Image img;

    public void Init(int v, Point p, Sprite piece){
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        n = new Node(v, p);
        img.sprite = piece;

        SetIndex(p);
    }

    // Set node index values to point p
    public void SetIndex(Point p){
        n.index = p;
        Reset();
        UpdateName();
    }

    // Reset graphical position of the piece to be correct to the current index
    public void Reset(){
        rect.anchoredPosition = new Vector2(32 + (64 * n.index.x), -32 - (64 * n.index.y));
    }

    // Changes name of Piece so that it reflects the current index and value
    private void UpdateName(){
        this.name = "Node [" + n.index.x + ", " + n.index.y + "  :  " + n.Val +"]";
    }

    public bool UpdatePiece(){
        return true;
        //return false when it is not moving
    }

    // Called when user inputs pointer (mouse click) on this object
    public void OnPointerDown(PointerEventData eventData){
        if (updating) return;
        Debug.Log("Grab" + transform.name);
    }

    // Called when user cancels pointer (mouse click) input on this object
    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Let go of " + transform.name);
    }
}
