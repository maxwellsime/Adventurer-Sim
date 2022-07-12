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

    // Update is called once per frame
    // If moving variable is assigned a value then it is moved in the direction of the mouse input.
    private void Update(){
        if(moving != null){
            // Mouse position of the current frame - mouse position of when the script was activated.
            Vector2 dir = ((Vector2)Input.mousePosition - mouseInput);
            // Absolute values used to determine direction of movement.
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
            // Normalized to determine if the intended axis is positive or negative.
            Vector2 nDir = dir.normalized;
            newIndex = moving.n.index;
            Vector2Int add = Vector2Int.zero;

            // Mouse is 32 pixels away from the starting point of the mouse ?Remove for specific weapons.
            if(dir.magnitude > 32){
                // Return (1, 0) || (-1, 0) || (0, 1) || (0, -1) depending on mouse direction.
                // Movement on x axis.
                if(aDir.x > aDir.y){
                    add.x = nDir.x > 0 ? 1 : -1;
                }
                // Movement on y axis.
                else if(aDir.y > aDir.x){                    
                    add.y = nDir.y > 0 ? 1 : -1;
                }
            }

            // Index of the position trying to move to.
            newIndex += add;
            Vector2 pos = game.getPosFromVector(moving.n.index);
            if(!newIndex.Equals(moving.n.index)){
                pos += add * 16;
            }

            // Move moving variable to direction of mouse movement.
            moving.MoveTo(pos); 
        }
    }

    // Saves the piece as the moving variable, causing the movement action next frame, from Update().
    public void Move(Piece piece){
        if(moving != null){
            return;
        }

        moving = piece;
        mouseInput = Input.mousePosition;
    }

    // Resets index position if the movement is disallowed then nullifies moving variable.
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
