using QuotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Services.Interface
{
    public interface IQuoteService
    {
        Task<QuoteModel> AddQuoteService(QuotePostModel model);
    }
}
