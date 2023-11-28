using System;

namespace Calc_Rendimento
{
    class Program
    {
        static void Main()
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

            float[] teste = Calc_Table(capital, taxa, tempo, tipo2);


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
                Console.WriteLine("Insira o capital inicial: ");
                if (!(float.TryParse(Console.ReadLine(), out capital_inicial)))
                {
                    Console.WriteLine("\nErro. Insira um valor válido.\n");
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Digite 1 para taxa mensal e 2 para taxa anual: ");
                        string answer = Console.ReadLine();
                        if (!(int.TryParse(answer, out int aux_answer)))
                        {
                            Console.WriteLine("\nErro. Digite um valor válido.\n");
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
                                    Console.WriteLine("\nErro. Digite um valor válido.\n");
                                }
                            }
                        }
                    }
                    Console.WriteLine("Insira a taxa {0} (em %): ", tipo_periodo);
                    string aux_taxa = Console.ReadLine();
                    if (!(float.TryParse(aux_taxa, out taxa)))
                    {
                        Console.WriteLine("\nErro. Digite um valor válido.\n");
                    }
                    else
                    {
                        if (taxa <= 0)
                        {
                            Console.WriteLine("\nErro. Digite um valor válido.\n");
                        }
                        else
                        {
                            Console.WriteLine("Insira o período {0}: ", tipo_periodo);
                            string aux_periodo = Console.ReadLine();
                            if (int.TryParse(aux_periodo, out int aux_tempo))
                            {
                                if (aux_tempo <= 0)
                                {
                                    Console.WriteLine("\nErro. Digite um valor válido.\n");
                                }
                                else
                                {
                                    tempo = Convert.ToSingle(aux_tempo);
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nErro. Digite um valor válido.\n");
                            }
                        }
                    }
                }
            }


            float[] info = { capital_inicial, taxa, tempo, tipo_return };
            return info;
        }

        static float[] Calc_Table(float capital_inicial, float taxa, float periodo, string tipo)
        {
            int right_periodo = Convert.ToInt32(periodo);
            float[,] tabela = new float[right_periodo + 1, 3];
            float montante, total_juros, aux_montante, aux_juros;
            int int_periodo = Convert.ToInt16(periodo);
            int[] periodos = new int[int_periodo + 1];
            float[] montantes = new float[int_periodo + 1];
            float[] juros = new float[int_periodo + 1];

            for (int i = 0; i <= int_periodo; i++)
            {
                periodos[i] = i;
            }
            montantes[0] = capital_inicial;
            juros[0] = 0;

            Console.WriteLine("|  {0}  |  Montante  |  Juros  |", tipo);
            for (int linha = 0; linha < tabela.GetLength(0) + 1; linha++)
            {
                montantes[linha + 1] = montantes[linha] + (montantes[linha] * taxa);
                aux_juros[linha + 1] =

                aux_montante = montantes[linha];
                aux_juros = juros[linha];
                Console.WriteLine("|  {0}  |   {1}   |    {2}    |", linha + 1, aux_montante, aux_juros);
            }

            float[] results = { montante, total_juros };
            return results;
        }
    }

}