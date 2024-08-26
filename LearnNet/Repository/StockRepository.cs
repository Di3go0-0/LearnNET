using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AplicationDBContext _context;
        public StockRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            var stocks = await _context.Stocks.Include(c => c.Comments).ToListAsync();
            return stocks;
        }

        public async Task<Stock?> GetStockAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock> CreateStockAsync(CreateStockRequestDto stock)
        {
            var stockModel = stock.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stock)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = stock.Symbol;
            stockModel.CompanyName = stock.CompanyName;
            stockModel.Purchase = stock.Purchase;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.Industry = stock.Industry;
            stockModel.MarketCap = stock.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
         
    }
}