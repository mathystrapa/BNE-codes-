// PROBLEMA 1:	Crie uma Tabela que calcule pela fórmula o Rendimento de um Investimento

using System;

namespace InvestmentNamespace
{
    class Program
    {
        static void Main()
        {
            string? user_input;
            string tipo_taxa, tipo_tempo;
            while (true)
            {
                Console.WriteLine("\nInsira o investimento (R$): ");
                user_input = Console.ReadLine();
                if (float.TryParse(user_input, out float starting_capital) == false)
                {
                    ErrorMessage();
                }
                else
                {
                    Console.WriteLine("Qual o tipo do investimento? (1 para mensal e 2 para anual)");
                    user_input = Console.ReadLine();
                    if (int.TryParse(user_input, out int aux_tipo) == false)
                    {
                        ErrorMessage();
                    }
                    else
                    {
                        if (aux_tipo != 1 && aux_tipo != 2)
                        {
                            ErrorMessage();
                        }
                        else
                        {
                            if (aux_tipo == 1)
                            {
                                tipo_taxa = "mensal";
                                tipo_tempo = "meses";
                            }
                            else
                            {
                                tipo_taxa = "anual";
                                tipo_tempo = "anos";
                            }

                            Console.WriteLine($"\nInsira a taxa {tipo_taxa} em %: ");
                            user_input = Console.ReadLine();
                            if (float.TryParse(user_input, out float taxa) == false)
                            {
                                ErrorMessage();
                            }
                            else
                            {
                                taxa /= 100;
                                Console.WriteLine($"\nPor quantos {tipo_tempo} o investimento vai durar?");
                                user_input = Console.ReadLine();
                                if (float.TryParse(user_input, out float tempo) == false)
                                {
                                    ErrorMessage();
                                }
                                else
                                {
                                    Investment investment = new Investment(starting_capital, taxa, tempo);
                                    float amount = investment.Montante;
                                    float liquid_profit = investment.LucroLiquido;
                                    float percentage_profit = investment.LucroPorcentagem;
                                    investment.ShowResults(starting_capital, taxa, tempo, amount, liquid_profit, percentage_profit, aux_tipo);
                                    AskUser();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void AskUser()
        {
            while (true)
            {
                Console.WriteLine("\nDeseja fazer outra simulação? ('s' para sim e 'n' para não): ");
                string? answer = Console.ReadLine();
                if (answer.ToLower() == "n")
                {
                    Environment.Exit(0);
                }
                else
                {
                    if (answer.ToLower() == "s")
                    {
                        break;
                    }
                    else
                    {
                        ErrorMessage();
                    }
                }
            }
        }

        private static void ErrorMessage()
        {
            Console.WriteLine("\nErro. Por favor, digite uma resposta válida.\n");
        }
    }

    class Investment
    {
        public float Montante;
        public float LucroLiquido;
        public float LucroPorcentagem;

        public Investment(float capital_inicial, float taxa, float tempo)
        {
            Montante = CalcMontante(capital_inicial, taxa, tempo);
            float[] Lucros = CalcLucro(Montante, capital_inicial);
            LucroLiquido = Lucros[0];
            LucroPorcentagem = Lucros[1];
        }

        private float CalcMontante(float capital_inicial, float taxa, float tempo)
        {
            float montante = (float) (capital_inicial * (Math.Pow(1 + taxa, tempo)));
            return montante;
        }

        private float[] CalcLucro(float total, float capital_inicial)
        {
            float lucro_liquido = total - capital_inicial;
            float lucro_porcentagem = lucro_liquido * 100 / capital_inicial;
            float[] lucros = { lucro_liquido, lucro_porcentagem };
            return lucros;
        }

        public void ShowResults(float capital_inicial, float taxa, float tempo, float montante, float lucro_liquido, float lucro_porcentagem, int tipo)
        {
            string tipo_taxa, tipo_tempo;
            
            if (tipo == 1)
            {
                tipo_taxa = "mensal";

                if (tempo < 2)
                {
                    tipo_tempo = "mês";
                }
                else
                {
                    tipo_tempo = "meses";
                }
            }
            else
            {
                tipo_taxa = "anual";

                if (tempo < 2)
                {
                    tipo_tempo = "ano";
                }
                else
                {
                    tipo_tempo = "anos";
                }
            }

            Console.WriteLine($"O investimento de R$ {Math.Round(capital_inicial, 2)} a uma taxa {tipo_taxa} de {taxa * 100} % durante {tempo} {tipo_tempo} irá gerar um montante de R$ {Math.Round(montante, 2)}, o que representa um lucro de R$ {Math.Round(lucro_liquido, 2)} ({lucro_porcentagem} %).");
        }
    }
}