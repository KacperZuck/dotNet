using System;
using Microsoft.EntityFrameworkCore;
using Store.Api.Data;
using Store.Api.DTO;

namespace Store.Api.EndPoints;

public static class TypeEndPoints
{

    public static void MapTypeEndPoints(this WebApplication app)
    {
        var groupType = app.MapGroup("/types");

        groupType.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Types.Select(type =>
                new TypeDTO(type.id, type.name)).AsNoTracking().ToListAsync());
    }
}
