namespace Store.Api.DTO;

public record class GameDetailsDTO(int id, string name, int typeId, decimal price, DateOnly releaseDate)
{
}
