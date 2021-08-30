using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Entitys;
using QuotesAPI.Models;
using QuotesAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        IQuoteService _allQuoteService;

        private IDataStorage _dataStorage;

        public QuoteController(IDataStorage dataStorage, IQuoteService quoteService)
        {
            _allQuoteService = quoteService;
            _dataStorage = dataStorage;
        }

        [HttpGet]
        public async Task<ResponseData> GetAllQuotes()
        {
            return _dataStorage.Get();
        }

        [HttpPost]
        public async Task<ResponseData> CreateQuote([FromBody] QuotePostModel model)
        {
            var quoteModel = await _allQuoteService.AddQuoteService(model);
            _dataStorage.Create(model);
            return "Quote Created.";
        }

        [HttpGet("getbycategory")]
        public async Task<ResponseData> GetQuoteByCategory([FromQuery] string category)
        {
            var quotes = await _dataStorage.GetByCategory(category);
            if (quotes == null)
                return NotFound();
            return Ok(quotes);
        }

        [HttpGet("randomQuote")]
        public ActionResult RandomQuote()
        {
            return Ok(_dataStorage.RandomQuote);
        }

        [HttpPut]
        public async Task<ResponseData> EditQuote(int id, QuoteModel quote)
        {
            if (await _dataStorage.Edit(id, quote))
                return Ok("Quote edit.");
            return Ok("Quote with this Id Does not excist.");
        }

        [HttpDelete]
        public async Task<ResponseData> DeleteQuote(int id)
        {
            if (await _dataStorage.Delete(id))
                return Ok("Quote deleted.");
            return Ok("Quote with this Id Does not excist.");
        }
    }
}
