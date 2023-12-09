// PROBLEMA 4: 	Elabore um tabela de iteração, caso ocorra um Resgate(saque) do Rendimento no 5º mês, o Saldo?.

// Valor presente: R$ 20.000
// Taxa mensal: 2 %
// Período: 6 meses
// Saque no 5º mês

using System;
using System.Runtime.CompilerServices;

namespace InvestmentwithRescue
{
    class Program
    {
        static void Main()
        {
            double starting_capital = 20000, interest_rate = 0.02;
            int time = 6;
            Investment myInvestment = new Investment(starting_capital, interest_rate, time);
            Console.WriteLine($"\nResultados do investimento:\nMontante final: R$ {Math.Round(myInvestment.EndingCapital, 2)}\nLucro líquido: R${Math.Round(myInvestment.LiquidProfit, 2)}\nLucro percentual: {Math.Round(myInvestment.PercentageProfit, 2)} %");
            Console.ReadKey();
        }
    }

    class Investment
    {
        public double StartingCapital, EndingCapital, Time, InterestRate, LiquidProfit, PercentageProfit;

        public Investment(double starting_capital, double interest_rate, int time)
        {
            StartingCapital = starting_capital;
            Time = time;
            InterestRate = interest_rate;
            NewMonth();
        }

        private void NewMonth()
        {
            double balance = StartingCapital, liquid_profit = 0, rescue = 0, previous_balance;
            int cont = 0;

            while (cont <= Time)
            {
                if (cont != 1)
                {
                    Console.WriteLine("\n|  Mês  |  Rendimento  |  Saque  |  Saldo  |");
                }
                
                Console.WriteLine($"|   {cont}   |    {liquid_profit.ToString("N2")}    |   {rescue.ToString("N2")}   |  {balance.ToString("N2")}  |");

                if (cont != Time)
                {
                    if (cont != 0)
                    {
                        rescue = AskUser();
                    }
                    previous_balance = balance;
                    balance -= rescue;
                    liquid_profit = balance * InterestRate;
                    balance = previous_balance - rescue + liquid_profit;
                }

                cont += 1;
            }

            EndingCapital = balance;
            LiquidProfit = EndingCapital - StartingCapital;
            PercentageProfit = LiquidProfit * 100 / StartingCapital;
        }

        private double AskUser()
        {
            string? user_input;
            double rescue;

            while (true)
            {
                Console.WriteLine("\nDeseja sacar ? ('s' para sim e 'n' para não) ");
                user_input = Console.ReadLine();
                if (string.IsNullOrEmpty(user_input))
                {
                    Console.WriteLine("\nErro. Digite uma resposta válida.\n");
                }
                else
                {
                    if (user_input.ToLower() != "s" && user_input.ToLower() != "n")
                    {
                        Console.WriteLine("\nErro. Digite uma resposta válida.\n");
                    }
                    else
                    {
                        if (user_input == "n")
                        {
                            rescue = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Quanto deseja sacar?");
                            user_input = Console.ReadLine();
                            if (double.TryParse(user_input, out rescue) == false)
                            {
                                Console.WriteLine("\nErro. Digite uma resposta válida.\n");
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return rescue;
        }
    }
}