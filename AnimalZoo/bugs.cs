namespace AnimalZoo;

class Bugs : Animal{
  public bool hasWings;
  
  public Bugs(string name, int age, bool haswings) : base(name, age){
    hasWings = haswings;
  }

  public override void Greeting(){

  }
}



class Ant : Bugs{

    public override void Greeting()
    {
        Console.WriteLine($"Hello! I am a Ant named {Name}, I am {Age} years old. I am tiny!");
    }
    public Ant(string name, int age, bool haswings) : base(name, age, haswings) {

  }
}