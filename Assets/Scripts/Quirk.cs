using System.Collections;
using System.Collections.Generic;

public class Quirk{
    public string name;
    // List of effects that Hunter class uses when calculating gameplay outcomes.
    public Dictionary<string, int> effects = new Dictionary<string, int>();

    public Quirk(string name, Dictionary<string, int> effects){
        this.name = name;
        this.effects = effects;
    }
}