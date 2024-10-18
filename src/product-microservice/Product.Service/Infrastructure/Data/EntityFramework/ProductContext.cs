﻿using Microsoft.EntityFrameworkCore;
using Product.Service.Models;

namespace Product.Service.Infrastructure.Data.EntityFramework;

internal class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options), IProductStore
{
    public DbSet<Models.Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
    }

    public async Task<Models.Product?> GetById(int id)
    {
        return await FindAsync<Models.Product>(id);
    }
    public async Task CreateProduct(Models.Product product)
    {
        Products.Add(product);
        await SaveChangesAsync();
    }
    public async Task UpdateProduct(Models.Product product)
    {
        var existingProduct = await FindAsync<Models.Product>(product.Id);
        if (existingProduct is not null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            await SaveChangesAsync();
        }
    }
}
