using Entities.PostModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteServices.Interface;
using System.Threading.Tasks;
using WebCore.ResponseDate;

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        IQuoteService _allQuoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _allQuoteService = quoteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseData> GetAllQuotes()
        {
            return await _allQuoteService.GetAllQuotesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseData> CreateQuote([FromBody] QuotePostModel model)
        {
            return await _allQuoteService.AddQuoteAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("getbycategory")]
        public async Task<ResponseData> GetQuoteByCategory([FromQuery] string category)
        {
            return await _allQuoteService.GetByCategoryAsync(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("randomQuote")]
        public async Task<ResponseData> RandomQuote()
        {
            return await _allQuoteService.RandomQuoteAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseData> EditQuote(QuotePutModel model)
        {
            return await _allQuoteService.UpdateQuoteAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResponseData> DeleteQuote(int id)
        {
            return await _allQuoteService.DeleteQuoteAsync(id);
        }
    }
}
