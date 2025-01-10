namespace AnimalZoo;

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