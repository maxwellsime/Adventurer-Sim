using System.Collections;
using System.Collections.Generic;

public class NPC : Hunter{
    // Constructor called from world generator code    
    public NPC(string name, int rank, int  age, char gender){
        this.name = name;
        this.rank = rank;
        this.age = age;
        this.gender = gender;
    }
}