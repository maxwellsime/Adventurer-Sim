using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int width = 20;
    public int height = 16;
    public Node[,] board;
    System.Random random;
    [Header("UI")]
    public Sprite[] pieces;
    public RectTransform gameBoard;
    [Header("Prefabs")]
    public GameObject piece;

    // Called at first frame of update.
    private void Start(){
        StartGame();
    }

    private void StartGame(){
        string seed = GetRandomSeed();
        random = new System.Random(seed.GetHashCode());
        
        InitializeBoard();
        VerifyBoard();
        InstantiateBoard();
    }

    // Initialize combat board.
    private void InitializeBoard(){
        board = new Node[width, height];

        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                board[x, y] = new Node(RandomVal(), new Point(x, y));
            }
        }
    }

    // Verify the board does not start with existing matches.
    private void VerifyBoard(){
        List<int> used;

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                used = new List<int>();
                Point p = new Point(x,y);
                int val = GetValueFromPoint(p);
                
                while(IsConnected(p, true).Count > 0){
                    val = GetValueFromPoint(p);

                    if(!used.Contains(val)){
                        used.Add(val);
                    }
                    
                    SetValueAtPoint(p, NewVal(ref used));
                }
            }
        }
    }
    
    // Instantiate the board.
    private void InstantiateBoard(){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                int val = board[x, y].Val;

                GameObject b = Instantiate(piece, gameBoard);
                Piece p = b.GetComponent<Piece>();

                // Make 0,0 start at the top-left
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(32 + (64 * x), -32 - (64 * y));
                p.Init(val, new Point(x,y), pieces[val]);
            }
        }
    }

    // Generate random value 0 -> pieces.Length.
    private int RandomVal(){
        int val = 0;
        val = (random.Next(0, 100)/ (100/pieces.Length));
        return val;
    }

    /* Checks if the board has any connected matches, if so gets rid of them.
        Point p = Node position being checked for matches.
        bool main = Used to run the code twice, guaranteeing a clean board. */
    private List<Point> IsConnected(Point p, bool main){
        List<Point> connected = new List<Point>();
        // List of direction functions, ordered to make looping easier.
        Point[] directions = { 
            Point.Up(), 
            Point.Right(), 
            Point.Down(), 
            Point.Left() 
        }; 
        int val = GetValueFromPoint(p);

        // Check for nodes matching val from any direction from p.
        foreach(Point dir in directions){
            List<Point> line = new List<Point>();
            int count = 0;
    
            for(int i = 1; i < 3; i++){
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
            Point[] check = {   
                Point.Add(p, directions[i]), 
                Point.Add(p, directions[i + 2]) 
            }; 
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

        // Check for nodes matching val in 2x2
        for(int i = 0; i < 4; i++){
            List<Point> square = new List<Point>();
            int next = i + 1;
            int count = 0;

            if(next >= 4){
                next -= 4;
            }
            // Array of points that create a square.
            Point[] check = { 
                Point.Add(p, directions[i]), 
                Point.Add(p, directions[next]), 
                Point.Add(p, Point.Add(directions[i], directions[next])) 
            };

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
                AddPoints(ref connected, IsConnected(connected[i], false));
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
    private void AddPoints(ref List<Point> connected, List<Point> add){
        foreach(Point p in add){
            bool unique = true;

            for(int i = 0; i < connected.Count; i++){
                if(connected[i].Equals(p)){
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
    private int GetValueFromPoint(Point p){
        if(p.x < 0 || p.x >= width || p.y < 0 || p.y >= height){
            return -1;
        }

        return board[p.x, p.y].Val;
    }

    // Sets node value at Point p inside board array.
    private void SetValueAtPoint(Point p, int v){
        board[p.x, p.y].Val = v;
    }

    // Get position of piece on the gameboard from point.
    public Vector2 getPosFromPoint(Point p){
        return new Vector2(32 + (64 *p.x), -32 - (64 * p.y));
    }

    // Returns new node value that isn't inside List used.
    private int NewVal(ref List<int> used){
        List<int> usable = new List<int>();

        if(used.Count == pieces.Length){
            Debug.Log("usable count <= 0");
            return RandomVal();
        }
        for(int i = 0; i < pieces.Length; i++){
            usable.Add(i);
        }
        foreach(int i in used){
            usable.Remove(i);
        }

        return usable[random.Next(0, usable.Count)];
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
