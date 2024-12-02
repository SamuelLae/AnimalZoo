using System;
using System.Collections.Generic;

public class Troubleshoot : Exception{
  string message = "Faulty input";
    public override string ToString()
    {
        return message;
    }
}

class Zoo {
    public List<Animal> Animals = new List<Animal>();
    private string filePath = "animals.txt"; 

    public Zoo() {
        LoadFromFile(); 
    }

    public void GreetAnimals() {
        Console.WriteLine("");
        foreach (Animal a in Animals) {
            a.Greeting();
        }
    }

    public void Add(Animal x) {
        Animals.Add(x);
        
    }

    public void Remove(int y) {
        Animals.RemoveAt(y);
        
    }

    public Animal First() {
      Animal firstAnimal = Animals[0];
      Console.WriteLine($"The first animal is a {firstAnimal.GetType().Name} named {firstAnimal.Name}, aged {firstAnimal.Age}.");
        return firstAnimal;
    }

    public void ExitZoo() {
        Console.WriteLine("Goodbye");
        SaveToFile(); 
    }

    
    private void SaveToFile() {
        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (var animal in Animals) {
                string animalData = animal.GetType().Name + "," + animal.Name + "," + animal.Age;
                if (animal is Birds bird) {
                    animalData += "," + bird.canFly;
                } else if (animal is Bugs bug) {
                    animalData += "," + bug.hasWings;
                } else if (animal is Mamals mammal) {
                    animalData += "," + mammal.isNocturnal + "," + mammal.isAsleep;
                }
                writer.WriteLine(animalData);
            }
        }
        Console.WriteLine("Animals automatically saved.");
    }

    private void LoadFromFile() {
        if (!File.Exists(filePath)) {
            Console.WriteLine("No saved file found. Starting fresh.");
            return;
        }

        using (StreamReader reader = new StreamReader(filePath)) {
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] data = line.Split(',');
                string type = data[0];
                string name = data[1];
                int age = int.Parse(data[2]);

                switch (type) {
                    case "Parrot":
                        Add(new Parrot(name, age, bool.Parse(data[3])));
                        break;
                    case "Penguin":
                        Add(new Penguin(name, age, bool.Parse(data[3])));
                        break;
                    case "Ant":
                        Add(new Ant(name, age, bool.Parse(data[3])));
                        break;
                    case "Cat":
                        Add(new Cat(name, age, bool.Parse(data[4])));
                        break;
                    default:
                        Console.WriteLine($"Unknown animal type: {type}");
                        break;
                }
            }
        }
        Console.WriteLine("Animals loaded from file.");
    }
}


abstract class Animal{
  public string Name { get; set; }
  public int Age { get; set; }

  public Animal(string name, int age){
    Name = name;
    Age = age;
  }

  public abstract void Greeting();
}

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

class Bugs : Animal{
  public bool hasWings;
  
  public Bugs(string name, int age, bool haswings) : base(name, age){
    hasWings = haswings;
  }

  public override void Greeting(){

  }
}

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

class Ant : Bugs{

    public override void Greeting()
    {
        Console.WriteLine($"Hello! I am a Ant named {Name}, I am {Age} years old. I am tiny!");
    }
    public Ant(string name, int age, bool haswings) : base(name, age, haswings) {

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

class Program{
  static void Main(){
    Zoo operations = new Zoo();

    bool zooLoop = true;
    while (zooLoop){
      Console.Clear();
      Console.WriteLine("Welcome to Tannbergsskolans Zoo!");
      Console.WriteLine("1) Add Animal");
      Console.WriteLine("2) Remove Animal");
      Console.WriteLine("3) Show First Animal");
      Console.WriteLine("4) Greet All Animals");
      Console.WriteLine("5) Exit");
      Console.Write("Choose an option: ");

      switch (Console.ReadKey(true).KeyChar){
        case '1': 
        Console.WriteLine("Would You Like To Add An Animal (y/n)");
        if (Console.ReadKey().KeyChar.ToString().ToLower() == "y"){
          Console.WriteLine("\nWhich animal would you like to add?");
          Console.WriteLine("1) Parrot\n2) Penguin\n3) Ant\n4) Cat");
          char animalType = Console.ReadKey(true).KeyChar;
          if (animalType > '4'){
            throw new Troubleshoot();
            
          }

          Console.Write("\nEnter the animal's name: ");
          string name = Console.ReadLine();
          if (name == null || name.All(char.IsDigit)){
            Console.WriteLine("Invalid name. Returning to main menu");
            throw new Troubleshoot();
          }

          Console.Write("Enter the animal's age: ");
          int age;
          if (!int.TryParse(Console.ReadLine(), out age)){
            Console.WriteLine("Invalid age. Returning to main menu.");
            throw new Troubleshoot();
          }

          switch (animalType){
            case '1':
              operations.Add(new Parrot(name, age, true));
              break;
            case '2':
              operations.Add(new Penguin(name, age, false));
              break;
            case '3':
              operations.Add(new Ant(name, age, false));
              break;
            case '4':
              operations.Add(new Cat(name, age, false));
              break;
            default:
              Console.WriteLine("Invalid choice.");
              break;
          }
          break;
       } else {
          break;
    }

          

        case '2':
          Console.WriteLine("\nEnter the index of the animal to remove: ");
          int removeIndex = int.Parse(Console.ReadLine());
          if (removeIndex < operations.Animals.Count() && removeIndex >= 0){
            operations.Remove(removeIndex);
            Console.WriteLine("Animal removed.");
          } else {
            Console.WriteLine("Animal not found.");
          }
          break;

        case '3':
          if (operations.Animals.Count > 0){
            operations.First();
          } else {
            Console.WriteLine("There are no animals in the zoo.");
          }
          
          break;

        case '4':
           if (operations.Animals.Count > 0){
            Console.WriteLine(operations.Animals.Count());
            operations.GreetAnimals();
          } else {
            Console.WriteLine("There are no animals to greet in the zoo.");
          }
          break;

        case '5':
          operations.ExitZoo();
          zooLoop = false;
          break;

        default:
          Console.WriteLine("Invalid option. Try again.");
          break;
      }

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}