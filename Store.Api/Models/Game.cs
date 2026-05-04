using System;

namespace Store.Api.Models;

public class Game
{
    public int id { get; set; }
    public required string name {get; set;}
    public Type? type { get; set; }
    public int typeId { get; set; }

    public decimal price { get; set;}

    public DateOnly releaseDate { get; set;}


}
