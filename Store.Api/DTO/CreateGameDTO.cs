using System.ComponentModel.DataAnnotations;

namespace Store.Api.DTO;

public record CreateGameDTO( 
    [Required][StringLength(30)] string name, 
    [Range(1,50)] int typeId, 
    [Range(0,1000)] decimal price,
    DateOnly releaseDate
    );
