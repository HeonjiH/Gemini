using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini
{
    public interface IGeminiService
    {
        public Task Build(RequestTypes types);
    }
}
