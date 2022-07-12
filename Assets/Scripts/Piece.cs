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

    public void Init(int val, Vector2Int v, Sprite piece){
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        n = new Node(val, v);
        img.sprite = piece;

        SetIndex(v);
    }

    // Set node index values to point v.
    public void SetIndex(Vector2Int v){
        n.index = v;
        Reset();
        UpdateName();
    }

    // Reset graphical position of the piece to be correct to the current index.
    public void Reset(){
        rect.anchoredPosition = new Vector2(40 + (80 * n.index.x), -40 - (80 * n.index.y));
    }

    // Move anchoredPosition towards pos.
    public void Move(Vector2 pos){
        rect.anchoredPosition += pos * Time.deltaTime * 16f;
        //?? redundant ??
    }

    // Move anchoredPosition to pos.
    public void MoveTo(Vector2 pos){
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, pos, Time.deltaTime * 16f);
    }

    // Changes name of Piece so that it reflects the current index and value.
    private void UpdateName(){
        this.name = "Node [" + n.index.x + ", " + n.index.y + "  :  " + n.Val +"]";
    }

    // Updates "updating" variable to show if the piece is currently being moved.
    public bool UpdatePiece(){
        return true;
        //return false when it is not moving
    }

    // Called when user inputs pointer (mouse click) on this object.
    public void OnPointerDown(PointerEventData eventData){
        if (updating) return;
        MovePiece.instance.Move(this);
        Debug.Log("-----------Grab" + transform.name);
    }

    // Called when user cancels pointer (mouse click) input on this object.
    public void OnPointerUp(PointerEventData eventData){
        MovePiece.instance.DropPiece();
        Debug.Log("-----------Let go of " + transform.name);
    }
}
