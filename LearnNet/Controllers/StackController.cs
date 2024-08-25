using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.models;
using api.data;
using api.Mappers;

namespace api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackController : ControllerBase 
    {
        private readonly AplicationDBContext _context;
        public StackController(AplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var stocks = _context.Stocks.ToList()
            .Select(s =>s.ToStockDto());
            return Ok(new { message = "Stocks Found", data = stocks });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if(stock == null){
                return NotFound(new { message = "Stock not found" });
            }
            return Ok(new { message = "Stock Found", data = stock.ToStockDto() });
        }
    }
}