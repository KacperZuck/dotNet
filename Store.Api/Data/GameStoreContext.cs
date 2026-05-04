using System;
using Microsoft.EntityFrameworkCore;
using Store.Api.Models;

namespace Store.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Models.Type> Types => Set<Models.Type>();

}
