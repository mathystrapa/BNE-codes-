// PROBLEMA 2: 	Crie uma Tabela para 6 meses, taxa a.m. Rendimento para cada mês e mostrar todos os meses					
// Valor presente: R$ 3800 
// Taxa mensal: 1,25 %
// Período: 6 meses

using System;

namespace TableInvestment
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Simulação de investimento de R$ 3800 a uma taxa mensal de 1,25 % durante 6 meses: \n");

            double capital = 3800;
            double rate = 0.0125;
            int time = 6;

            Investment investment = new Investment(capital, rate, time);

            investment.ShowTableResults();
            Console.WriteLine($"\nResultados do investimento: \nMontante final: R$ {Math.Round(investment.Amount, 2)}\nLucro líquido: {Math.Round(investment.LiquidProfit, 2)}\nLucro percentual: {Math.Round(investment.PercentageProfit, 2)}.");
            Console.ReadKey();
        }
    }

    class Investment
    {
        public double StartingCapital, InterestRate, Amount, LiquidProfit, PercentageProfit;
        public int Time;

        public Investment(double starting_capital, double interest_rate, int time)
        {
            StartingCapital = starting_capital;
            InterestRate = interest_rate;
            Time = time;
            Amount = CalcAmount(StartingCapital, InterestRate, Time);
            double[] profits = CalcProfits(StartingCapital, Amount);
            LiquidProfit = profits[0];
            PercentageProfit = profits[1];
        }

        private double CalcAmount(double starting_capital, double interest_rate, int time)
        {
            return starting_capital * (Math.Pow(interest_rate + 1, time));
        }

        private double[] CalcProfits(double starting_capital, double amount)
        {
            double[] Profits = { amount - starting_capital, (amount - starting_capital) * 100 / starting_capital };
            return Profits;
        }

        public void ShowTableResults()
        {
            double[] amounts = new double[Time + 1], liquid_profits = new double[Time + 1];
            int cont = 0;

            amounts[0] = StartingCapital;
            liquid_profits[0] = 0;

            Console.WriteLine("|  Meses  |  Montante  |  Lucro  |");
            while (cont <= Time)
            {
                Console.WriteLine($"  {cont}  |  {Math.Round(amounts[cont], 2)}  |  {Math.Round(liquid_profits[cont], 2)}  |");

                if (cont != Time)
                {
                    liquid_profits[cont + 1] = amounts[cont] * InterestRate;
                    amounts[cont + 1] = amounts[cont] + liquid_profits[cont + 1];
                }

                cont += 1;
            }
        }
    }
}