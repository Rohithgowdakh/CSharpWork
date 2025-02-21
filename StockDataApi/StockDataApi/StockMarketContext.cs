using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StockDataApi;

public class StockMarketContext : DbContext
{
    public StockMarketContext(DbContextOptions<StockMarketContext> options)
        : base(options)
    {
    }

    public DbSet<Fund> Funds { get; set; }
}
