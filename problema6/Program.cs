// Crie uma Tabela e Programa C# que leia valores da Entrada. Mostre os cálculos para um período de 8 meses e 10 dias e que mostre o Rendimento Futuro:
// Entradas: 1000, 5500, 12000	
// Taxas: 3 %, 2,48 % e 2 %
// Período: 8 meses e 10 dias

using System;

namespace InvestmentWithDateTime
{
    class Program
    {
        static void Main()
        {
            double[] info = GetStartingCapitalAndInterestRate();
            double startingCapital = info[0], monthlyInterestRate = info[1]/100;
            DateTime todayDate = DateTime.Now;
            DateTime eightMonthsAndTenDaysFromNow = todayDate.AddMonths(8).AddDays(10);
            double daysOfDifference = Convert.ToDouble((eightMonthsAndTenDaysFromNow - todayDate).Days);
            double dailyInterestRate = Investment.ConvertFromMonthlyToDaily(monthlyInterestRate);
            Investment investment = new Investment(startingCapital, dailyInterestRate, daysOfDifference);

            Console.WriteLine($"\nTaxa diária: {dailyInterestRate*100} %\n");

            Console.WriteLine($"\nHoje é dia {todayDate.Day} do mês {todayDate.Month} do ano {todayDate.Year}.");
            Console.WriteLine($"\nResultados do investimento de R$ {startingCapital} a uma taxa mensal de {monthlyInterestRate*100} %, começando agora, até o dia {eightMonthsAndTenDaysFromNow.Day} do mês {eightMonthsAndTenDaysFromNow.Month} do ano {eightMonthsAndTenDaysFromNow.Year} (daqui a 8 meses e 10 dias):\n\nMontante final: R$ {investment.EndingCapital.ToString("N2")}\nLucro líquido: R$ {investment.LiquidProfit.ToString("N2")}\nLucro percentual: {investment.PercentageProfit.ToString("N2")} %");

            Console.ReadKey();
        }

        static double[] GetStartingCapitalAndInterestRate()
        {
            double[] starting_capital_and_interest_rate = new double[2];
            string? user_input;

            while (true)
            {
                Console.WriteLine("Insira o capital inicial: R$");
                user_input = Console.ReadLine();
                if (double.TryParse(user_input, out starting_capital_and_interest_rate[0]) == false){
                    ErrorMessage();
                }
                else
                {
                    if (starting_capital_and_interest_rate[0] < 0)
                    {
                        ErrorMessage();
                    }
                    else
                    {
                        Console.WriteLine("\nInsira a taxa mensal (em %): ");
                        user_input = Console.ReadLine();
                        if (double.TryParse(user_input, out starting_capital_and_interest_rate[1]) == false)
                        {
                            ErrorMessage();
                        }
                        else
                        {
                            if (starting_capital_and_interest_rate[1] < 0)
                            {
                                ErrorMessage();
                            }
                            else
                            {
                                return starting_capital_and_interest_rate;
                            }
                        }
                    }
                }
            }
        }

        static void ErrorMessage()
        {
            Console.WriteLine("\nErro. Digite um valor válido.\n");
        }
    }

    class Investment
    {
        public double StartingCapital, EndingCapital, LiquidProfit, PercentageProfit;

        public Investment(double startingCapital, double interestRate, double period)
        {
            StartingCapital = startingCapital;
            CalcInvestmentResults(interestRate, period);
        }

        private void CalcInvestmentResults(double interestRate, double period)
        {
            EndingCapital = StartingCapital * Math.Pow(1 + interestRate, period);
            LiquidProfit = EndingCapital - StartingCapital;
            PercentageProfit = LiquidProfit * 100 / StartingCapital;
        }

        public static double ConvertFromMonthlyToDaily(double monthly_interest_rate)
        {
            double dailyInterestRate = Math.Pow(monthly_interest_rate + 1, 1.0 / 30.0) - 1;
            return dailyInterestRate;
        }

        public static double ConvertFromMonthlyToYearly(double monthly_interest_rate)
        {
            double yearlyInterestRate = Math.Pow(monthly_interest_rate + 1, 12) - 1;
            return yearlyInterestRate;
        } 

        public static double ConvertFromYearlyToMonthly(double yearlyInterestRate)
        {
            double monthlyInterestRate = Math.Pow(yearlyInterestRate + 1, 1.0 / 12.0) - 1;
            return monthlyInterestRate;
        }

        public static double ConvertFromDailyToMonthly(double dailyInterestRate)
        {
            double monthlyInterestRate = Math.Pow(dailyInterestRate + 1, 30) - 1;
            return monthlyInterestRate;
        }
    }
}