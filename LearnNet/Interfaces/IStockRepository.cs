using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockAsync(int id);
        Task<Stock> CreateStockAsync(CreateStockRequestDto stock);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stock);
        Task<Stock?> DeleteStockAsync(int id);
    }
}