namespace Store.Api.DTO;

public record class GameSummaryDTO(int id, string name, string type, decimal price, DateOnly releaseDate)
{
}
