using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Point used instead of Vector2, means you don't have to cast to int
[System.Serializable]
public class Point
{
    public int x;
    public int y;

    public Point(int x, int y){
        this.x = x;
        this.y = y;
    }

    public bool Equals(Point p){
        return (x == p.x && y == p.y);
    }

    public void Mult(Point p){
        x *= p.x;
        y *= p.y;
    }

    public void Add(Point p){
        x += p.x;
        y += p.y;
    }
    
    public static Point FromVector2(Vector2 v){
        return new Point((int)v.x, (int)v.y);
    }
    
    public static Point Mult(Point p, int n){
        return new Point(p.x * n, p.y * n);
    }

    public static Point Add(Point p, Point n){
        return new Point(p.x + n.x, p.y + n.y);
    }

    public static Point Clone(Point p){
        return new Point(p.x, p.y);
    }
    
    public static Point Up(){
        return new Point(0, 1);
    }
    public static Point Down(){
        return new Point(0, -1);
    }

    public static Point Left(){
        return new Point(-1, 0);
    }

    public static Point Right(){
        return new Point(1, 0);
    }
}
