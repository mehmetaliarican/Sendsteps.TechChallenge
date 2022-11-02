using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstract.Business
{
    public interface IAsyncMatcherService<TInput, TOutput>
    {
        Task<TOutput> MatchAsync(TInput input);
    }
}
