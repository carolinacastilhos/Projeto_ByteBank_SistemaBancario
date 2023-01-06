using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank
{
    public class Program
    {
        public static void MenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("- MENU PRINCIPAL -\n");
            Console.ResetColor();
           
            Console.WriteLine("1. Adicionar nova conta");
            Console.WriteLine("2. Listar todas as contas registradas");
            Console.WriteLine("3. Acessar/Manipular uma conta");
            Console.WriteLine("4. Finalizar programa\n");
            Console.Write("Digite a opção desejada: ");            
        }

        public static void ManipularConta(int indexParaLogar,List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            int opcaoMenuManipular;

            do
            {
                Console.ForegroundColor= ConsoleColor.DarkCyan;
                Console.WriteLine("Olá, {0}!", titulares[indexParaLogar]);
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n- MENU CONTA -\n");
                Console.ResetColor();
                
                Console.WriteLine("1. Detalhes da conta");
                Console.WriteLine("2. Realizar transações");
                Console.WriteLine("3. Excluir conta");
                Console.WriteLine("4. Sair da conta (Logout)\n");
                Console.Write("Digite a opção desejada: ");

                int.TryParse(Console.ReadLine(), out opcaoMenuManipular);

                switch (opcaoMenuManipular)
                {
                    case 1:
                        DetalhesConta(indexParaLogar, contas, cpfs, titulares, saldos);
                        break;
                    case 2:
                        MenuTransacoes(indexParaLogar, cpfs, titulares, saldos, contas);
                        break;
                    case 3:
                        ExcluirConta(indexParaLogar, cpfs, titulares, senhas, saldos, contas);
                        break;
                    case 4:
                        LogoutConta(indexParaLogar, titulares);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        break;
                }

            } while (opcaoMenuManipular != 4);
                       
        }

        private static void MenuTransacoes(int indexParaLogar, List<string> cpfs, List<string> titulares, List<double> saldos, List<int> contas)
        {
            Console.WriteLine("O que você precisa?\n");
            Console.WriteLine("1. Saque");
            Console.WriteLine("2. Depósito");
            Console.WriteLine("3. Transferência entre contas");
            Console.WriteLine("4. Voltar ao menu anterior\n");
            Console.Write("Digite a opção desejada: ");

            int.TryParse(Console.ReadLine(), out int opcaoMenuTransacoes);
            switch (opcaoMenuTransacoes)
            {
                case 1:
                    Saque(indexParaLogar, saldos);
                    break;
                case 2:
                    Deposito(indexParaLogar, saldos);
                    break;
                case 3:
                    Transferencia(indexParaLogar, cpfs, contas, titulares, saldos);
                    break;
                case 4:
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                    Console.ResetColor();
                    break;
            }
        }

        public static void RegistrarNovoUsuario(List<string> titulares, List<string> cpfs, List<string> senhas, List<int> contas, List<double> saldos)
        {
            
            Console.Write("Digite o nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            saldos.Add(0);
              

            string senha = "0";
            string senha2 = "1";

            do
            {
                Console.Write("Digite uma nova senha: ");
                senha = EsconderSenha();
                Console.Write("Repita a nova senha: ");
                senha2 = EsconderSenha();
                
                if (senha != senha2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nAs senhas não conferem. Por favor, digite novamente.\n");
                    Console.ResetColor();
                }
            }
            while (senha != senha2);

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nSenha salva.");
            Console.ResetColor();
            
            senhas.Add(senha);

            Random numeroAleatorio = new Random();
            int conta = numeroAleatorio.Next(2500, 8750);
            contas.Add(conta);

            
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nConta nº {0} adicionada com sucesso.\n", conta);
            Console.ResetColor();
                        
        }
        
        public static string EsconderSenha()
        {
            var pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        public static void ListarTodasAsContas(List<int> contas, List<string> titulares, List<string> cpfs)
        {
            if (cpfs.Count > 0)
            {
                for (int i = 0; i < cpfs.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Conta: {contas[i]} | Titular: {titulares[i]} | CPF: {cpfs[i]}\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Não há contas registradas no momento.");
                Console.ResetColor();
            }
        }
                
        public static void LoginDeAcesso(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int>contas)
        {
            Console.WriteLine("Login de Acesso");
            Console.WriteLine("...............\n");

            int indexParaLogar;
            string entrada = "n";

              do
              {
                Console.Write("Digite o CPF: ");
                string cpfParaLogar = Console.ReadLine();
                indexParaLogar = cpfs.FindIndex(cpf => cpf == cpfParaLogar);
                                
                if (indexParaLogar == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nNão foi possível acessar a conta.\n");
                    Console.Write("CPF não encontrado. Você quer tentar novamente (S/N)? ");
                    Console.ResetColor();
                    entrada = Console.ReadLine().ToLower();
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    string senhaLogin = EsconderSenha();

                    if (senhas[indexParaLogar] == senhaLogin)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nSenha válida/n");
                        Console.ResetColor();
                        ManipularConta(indexParaLogar, cpfs, titulares, senhas, saldos, contas); 
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nNão foi possível acessar a conta.\n");
                        Console.Write("Senha não compatível. Você quer tentar novamente (S/N)? ");
                        Console.ResetColor();
                        entrada = Console.ReadLine().ToLower();
                        Console.WriteLine();
                    }
                }

              }
              while (entrada == "s"); 

        }

        public static void LogoutConta(int indexParaLogar, List<string> titulares)
        {
            string entrada = "s";

            do
            {
                Console.Write("Deseja realmente sair da conta (S/N)? ");
                entrada = Console.ReadLine().ToLower();

                if (entrada == "s")
                {
                    Console.WriteLine("Até mais {0}!", titulares[indexParaLogar]);
                    break;
                }
                else if (entrada == "n")
                {
                    Console.WriteLine("Ok. Digite qualquer tecla para voltar ao menu anterior");
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nCódigo digitado inválido.\n");
                    Console.ResetColor();
                }

            } while (entrada != "s");
        }          

        public static void ExcluirConta(int indexParaLogar, List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            string entrada = "n";

            do
            {
                DetalhesConta(indexParaLogar, contas, cpfs, titulares, saldos);

                Console.Write("Deseja mesmo excluir a conta (S/N)? ");
                entrada = Console.ReadLine().ToLower();

                if (entrada == "s")
                {
                    cpfs.Remove(cpfs[indexParaLogar]);
                    titulares.RemoveAt(indexParaLogar);
                    senhas.RemoveAt(indexParaLogar);
                    saldos.RemoveAt(indexParaLogar);
                    contas.RemoveAt(indexParaLogar);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nConta deletada com sucesso.\n");
                    Console.ResetColor();
                }
                else if (entrada == "n")
                {

                    Console.WriteLine("Ok. Digite qualquer tecla para voltar ao menu anterior");
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nCódigo digitado inválido.\n");
                    Console.ResetColor();
                }

            } while (entrada != "s");
                        
        }

        public static void DetalhesConta(int indexParaLogar, List<int> contas, List<string> cpfs, List<string> titulares, List<double> saldos)
        {               
            Console.WriteLine($"Conta nº {contas[indexParaLogar]} | CPF:  {cpfs[indexParaLogar]} | Titular: {titulares[indexParaLogar]} | Saldo: R${saldos[indexParaLogar]:F2}");
        }

        public static void Deposito(int indexParaLogar, List<double> saldos)
        {
            double valorDeposito;

            do
            {
                Console.WriteLine();
                Console.Write("Forneça o valor que deseja depositar na conta: ");
                double.TryParse(Console.ReadLine(), out valorDeposito);


                if (valorDeposito <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Valor inválido. Tente outro valor");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Depósito realizado com sucesso!\n");
                    Console.ResetColor();

                    saldos[indexParaLogar] += valorDeposito;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Saldo atual após o depósito: R$ {saldos[indexParaLogar]:f2}");
                    Console.ResetColor();
                }

            } while (valorDeposito <= 0);
        }

        public static void Saque(int indexParaLogar, List<double> saldos)
        {
            double valorSaque;

            do
            {
                Console.WriteLine();
                Console.Write("Forneça o valor que deseja sacar da conta: ");
                double.TryParse(Console.ReadLine(), out valorSaque);


                if (valorSaque <= 0 || valorSaque > saldos[indexParaLogar])
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Valor inválido. Tente outro valor");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Saque realizado com sucesso!\n");
                    Console.ResetColor();

                    saldos[indexParaLogar] -= valorSaque;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Saldo atual após saque: R$ {saldos[indexParaLogar]:f2}");
                    Console.ResetColor();
                }

            } while (valorSaque <= 0 || valorSaque > saldos[indexParaLogar]);
        }

        public static void Transferencia(int indexParaLogar, List<string> cpfs, List<int> contas, List<string> titulares, List<double> saldos)
        {
            int indexContaDestino;
            string entrada = "n";

            do
            {
                Console.WriteLine();
                Console.Write("Forneça o CPF da conta na qual deseja realizar a transferência: ");
                string cpfContaDestino = Console.ReadLine();
                indexContaDestino = cpfs.FindIndex(cpf => cpf == cpfContaDestino);

                if (indexContaDestino == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nConta de destino não encontrada.\n");
                    //Console.Write("Você quer tentar novamente (S/N)? ");
                    Console.ResetColor();
                    //entrada = Console.ReadLine().ToLower();
                    Console.WriteLine();
                }
                else if (indexContaDestino == indexParaLogar)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nContas equivalentes.\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Conta Destino encontrada:\n" +
                        $"Conta nº {contas[indexContaDestino]} | CPF:  {cpfs[indexContaDestino]} | Titular: {titulares[indexContaDestino]}");
                    Console.ResetColor();
                }

            }while(indexContaDestino == -1 || indexContaDestino == indexParaLogar);

            double valorTransferencia;

            do
            {
                Console.Write("Digite o valor que deseja transferir para a Conta de destino: ");
                double.TryParse(Console.ReadLine(), out valorTransferencia);

                if (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar])
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Valor inválido. Tente outro valor");
                    Console.ResetColor();
                }
                else
                {
                    saldos[indexParaLogar] -= valorTransferencia;
                    saldos[indexContaDestino] += valorTransferencia;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Transferência realizada com sucesso!\n");
                    Console.WriteLine($"Saldo atual Conta Remetente após transferência: R$ {saldos[indexParaLogar]:f2}\n");
                    Console.ResetColor();
                }

            } while (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar]);
               
        }

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Olá! Bem-Vindo(a) ao ByteBank!\n");
            Console.ResetColor();

            List<int> contas = new List<int>();
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            
            Console.WriteLine();

            int opcao;

            do
            {
                MenuPrincipal();

                int.TryParse(Console.ReadLine(), out opcao);
                
                Console.WriteLine();
                Console.WriteLine(".............................");
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        RegistrarNovoUsuario(titulares, cpfs, senhas, contas, saldos);
                        break;
                    case 2:
                        ListarTodasAsContas(contas, titulares, cpfs);    
                        break;
                    case 3:
                        LoginDeAcesso(cpfs, titulares, senhas, saldos, contas);              
                        break;
                    case 4:
                        Console.WriteLine("Finalizando o programa... Até mais!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        break;

                }
                                
            }
            while (opcao != 4);

            //Console.WriteLine("\nPressione qualquer tecla para continuar...");
            //Console.ReadKey();
           // Console.Clear();


        }
                
    }
}
