using Domaın.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domaın.Services
{
    public class TextProcessorServıce : ITextProcessorServıce
    {
        public IEnumerable<string> FindPalindromes(string inputWord)
        {
            if (string.IsNullOrWhiteSpace(inputWord))
                throw new Exception("input can not be null");

            var result = new List<string>();
            var inputChars = inputWord.ToLower().ToCharArray();

            var taskList = new List<Task<string>>();

            for (int i = 1; i < inputChars.Length; i++)
                taskList.Add(GetPalindromesAsync(inputChars, i));
            var resultArray = Task.WhenAll(taskList).GetAwaiter().GetResult();
            return resultArray.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
        }
        private async Task<string> GetPalindromesAsync(char[] inputCharArray, int charCount)
        {
            if (charCount == 1)
                return string.Join(",", inputCharArray);

            string result = "";
            int skipCount = 1;
            if (charCount % 2 == 0)
                skipCount = 0;
            var half = charCount / 2;

            var lastInsertedCharIndex = 0;
            for (int i = 0; i <= inputCharArray.Length - charCount; i++)
            {
                var currentArray = inputCharArray.Skip(i).Take(charCount);
                var subArray1 = currentArray.Take(half).ToArray();
                var subArray2 = currentArray.Skip(half + skipCount).Take(half).Reverse().ToArray();
                if (Enumerable.SequenceEqual(subArray1, subArray2))
                {
                    result += string.Join("", currentArray)+",";
                    i = i + charCount - 1;
                }
                else
                {
                    result += inputCharArray[i] + ",";
                }

                lastInsertedCharIndex = i;
            }

            result += string.Join(",", inputCharArray.Skip(lastInsertedCharIndex + 1));
            return result;
        }
    }
}
