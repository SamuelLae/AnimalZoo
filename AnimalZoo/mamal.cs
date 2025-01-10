namespace AnimalZoo;

class Mamals : Animal{
  public bool isNocturnal;
  public bool isAsleep;

    public Mamals(string name, int age, bool isasleep) : base(name, age)
    {
    }

    public Mamals(string name, int age, bool isnocturnal, bool isasleep) : base(name, age){
    isNocturnal = isnocturnal;
    isAsleep = isasleep;
  }

  public override void Greeting(){

  }
}

class Cat : Mamals{
    public override void Greeting()
    {
        Console.WriteLine($"Hello! I am a Cat named {Name}, I am {Age} years old. I love to get pet!");
    }
    public Cat(string name, int age, bool isasleep) : base(name, age, isasleep) {
      isAsleep = true;
  }
}