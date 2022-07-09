public class Quirk{
    string name;
    // List of effects that Hunter class uses when calculating gameplay outcomes.
    Dictionary<string, int> effects = new Dictionary<string, int>();

    public void Quirk(string name, Dictionary<string, int> effects){
        this.name = name;
        this.effects = effects;
    }
}