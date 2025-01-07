using System;
using System.Xml.Serialization;
using System.IO;

public abstract class Animal
{
    public string Name { get; set; }
    public string Country { get; set; }
    public bool HideFromOtherAnimals { get; set; }

    public abstract void SayHello();
    public abstract eFavoriteFood GetFavouriteFood();
    public abstract eClassificationAnimal GetClassificationAnimal();
}

public class Cow : Animal
{
    public override void SayHello() => Console.WriteLine("Муу!");
    public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Plants;
    public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Herbivores;
}

public class Lion : Animal
{
    public override void SayHello() => Console.WriteLine("Рррр!");
    public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Meat;
    public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Carnivores;
}

public class Pig : Animal
{
    public override void SayHello() => Console.WriteLine("Хрю!");
    public override eFavoriteFood GetFavouriteFood() => eFavoriteFood.Everything;
    public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Omnivores;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CommentAttribute : Attribute
{
    public string Description { get; }

    public CommentAttribute(string description)
    {
        Description = description;
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CommentAttribute1 : Attribute
    {
        public string Description { get; }

        public CommentAttribute1(string description)
        {
            Description = description;
        }
    }
}

public enum eClassificationAnimal
{
    Herbivores,
    Carnivores,
    Omnivores
}

public enum eFavoriteFood
{
    Meat,
    Plants,
    Everything
}

class main
{
    static void Main()
    {
        Animal pig = new Pig { Name = "Свин", Country = "Россия", HideFromOtherAnimals = false };

        XmlSerializer serializer = new XmlSerializer(typeof(Pig));
        using (FileStream fs = new FileStream("Animals.xml", FileMode.Create))
        {
            serializer.Serialize(fs, pig);
        }

        Console.WriteLine("XML файл с животным создан.");

        using (FileStream fs = new FileStream("Animals.xml", FileMode.Open))
        {
            Animal deserializedAnimal = (Animal)serializer.Deserialize(fs);
            Console.WriteLine($"Имя: {deserializedAnimal.Name}, Страна: {deserializedAnimal.Country}, Любимая еда: {deserializedAnimal.GetFavouriteFood()}, Классификация: {deserializedAnimal.GetClassificationAnimal()}");
        }
    }
}