using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstract.Business
{
    public interface IMatcherService<TInput, TOutput>
    {
        TOutput Match(TInput input);
    }
}
