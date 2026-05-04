using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Store.Api.DTO;

public record UpdateGameDTO(     
    [Required][StringLength(30)] string name, 
    [Range(1,50)] int typeId, 
    [Range(0,1000)] decimal price,
    DateOnly releaseDate
);
