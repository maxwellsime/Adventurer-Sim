using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //[Header]
    public int width = 20;
    public int height = 16;
    public Node[,] board;
    public Sprite[] pieces;     // List of available pieces
    //[Header]
    System.Random random;

    // Called at first frame of update
    void Start(){
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());
        InitializeBoard();    
    }

    // Initialize combat board
    void InitializeBoard(){
        board = new Node[width, height];

        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                board[x, y] = new Node(RandomPiece(), new Vector2(x, y));
            }
        }
    }

    // Generate random piece 0 - pieces.Length
    private int RandomPiece(){
        int val = 1;
        val = (random.Next(0, 100)/ (100/pieces.Length));
        return val;
    }

    // Verify the board does not start with existing matches
    void VerifyBoard(){
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                Vector2 v = new Vector2(x, y);
                int val = getValueFromVector(v);
            }
        }
    }

    // returns value at Node inside board using Vector2 v
    int getValueFromVector(Vector2 v){
        return board[(int)v.x, (int)v.y].value;
    }

    // Generate seed for improved random
    private string getRandomSeed(){
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRTUSVWXYZabcdefghijklmnopqrstuvwxyz1234567890!£$#";

        for (int i = 0; i < 20; i++){   
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }
}
