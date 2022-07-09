public class Company{
    string name;
    List<Hunter> members;
    // Chemistry value dependant on specific stat differences between party members.
    int chemistry;

    public void Company(string name, List<Hunter> members){
        this.name = name;
        this.members = members;
    }

    // Calculate chemistry values from specific stat differences.
    public void CalculateChemistry(){
        chemistry = 100;
    }
}