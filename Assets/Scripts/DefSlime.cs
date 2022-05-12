using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour{
    /*private Vector2 firstTouchPos; // Original mouse position
    private Vector2 finalTouchPos; // Mouse position after movement
    private Vector2 tempPos;
    private GameObject otherSlime;
    private board board;
    public float moveAngle = 0; // Calculated angle of mouse movement
    public int col;
    public int row;
    public int tarCol;
    public int tarRow;
    public bool matched;
    

    // Start is called before the first frame update
    void Start(){
        board = FindObjectOfType<board>();
        tarCol = (int)transform.position.x;
        tarRow = (int)transform.position.y;
        col = tarCol;
        row = tarRow;
    }

    // Update is called once per frame
    void Update(){
        tarCol = col;
        tarRow = row;

        if(matched){    // Testing
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }else if(!matched){
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(255, 255, 255, 1);
        }
        
        // X-axis movement
        if (Mathf.Abs(tarCol - transform.position.x) > .1){
            // Move towards target
            tempPos = new Vector2(tarCol, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }else{
            // Directly set the position
            tempPos = new Vector2(tarCol, transform.position.y);
            transform.position = tempPos;
            board.slimesArr[col, row] = this.gameObject;
        }

        // Y-axis movement
        if (Mathf.Abs(tarRow - transform.position.y) > .1){
            // Move towards target
            tempPos = new Vector2(transform.position.x, tarRow);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }else{
            // Directly set the position
            tempPos = new Vector2(transform.position.x, tarRow);
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
        MoveSlime();
        Update();
        FindMatches();
    }

    // Calculate angle of mouse movement
    void CalculateAngle(){
        moveAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
    }

    void MoveSlime(){
        if(moveAngle > -45 && moveAngle <= 45 && col < board.width - 1){
            // Move Right
            otherSlime = board.slimesArr[col + 1, row];
            otherSlime.GetComponent<slime>().col -= 1;
            col += 1;
        }else if(moveAngle > 45 && moveAngle <= 135 && row < board.height - 1){
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

    // Changes bool "match" if in adjacent to multiple other slimes of the same tag
    void FindMatches(){
        GameObject tempGO;
        GameObject prevGO = null;
        int n = -1;

        if(row + 1 < board.height - 1){
            // Check Right
            tempGO = board.slimesArr[col, row + 1];
            if(this.gameObject.tag == tempGO.tag){
                if(MatchSearchRow(tempGO, 1)){
                    tempGO.GetComponent<slime>().matched = true;
                    this.matched = true;
                }else{
                    prevGO = tempGO;
                }
            }
        }
        if(row - 1 >= 0){
            // Check Left
            tempGO = board.slimesArr[col, row - 1];
            if(this.gameObject.tag == tempGO.tag){
                if(prevGO != null){
                    prevGO.GetComponent<slime>().matched = true;
                }
                if(MatchSearchRow(tempGO, n)){
                    tempGO.GetComponent<slime>().matched = true;
                }
            }
        }
    }

    // Recursive matching function, checks for matches in row.
    // g = starting GameObject to search from;       i = index direction to search
    bool MatchSearchRow(GameObject g, int i){
        bool r = false;
        Debug.Log("g row = " + g.GetComponent<slime>().row);
        int tempRow = g.GetComponent<slime>().row + i;
        Debug.Log("TempRow = " + tempRow);

        if(tempRow < board.height -1 && tempRow >= 0){
            GameObject tempGO = board.slimesArr[col, tempRow];
            Debug.Log(col + "," + g.GetComponent<slime>().row + " v " + col + "," + tempRow);
            Debug.Log(g.tag + " v " + tempGO.tag);

            if(g.tag == tempGO.tag){
                r = true;
                tempGO.GetComponent<slime>().matched = true;
                Debug.Log("Tags are equal");
                //MatchSearchRow(tempGO, i);
            }
        }

        return r;
    }

    // Recursive matching function, checks for matches in column.
    // g = starting GameObject to search from;       i = index direction to search
    bool MatchSearchCol(GameObject g, int i){
        bool r = false;
        int tempCol = g.GetComponent<slime>().col + i;

        if(tempCol < board.width -1 && tempCol >= 0){
            GameObject tempGO = board.slimesArr[tempCol, row];

            if(g.tag == tempGO.tag){
                r = true;
                tempGO.GetComponent<slime>().matched = true;
                MatchSearchRow(tempGO, i);
            }
        }

        return r;
    }
    */
}
