using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.models;
using api.data;
using api.Mappers;
using api.DTOs.Stock;

namespace api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AplicationDBContext _context;
        public StockController(AplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(new { message = "Stocks Found", data = stocks });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return NotFound(new { message = "Stock not found" });
            }
            return Ok(new { message = "Stock Found", data = stock.ToStockDto() });
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());


        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null)
            {
                return NotFound(new { message = "Stock not found" });
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();
            return Ok(new { message = "Stock Updated", data = stockModel.ToStockDto() });
        }

    }
}