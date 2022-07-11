using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    public static MovePiece instance;
    Combat game;
    Piece moving;
    Vector2Int newIndex;
    Vector2 mouseInput;

    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update.
    private void Start(){
        game = GetComponent<Combat>();
    }

    // Update is called once per frame.
    private void Update(){
        if(moving != null){
            Vector2 dir = ((Vector2)Input.mousePosition - mouseInput);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
            newIndex = moving.n.index;
            Vector2Int add = Vector2Int.zero;

            // Mouse is 32 pixels away from the starting point of the mouse ?Remove for specific weapons.
            if(dir.magnitude > 32){
                // Return (1, 0) || (-1, 0) || (0, 1) || (0, -1) depending on mouse direction.
                // Movement on x axis
                if(aDir.x > aDir.y){
                    //add = (new Vector2Int((nDir.x > 0) ? 1 : -1, 0));
                    add.x = nDir.x > 0 ? 1 : -1;
                }
                // Movement on y axis
                else if(aDir.y > aDir.x){
                    //add = (new Vector2Int(0, (nDir.y > 0) ? 1 : -1));
                    add.y = nDir.y > 0 ? 1 : -1;
                }
            }

            newIndex += add;
            Vector2 pos = game.getPosFromVector(moving.n.index);
            if(!newIndex.Equals(moving.n.index)){
                // Negative y because 0,0 is top-left of the gameboard not bottom-left
                pos += add * 16;
            }

            moving.MoveTo(pos); 
        }
    }

    public void Move(Piece piece){
        if(moving != null){
            return;
        }

        moving = piece;
        mouseInput = Input.mousePosition;
    }

    public void DropPiece(){
        if (moving == null){
            return;
        }

        Debug.Log("Dropped!");
        // if newIndex != moving.index
        // Flip pieces around in the game board
        // Else
        // Reset piece back to OG spot

        moving = null;
    }
}
