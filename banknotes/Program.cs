using System;
using System.Collections.Generic;

namespace banknotes
{
    internal class Program
    {
        static long inputExchangeable()
        {
            long value;

            try
            {
                value = Convert.ToInt64(Console.ReadLine());
                if (value <= 0)
                {
                    Console.WriteLine("Введенное значение <= 0. Введите другое число!");
                    value = inputExchangeable();
                }
            }
            catch
            {
                Console.WriteLine("Введенное значение недопустимо!");
                return inputExchangeable();
            }

            return value;
        }

        static long[] inputChanger(long value)
        {
            HashSet<string> bankSet = new HashSet<string>();
            string nextVal = "";
            while (nextVal != "0")
            {
                nextVal = Console.ReadLine();
                if (nextVal != "0") bankSet.Add(nextVal);
            }

            long match;
            List<long> numbers = new List<long>();
            foreach (string banknote in bankSet)
            {
                try
                {
                    match = Convert.ToInt64(banknote);
                    if (match <= 0 || match > value)
                    {
                        Console.WriteLine($"Значение {match} недопустимо, так как либо <= 0, либо >{value}");
                    }
                    else
                    {
                        numbers.Add(match);
                    }
                }
                catch
                {
                    Console.WriteLine($"Значение {banknote} недопустимо, оно будет пропущенно!");
                }
            }

            if (numbers.Count == 0)
            {
                Console.WriteLine("Повторите попытку ввода номиналов, так как ни один не оказался валидным:");
                return inputChanger(value);
            }

            numbers.Sort((a, b) => b.CompareTo(a));
            return numbers.ToArray();
        }

        static void exchange(long value, long[] bankM, int i, long[] denomination)
        {
            if (value == 0)
            {
                for (int j = 0; j < denomination.Length; ++j)
                {
                    if (denomination[j] != 0)
                    {
                        for (int k = 0; k < denomination[j]; ++k) Console.Write(bankM[j] + " ");
                    }
                }

                Console.WriteLine();
                return;
            }

            for (int j = i; j < bankM.Length; ++j)
            {
                if (value / bankM[j] > 0)
                    for (long k = value / bankM[j]; k > 0; --k)
                    {
                        value -= bankM[j] * k;
                        denomination[j] += k;
                        exchange(value, bankM, j + 1, denomination);
                        denomination[j] -= k;
                        value += bankM[j] * k;
                    }
            }
        }

        public static void Main(string[] args)
        {
            long res;
            Console.WriteLine("Введите сумму для размена: ");
            long value = inputExchangeable();
            Console.WriteLine("Введите номиналы для размена: ");
            long[] banknotes = inputChanger(value);
            long[] denomination = new long[banknotes.Length];
            Console.WriteLine("Комбинации: ");
            exchange(value, banknotes, 0, denomination);
        }
    }
}