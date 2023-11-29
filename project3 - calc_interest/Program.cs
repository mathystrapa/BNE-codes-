using System;
using System.IO.Pipes;

namespace Calc_Rendimento
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                float[] info = Get_info();
                float capital = info[0];
                float taxa = info[1] / 100;
                float tempo = info[2];
                float aux_tipo = info[3];
                string tipo1, tipo2;

                if (aux_tipo == 1.1f)
                {
                    tipo1 = "mensal";
                    tipo2 = "meses";
                }
                else
                {
                    tipo1 = "anual";
                    tipo2 = "anos";
                }
                Console.WriteLine("\nSimulação de investimento de R$ {0} a uma taxa {1} de {2} em um período de {3} {4}:\n", capital, tipo1, taxa, tempo, tipo2);

                float[] resultados = Calc_Table(capital, taxa, Convert.ToInt32(tempo), tipo2);
                Console.WriteLine("\nO investimento de R$ {0} a uma taxa {1} de {2} % em um período de {3} {4} irá gerar um montante de R$ {5}, o que representa um lucro de R$ {6} ({7} %).", capital, tipo1, taxa*100, tempo, tipo2, resultados[0], resultados[1], resultados[2]);

                AskSimulation();
            } 
        }

        static float[] Get_info()
        {
            float taxa;
            float tempo;
            float capital_inicial;
            float tipo_return;
            string tipo_periodo;

            while (true)
            {
                Console.WriteLine("Insira o capital inicial: R$ ");
                if (!(float.TryParse(Console.ReadLine(), out capital_inicial)))
                {
                    ReturnErrorMessage();
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Digite 1 para taxa mensal e 2 para taxa anual: ");
                        string? answer = Console.ReadLine();
                        if (!(int.TryParse(answer, out int aux_answer)))
                        {
                            ReturnErrorMessage();
                        }
                        else
                        {
                            if (aux_answer == 1)
                            {
                                tipo_periodo = "mensal";
                                tipo_return = 1.1f;
                                break;
                            }
                            else
                            {
                                if (aux_answer == 2)
                                {
                                    tipo_periodo = "anual";
                                    tipo_return = 1.2f;
                                    break;
                                }
                                else
                                {
                                    ReturnErrorMessage();
                                }
                            }
                        }
                    }
                    Console.WriteLine("Insira a taxa {0} (em %): ", tipo_periodo);
                    string? aux_taxa = Console.ReadLine();
                    if (!(float.TryParse(aux_taxa, out taxa)))
                    {
                        ReturnErrorMessage();
                    }
                    else
                    {
                        if (taxa <= 0)
                        {
                            ReturnErrorMessage();
                        }
                        else
                        {
                            Console.WriteLine("Insira o período {0}: ", tipo_periodo);
                            string? aux_periodo = Console.ReadLine();
                            if (!string.IsNullOrEmpty(aux_periodo) && int.TryParse(aux_periodo, out int aux_tempo))
                            {
                                if (aux_tempo <= 0)
                                {
                                    ReturnErrorMessage();
                                }
                                else
                                {
                                    tempo = Convert.ToSingle(aux_tempo);
                                    break;
                                }
                            }
                            else
                            {
                                ReturnErrorMessage();
                            }
                        }
                    }
                }
            }


            float[] info = { capital_inicial, taxa, tempo, tipo_return };
            return info;
        }

        static float[] Calc_Table(float capital_inicial, float taxa, int periodo, string tipo)
        {
            float[,] tabela = new float[periodo + 1, 3];
            float total_juros = 0, porcentagem_lucro;
            int[] periodos = new int[periodo + 1];
            float[] montantes = new float[periodo + 1];
            float[] juros = new float[periodo + 1];

            for (int i = 0; i <= periodo; i++)
            {
                periodos[i] = i;
            }
            montantes[0] = capital_inicial;
            juros[0] = 0;

            Console.WriteLine("|  {0}  |  Montante  |  Juros  |", tipo);
            for (int linha = 0; linha <= periodo; linha++)
            {
                Console.WriteLine("|  {0}  |   {1}   |    {2}    |", linha, montantes[linha], juros[linha]);
                total_juros += juros[linha];
                if (linha != periodo)
                {
                    juros[linha + 1] = (float)Math.Round(montantes[linha] * taxa, 2);
                    montantes[linha + 1] = (float)Math.Round(montantes[linha] + juros[linha + 1], 2);
                }
                else
                {
                    break;
                }
            }

            porcentagem_lucro = (total_juros * 100) / capital_inicial;
            float[] results = { (float)Math.Round(montantes[periodo], 2), (float)Math.Round(total_juros, 2), (float) Math.Round(porcentagem_lucro, 2) };
            return results;
        }

        static void ReturnErrorMessage()
        {
            Console.WriteLine("\nErro. Insira um valor válido.\n");
        }

        static bool AskSimulation()
        {
            while (true)
            {
                string answer;
                Console.WriteLine("\nDeseja fazer outra simulação? (digite 'S' para sim e 'N' para não)");
                answer = Console.ReadLine();
                if (answer != null)
                {
                    answer = answer.ToUpper();
                    if (answer == "N")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        if (answer == "S")
                        {
                            break;
                        }
                        else
                        {
                            ReturnErrorMessage();
                        }
                    }
                }
                else
                {
                    ReturnErrorMessage();
                }
            }
            return (true);
        }
    }
}