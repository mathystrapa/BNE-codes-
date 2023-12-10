// PROBLEMA 5:	Quais seriam os cálculos para Problema 2, para obter o Valor Futuro R$ 7.390,61
// Taxa mensal: 1,25 %
// Período: 6 meses
// VF = VP * (i + 1)^t => VP = VF/((1 + i)^t)

using System;
using TableInvestment;

namespace Problem5
{
    class Program
    {
        static void Main()
        {
            double starting_capital, interest_rate = 0.0125, ending_capital = 7390.61;
            int time = 6;

            starting_capital = ending_capital / Math.Pow(1 + interest_rate, time);
            Console.WriteLine($"\nO capital inicial necessário para que um investimento gere, a uma taxa de 1.25% ao mês durante 6 meses, um montante de  R$ 7390.61 é igual a R$ {starting_capital.ToString("N2")}. Veja o resultado do investimento:\n");

            Investment investment = new Investment(starting_capital, interest_rate, time);
            investment.ShowTableResults();
            Console.WriteLine($"\nResultados do investimento: \nMontante final: R$ {investment.Amount.ToString("N2")}\nLucro líquido:{investment.LiquidProfit.ToString("N2")}\nLucro percentual: {investment.PercentageProfit.ToString("N2")} %");
            Console.ReadKey();
        }
    }
}