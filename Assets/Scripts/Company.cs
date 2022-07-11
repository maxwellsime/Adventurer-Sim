using System.Collections;
using System.Collections.Generic;

public class Company{
    string name;
    List<Hunter> members = new List<Hunter>();
    // Chemistry value dependant on specific stat differences between party members.
    int chemistry;

    public Company(string name, List<Hunter> members){
        this.name = name;
        this.members = members;
    }

    // Calculate chemistry values from specific stat differences.
    public void CalculateChemistry(){
    }
}