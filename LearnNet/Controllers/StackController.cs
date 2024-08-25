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
using api.Interfaces;

namespace api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks =  await _stockRepository.GetAllStocksAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(new { message = "Stocks Found", data = stockDto });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetStockAsync(id);
            if (stock == null)
            {
                return NotFound(new { message = "Stock not found" });
            }
            return Ok(new { message = "Stock Found", data = stock.ToStockDto() });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = await _stockRepository.CreateStockAsync(stockDto);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());


        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepository.UpdateStockAsync(id, updateDto);

            if (stockModel == null)
            {
                return NotFound(new { message = "Stock not found" });
            }
            
            return Ok(new { message = "Stock Updated", data = stockModel.ToStockDto() });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockModel = await _stockRepository.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound(new { message = "Stock not found" });
            }
            return Ok(new { message = "Stock Deleted" , data = stockModel.ToStockDto() });
        }

    }
}