namespace AnimalZoo;

class Birds : Animal{
  public bool canFly;

  public Birds(string name, int age, bool canfly) : base(name, age){
    canFly = canfly;
  }

  public virtual void Fly(int m) {
        Console.Write($"How far should {Name} fly?: ");
        int distance;
        if (int.TryParse(Console.ReadLine(), out distance)) {
            Console.WriteLine($"{Name} flew {distance} meters.");
        } else {
            Console.WriteLine($"{Name} stood still.");
        }
    }

  public override void Greeting(){

  }
}


class Parrot : Birds{

    public override void Greeting(){
        Console.WriteLine($"Hello! I am a parrot named {Name}, I am {Age} years old. I can mimic human speech!");
    }
    public Parrot(string name, int age, bool canfly) : base(name, age, canfly) {
  }

  public void Speak(){
    Console.WriteLine("How should the parrot say?: ");
    string speak = Console.ReadLine();
    Console.WriteLine("The parrot said " + speak);
  }

    public override void Fly(int m)
    {
        base.Fly(m);
    }
}

class Penguin : Birds{
  public Penguin(string name, int age, bool canfly) : base(name, age, canfly) {

  }
    public override void Greeting()
    {
        Console.WriteLine($"Hello! I am a Penguin named {Name}, I am {Age} years old. I waddle around!");
    }

    public override void Fly(int m)
    {
        Console.WriteLine("This bird can not fly!");
    }
}