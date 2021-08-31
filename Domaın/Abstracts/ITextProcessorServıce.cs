using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaın.Abstracts
{
   public interface ITextProcessorServıce
    {
        IEnumerable<string> FindPalindromes(string inputWord);
    }
}
