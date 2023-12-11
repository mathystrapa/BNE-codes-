using System;
using System.Data;

namespace problem7
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Investment investment = new Investment();
                Console.WriteLine("\nResultados finais do investimento:\n");
                Console.WriteLine("| Data inicial |  Data final  | Capital inicial | Montante final | Lucro líquido | Lucro percentual | Total sacado |");
                Console.WriteLine($"|   {investment.StartingDate.ToShortDateString()}   |  {investment.EndingDate.ToShortDateString()}  |  R$ {investment.StartingCapital.ToString("N2")}  |  R$ {investment.EndingCapital.ToString("N2")}  |   R$ {investment.LiquidProfit.ToString("N2")}   |    {Math.Round(investment.PercentageProfit, 3)} %    |  R$ {investment.TotalRescue.ToString("N2")}  |");

                AskUser();
            }
        }

        static void AskUser()
        {
            string? user_input;
            while (true)
            {
                Console.WriteLine("\nDeseja fazer outra simulação de investimento? ('s' para sim e 'n' para não)");
                user_input = Console.ReadLine();
                if (string.IsNullOrEmpty(user_input) || (user_input.ToLower() != "s" && user_input.ToLower() != "n"))
                {
                    Console.WriteLine("\nErro. Digite uma resposta válida.\n");
                }
                else
                {
                    if (user_input.ToLower() == "n")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    class Investment
    {
        public double StartingCapital, EndingCapital, LiquidProfit, PercentageProfit, MonthlyInterestRate, DailyInterestRate, TotalRescue = 0;
        public DateTime StartingDate, EndingDate;

        public Investment()
        {
            GetInfo();
            DailyInterestRate = ConvertFromMonthlyToDaily(MonthlyInterestRate);
            CalcMonthResult();
        }

        private double ConvertFromMonthlyToDaily(double monthlyInterestRate)
        {
            double dailyInterestRate = Math.Pow(monthlyInterestRate + 1, 1.0 / 30.0) - 1;
            return dailyInterestRate;
        }

        private void CalcMonthResult()
        {
            double balance = StartingCapital, liquid_profit = 0, rescue = 0, previousBalance, daysForNextMonth;

            for (DateTime date = StartingDate; date <= EndingDate; date = date.AddMonths(1))
            {
                if (date != StartingDate.AddMonths(1))
                {
                    Console.WriteLine("\n|    Data    |  Rendimento  |  Saque  |   Saldo   |");
                }

                Console.WriteLine($"|  {date.ToShortDateString()}  |  R$ {liquid_profit.ToString("N2")}  |  R$ {rescue.ToString("N2")}  | R$ {balance.ToString("N2")} |");
                if (date != EndingDate)
                {
                    if (date != StartingDate)
                    {
                        rescue = AskRescue();
                        TotalRescue += rescue;
                    }

                    previousBalance = balance;
                    balance -= rescue;
                    daysForNextMonth = Convert.ToDouble((date.AddMonths(1) - date).Days);
                    liquid_profit = balance * (Math.Pow(1 + DailyInterestRate, daysForNextMonth) - 1);
                    balance += liquid_profit;
                }
            }
            EndingCapital = balance;
            LiquidProfit = EndingCapital - StartingCapital;
            PercentageProfit = LiquidProfit * 100 / StartingCapital;
        }

        private static double AskRescue()
        {
            string? user_input;
            double rescue;

            while (true)
            {
                Console.WriteLine("\nDeseja sacar ? Digite o valor do saque (R$) ou aperte 'enter' para continuar.");
                user_input = Console.ReadLine();
                if (string.IsNullOrEmpty(user_input))
                {
                    return 0.0;
                }
                else
                {
                    if (double.TryParse(user_input, out rescue) == false)
                    {
                        ErrorMessage(0);
                    }
                    else
                    {
                        if (rescue < 0)
                        {
                            ErrorMessage(-1);
                        }
                        else
                        {
                            return rescue;
                        }
                    }
                }
            }
        }

        private void GetInfo()
        {
            Console.WriteLine("\nInformações do investimento:\n");
            string? user_input;

            while (true)
            {
                Console.WriteLine("\nInsira o capital inicial: R$");
                user_input = Console.ReadLine();
                if (double.TryParse(user_input, out StartingCapital) == false)
                {
                    ErrorMessage(0);
                }
                else
                {
                    if (StartingCapital < 0)
                    {
                        ErrorMessage(-1);
                    }
                    else
                    {
                        Console.WriteLine("\nInsira a taxa de juros mensal em pontos percentuais (%):");
                        user_input = Console.ReadLine();
                        if (double.TryParse(user_input, out MonthlyInterestRate) == false)
                        {
                            ErrorMessage(0);
                        }
                        else
                        {
                            if (MonthlyInterestRate < 0) 
                            {
                                ErrorMessage(-1);
                            }
                            else
                            {
                                MonthlyInterestRate /= 100;
                                Console.WriteLine("\nInsira a data inicial do investimento (MM-DD-AAAA):");
                                user_input = Console.ReadLine();
                                if (DateTime.TryParse(user_input, out StartingDate) == false)
                                {
                                    ErrorMessage(1);
                                }
                                else
                                {
                                    Console.WriteLine("\nInsira a data final do investimento (MM-DD-AAAA):");
                                    user_input = Console.ReadLine();
                                    if (DateTime.TryParse(user_input, out EndingDate) == false)
                                    {
                                        ErrorMessage(1);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void ErrorMessage(int codError)
        {
            if (codError == -1)
            {
                Console.WriteLine("\nErro. O valor deve ser maior que 0.\n");
            }
            else
            {
                if (codError == 0)
                {
                    Console.WriteLine("\nErro. Valor inválido.\n");
                }
                else
                {
                    if (codError == 1)
                    {
                        Console.WriteLine("\nErro. Data inválida.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nErro. Digite uma resposta válida.\n");
                    }
                }
            }
        }
    }
}