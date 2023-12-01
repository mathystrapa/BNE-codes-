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
                float investimento_mensal = info[4];
                string tipo_taxa1, tipo_taxa2;

                if (aux_tipo == 1.1f)
                {
                    tipo_taxa1 = "mensal";
                    tipo_taxa2 = "meses";
                }
                else
                {
                    tipo_taxa1 = "anual";
                    tipo_taxa2 = "anos";
                }

                Console.WriteLine("\nSimulação de investimento inicial de R$ {0} mais um investimento mensal de R$ {1} a uma taxa {2} de {3} em um período de {4} {5}:\n", capital, investimento_mensal, tipo_taxa1, taxa, tempo, tipo_taxa2);

                float[] resultados = Calc_Table(capital, taxa, Convert.ToInt32(tempo), investimento_mensal, tipo_taxa2);
                Console.WriteLine("\nO investimento inicial de R$ {0} mais um investimento mensal de R$ {1} a uma taxa {2} de {3} em um período de {4} {5} irá gerar um montante de R$ {6}, o que representa um lucro de R$ {7} ({8} %) sobre o total investido ({9})\n", capital, investimento_mensal, tipo_taxa1, taxa, tempo, tipo_taxa2, resultados[0], resultados[1], resultados[2], resultados[3]);

                AskSimulation();
            } 
        }

        static float[] Get_info()
        {
            string tipo_periodo;
            float capital_inicial, tipo_return, taxa, tempo, investimento_mensal;
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
                            if (int.TryParse(aux_periodo, out int aux_tempo))
                            {
                                if (aux_tempo <= 0)
                                {
                                    ReturnErrorMessage();
                                }
                                else
                                {
                                    tempo = Convert.ToSingle(aux_tempo);
                                    Console.WriteLine("Insira o investimento mensal (se houver): ");
                                    string? aux_investimento_mensal = Console.ReadLine();
                                    if (!(float.TryParse(aux_investimento_mensal, out investimento_mensal)))
                                    {
                                        ReturnErrorMessage();
                                    }
                                    else
                                    {
                                        break;
                                    }
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


            float[] info = { capital_inicial, taxa, tempo, tipo_return, investimento_mensal };
            return info;
        }

        static float[] Calc_Table(float capital_inicial, float taxa, int periodo, float investimento_mensal, string tipo)
        {
            periodo *= 12;
            float[,] tabela = new float[periodo + 1, 3];
            float total_juros, porcentagem_lucro;
            int[] periodos = new int[periodo + 1];
            float[] montantes = new float[periodo + 1];
            float[] juros = new float[periodo + 1];
            float total_investido = capital_inicial + (investimento_mensal * (periodo - 1));

            for (int i = 0; i <= periodo; i++)
            {
                periodos[i] = i;
            }

            if (tipo == "anos")
                {
                    taxa = Convert.ToSingle(Math.Pow(1 + taxa, 1.0 / 12.0) - 1);
                }

            juros[0] = 0;
            montantes[0] = capital_inicial;
            juros[1] = montantes[0] * taxa;
            montantes[1] = montantes[0] + juros[1];
            juros[2] = montantes[1] * taxa;
            montantes[2] = montantes[1] + juros[2] + investimento_mensal;
            total_juros = juros[1] + juros[2];

            Console.WriteLine("|  Meses  |  Montante  |  Juros  |");
            Console.WriteLine("|    0    |  {0}  |   0   |", (float)Math.Round(montantes[0], 2));
            Console.WriteLine("|    1    |  {0}  |  {1}  |", (float)Math.Round(montantes[1], 2), (float)Math.Round(juros[1], 2));
            for (int linha = 2; linha <= periodo; linha++)
            {
                Console.WriteLine("|  {0}  |   {1}   |    {2}    |", linha, (float)Math.Round(montantes[linha], 2), (float)Math.Round(juros[linha], 2));
                total_juros += juros[linha];
                if (linha != periodo)
                {
                    juros[linha + 1] = montantes[linha] * taxa;
                    montantes[linha + 1] = montantes[linha] + juros[linha + 1] + investimento_mensal;
                }
                else
                {
                    break;
                }
            }

            porcentagem_lucro = total_juros * 100 / (total_investido);
            float[] results = { (float)Math.Round(montantes[periodo], 2), (float)Math.Round(total_juros, 2), (float) Math.Round(porcentagem_lucro, 2), total_investido };
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