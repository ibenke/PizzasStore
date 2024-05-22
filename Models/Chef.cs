namespace PizzaStore.Models 
{
    public class Chef
    {
          public int Id {get; set;}
          public string Name {get; set;} = string.Empty;
          public ICollection<Pizza> Pizzas {get; set;} = new List<Pizza>();
    }
}