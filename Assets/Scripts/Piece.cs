using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public Node n;
    public Vector2 pos;
    public RectTransform rect;

    Image img;

    public void Init(int v, Point p, Sprite piece){
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        n = new Node(v, p);
        img.sprite = piece;

        SetIndex(p);
    }

    public void SetIndex(Point p){
        n.index = p;
        Reset();
        UpdateName();
    }

    public void Reset(){
        rect.anchoredPosition = new Vector2(32 + (64 * n.index.x), -32 - (64 * n.index.y));
    }

    private void UpdateName(){
        this.name = "Node [" + n.index.x + ", " + n.index.y + "  :  " + n.Val +"]";
    }
}
