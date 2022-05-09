using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour{
    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    public float moveAngle = 0;
    public int col;
    public int row;
    public int tarX;
    public int tarY;
    private GameObject otherSlime;
    private board board;
    private Vector2 tempPos;

    // Start is called before the first frame update
    void Start(){
        board = FindObjectOfType<board>();
        tarX = (int)transform.position.x;
        tarY = (int)transform.position.y;
        col = tarX;
        row = tarY;
    }

    // Update is called once per frame
    void Update(){
        tarX = col;
        tarY = row;
        
        // X-axis movement
        if (Mathf.Abs(tarX - transform.position.x) > .1){
            // Move towards target
            tempPos = new Vector2(tarX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }else{
            // Directly set the position
            tempPos = new Vector2(tarX, transform.position.y);
            transform.position = tempPos;
            board.slimesArr[col, row] = this.gameObject;
        }

        // Y-axis movement
        if (Mathf.Abs(tarY - transform.position.y) > .1){
            // Move towards target
            tempPos = new Vector2(transform.position.x, tarY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }else{
            // Directly set the position
            tempPos = new Vector2(transform.position.x, tarY);
            transform.position = tempPos;
            board.slimesArr[col, row] = this.gameObject;
        }
    }

    private void OnMouseDown() {
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp() {
        finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    // Calculate angle of mouse movement
    void CalculateAngle(){
        moveAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
        MoveSlime();
    }

    void MoveSlime(){
        if(moveAngle > -45 && moveAngle <= 45 && col < board.width){
            // Move Right
            otherSlime = board.slimesArr[col + 1, row];
            otherSlime.GetComponent<slime>().col -= 1;
            col += 1;
        }else if(moveAngle > 45 && moveAngle <= 135 && row < board.height){
            // Move Upwards
            otherSlime = board.slimesArr[col, row + 1];
            otherSlime.GetComponent<slime>().row -= 1;
            row += 1;
        }else if((moveAngle > 135 || moveAngle <= -135) && col > 0){
            // Move Left
            otherSlime = board.slimesArr[col - 1, row];
            otherSlime.GetComponent<slime>().col += 1;
            col -= 1;
        }else if(moveAngle < -45 && moveAngle >= -135 && row > 0){
            // Move Downwards
            otherSlime = board.slimesArr[col, row - 1];
            otherSlime.GetComponent<slime>().row += 1;
            row -= 1;
        }
    }
}
