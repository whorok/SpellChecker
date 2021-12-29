using System;

namespace SpellChecker
{
    public class CheckAlgorithms
    {
        enum Operation
        {
            None,
            Deletion,
            Insertion,
            Any
        }

        public int GetDefaultDistance(string first, string second)
        {
            var opt = new int[first.Length + 1, second.Length + 1];
            var operations = new Operation[first.Length + 1, second.Length + 1];
            
            for (var i = 0; i <= first.Length; i++)
                opt[i, 0] = i;
            for (var i = 0; i <= second.Length; i++)
                opt[0, i] = i;
            
            for (var i = 1; i <= first.Length; i++)
            for (var j = 1; j <= second.Length; j++)
            {
                if (first[i - 1] == second[j - 1])
                {
                    opt[i, j] = opt[i - 1, j - 1];
                    operations[i, j] = Operation.None;
                }
                else if (operations[i - 1, j] == Operation.Insertion)
                {
                    opt[i, j] = opt[i, j - 1] + 1;
                    operations[i, j] = Operation.Deletion;
                }
                else if (operations[i, j - 1] == Operation.Deletion)
                {
                    opt[i, j] = opt[i - 1, j] + 1;
                    operations[i, j] = Operation.Insertion;
                }
                else if (opt[i - 1, j] == opt[i, j - 1])
                {
                    opt[i, j] = opt[i, j - 1] + 1;
                    operations[i, j] = Operation.Any;
                }
                else
                {
                    opt[i, j] = Math.Min(opt[i, j - 1] + 1, opt[i - 1, j] + 1);
                    operations[i, j] = opt[i, j] == opt[i, j - 1] + 1 ? Operation.Deletion : Operation.Insertion;
                }
            }

            return opt[opt.GetLength(0) - 1, opt.GetLength(1) - 1];
        }
    }
}