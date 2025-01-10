namespace AnimalZoo;

using System;
using System.Collections.Generic;

public class Troubleshoot : Exception{
  string message = "Faulty input";
    public override string ToString()
    {
        return message;
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