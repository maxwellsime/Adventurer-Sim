using System.Collections;
using System.Collections.Generic;

public class Player : Hunter{
    // Constructor called from player creation menu
    public Player(string name, int rank, int  age, char gender, Dictionary<string, int> stats){
        this.name = name;
        this.rank = rank;
        this.age = age;
        this.gender = gender;
        this.stats = stats;
    }
}