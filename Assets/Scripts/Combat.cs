using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //[Header]
    public int width = 20;
    public int height = 16;
    public Node[,] board;
    // List of available pieces.     
    public Sprite[] pieces;
    //[Header]
    System.Random random;

    // Called at first frame of update.
    void Start(){
        string seed = GetRandomSeed();
        random = new System.Random(seed.GetHashCode());
        InitializeBoard();    
    }

    // Initialize combat board.
    void InitializeBoard(){
        board = new Node[width, height];

        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                board[x, y] = new Node(RandomPiece(), new Point(x, y));
            }
        }
    }

    // Generate random piece 0 -> pieces.Length.
    private int RandomPiece(){
        int val = 0;
        val = (random.Next(0, 100)/ (100/pieces.Length));
        return val;
    }

    // Verify the board does not start with existing matches.
    void VerifyBoard(){
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                Point p = new Point(x,y);
                int val = getValueFromVector(v);
                if(val <= 0) continue;
            }
        }
    }

    /* Checks if the board has any connected matches, if so gets rid of them.
        Point p = Node position being checked for matches.
        bool main = Used to run the code twice, guaranteeing a clean board. */
    List<Point> IsConnected(Point p, bool main){
        List<Point> connected = new List<Point>();
        // List of direction functions, ordered to make looping easier.
        Point[] directions = { Point.Up, Point.Left, Point.Down, Point.Right }; 
        int val = GetValueFromPoint(p);

        // Check for nodes matching val from any direction from p.
        foreach(Point dir in directions){
            List<Point> line = new List<Point>();
            int count = 0;
    
            for(int i = 0; i < 3; i++){
                Point check = Point.Add(p, Point.Mult(dir, i));
                if(GetValueFromPoint(check) == val){
                    line.Add(check);
                    count++;
                }
            }

            // Count = 2 or more, there exists a current match.
            if(count > 1){
                AddPoints(ref connected, line);
            }
        }

        // Check for nodes matching val in adjacent points.
        for(int i = 0; i < 2; i++){
            List<Point> line = new List<Point>();
            // Array of adjacent points, i=0 {up, down}, i=1 {left, right}.
            Point[] check = {   Point.Add(p, directions[i]), 
                                Point.Add(p, directions[i + 2]) }; 
            int count = 0;

            foreach(Point c in check){
                if(GetValueFromPoint(c) == val){
                    line.Add(c);
                    count++;
                }
            }

            if(count > 1){
                AddPoints(ref connected, line);
            }
        }

        // Check for nodes matching val in 2x2 ?Maybe dont use, test.
        for(int i = 0; i < 4; i++){
            List<Point> square = new List<Point>();
            // Array of points that create a square.
            Point[] check = { Point.Add(p, directions[i]), Point.Add(p, directions[next]), Point.Add(p, Point.Add(direcionts[i], directions[next])) };
            int count = 0;
            int next = i + 1;

            if(next > 3){
                next -= 4;
            }

            foreach(Point c in check){
                if(GetValueFromPoint(c) == val){
                    square.Add(c);
                    count++;
                }
            }
            
            // Count = 3 or more, a 2x2 of matching values exist.
            if(count > 2){
                AddPoints(ref connected, square);
            }
        }

        // Re-check connected for each member added to connected list.
        if(main){
            for(int i = 0; i < connected.Count; i++){
                AddPoints(ref connected, isConnected(connected[i], false));
            }
        }

        if(connected.Count > 0){
            connected.Add(p);
        }

        return connected;
    }

    /* Add points from a given list to the referred connected.
        ref List<Point> connected = referenced list of currently connected points that need to have their values manipulated.
        List<Point> Add = list of points that need to be appended to connected.
    */
    void AddPoints(ref List<Point> connected, List<Point> add){
        foreach(Point p in add){
            bool unique = true;

            for(int i = 0; i < connected.Count; i++){
                if(points[i].Equals(p)){
                    unique = false;
                    break;
                }
            }

            // If Point p is not already inside connected then add it.
            if(unique){
                connected.Add(p);
            }
        }
    }

    // Returns node value at Point p inside board array.
    int GetValueFromPoint(Point p){
        return board[p.x, p.y].Val;
    }

    // Generate seed for improved random.
    private string GetRandomSeed(){
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRTUSVWXYZabcdefghijklmnopqrstuvwxyz1234567890!£$#";

        for (int i = 0; i < 20; i++){   
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }
}
