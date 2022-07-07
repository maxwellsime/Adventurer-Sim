using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public static PieceMove instance;
    Combat game;
    Piece target;
    Point newIndex;
    Vector2 mouseInput;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update.
    private void Start()
    {
        game = GetComponent<Combat>();
    }

    // Update is called once per frame.
    private void Update()
    {
        if(target != null){
            Vector2 dir = ((Vector2)Input.mousePosition - mouseInput);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
            newIndex = Point.clone(target.index);
            Point add = Point.zero;

            // mouse is 32 pixels away from the starting point of the mouse ?Remove for specific weapons.
            if(dir.magnitude > 32){
                // return (1, 0) || (-1, 0) || (0, 1) || (0, -1) depending on mouse direction.
                // movement on x axis
                if(aDir.x > aDir.y){
                    add = (new Point((nDir.x > 0) ? 1 : -1, 0));
                }
                // movement on y axis
                else if(aDir.y > aDir.x){
                    add = (new Point(0, (nDir.y > 0) ? 1 : -1));
                }
            }

            newIndex.add(add);
            Vector2 pos = game.getPosFromPoint(moving.index);
            if(!newIndex.Equals(moving.index)){
                pos += Point.mult(new Point(add.x, -add.y), 16).ToVector();
            }

            
        }
    }
}
