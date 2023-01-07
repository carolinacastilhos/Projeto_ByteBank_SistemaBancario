using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

namespace ByteBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            List<int> contas = new List<int>();
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
                        
            int opcao;
            
            do
            {
                TituloSistema();
                MenuPrincipal();

                int.TryParse(Console.ReadLine(), out opcao);
                
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
                        ExcluirConta(cpfs, titulares, senhas, saldos, contas);
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nFinalizando o programa... Até mais!\n");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nOpção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                        
                }

            }
            while (opcao != 5);

            //Console.WriteLine("\nPressione qualquer tecla para continuar...");
            //Console.ReadKey();
            //Console.Clear();

        }

        public static void TituloSistema()
        {
            //Console.WriteLine();
            //Console.ForegroundColor = ConsoleColor.DarkCyan;
            //Console.WriteLine(" ________________________________________");
            //Console.WriteLine("|                                        |");
            //Console.WriteLine("|                ByteBank                |");
            //Console.WriteLine("|                                        |");
            //Console.WriteLine("|________________________________________|");
            //Console.ResetColor();
            //Console.WriteLine();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("            ByteBank!          ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("                               ");
            Console.ResetColor();
            Console.WriteLine();
                       
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("\n     O seu sistema bancário    \n");
            //Console.ResetColor();
                                 
        }

        public static void MenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMENU PRINCIPAL\n");
            Console.ResetColor();
           
            Console.WriteLine("1. Adicionar nova conta");
            Console.WriteLine("2. Listar todas as contas registradas");
            Console.WriteLine("3. Acessar/Manipular uma conta");
            Console.WriteLine("4. Excluir uma conta");
            Console.WriteLine("5. Finalizar programa\n");
            Console.Write("Digite a opção desejada: ");            
        }

        public static void ManipularConta(int indexParaLogar,List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            int opcaoMenuManipular;

            Console.Clear();

            TituloSistema();

            do
            {                
                Console.ForegroundColor= ConsoleColor.DarkCyan;
                Console.WriteLine();
                Console.WriteLine("Bem-Vindo(a), {0}!", titulares[indexParaLogar]);
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("MENU CONTA\n");
                Console.ResetColor();
                Console.WriteLine("1. Detalhes da conta");
                Console.WriteLine("2. Realizar transações");
                Console.WriteLine("3. Sair da conta (Logout)\n");
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
                        LogoutConta(indexParaLogar, titulares);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (opcaoMenuManipular != 3);
                       
        }

        private static void MenuTransacoes(int indexParaLogar, List<string> cpfs, List<string> titulares, List<double> saldos, List<int> contas)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMENU TRANSAÇÕES\n");
            Console.ResetColor();
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
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }

        public static void RegistrarNovoUsuario(List<string> titulares, List<string> cpfs, List<string> senhas, List<int> contas, List<double> saldos)
        {
            int indexParaCompararTitulares;
            int indexParaCompararCpfs;
            string entradaTitular;
            string entradaCpf;
            string senha = "0";
            string senha2 = "1";

            Console.Clear();

            do
            {
                TituloSistema();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nADICIONAR NOVA CONTA\n");
                Console.ResetColor();

                Console.Write("Digite o nome completo: ");
                entradaTitular = Console.ReadLine();
                Console.Write("Digite o CPF: ");
                entradaCpf = Console.ReadLine();

                indexParaCompararTitulares = titulares.FindIndex(titular => titular == entradaTitular);
                indexParaCompararCpfs = cpfs.FindIndex(cpf => cpf == entradaCpf);

                if (indexParaCompararTitulares >= 0 || indexParaCompararCpfs >= 0 )
                {
                    Console.ForegroundColor= ConsoleColor.DarkYellow;
                    Console.WriteLine("\nConta com este Titular e CPF já existente. Tente Novamente.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    titulares.Add(entradaTitular);
                    cpfs.Add(entradaCpf);
                    
                }

            } while (indexParaCompararTitulares >= 0  || indexParaCompararCpfs >= 0);

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
            saldos.Add(0);

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nConta nº {0} adicionada com sucesso.\n", conta);
            Console.ResetColor();
            Console.WriteLine("< Pressione qualquer tecla para retornar ao Menu principal");
            Console.ReadKey();
            Console.Clear();

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
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nCONTAS CADASTRADAS\n");
            Console.ResetColor();

            if (cpfs.Count > 0)
            {
                for (int i = 0; i < cpfs.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Conta:");
                    Console.ResetColor();
                    Console.WriteLine($" {contas[i]} | Titular: {titulares[i]} | CPF: {cpfs[i]}\n");
                    
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nNão há contas registradas no momento.\n");
                Console.ResetColor();
            }

            Console.WriteLine("< Pressione qualquer tecla para retornar ao Menu principal");
            Console.ReadKey();
            Console.Clear();
        }
                
        public static void LoginDeAcesso(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int>contas)
        {

            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nLOGIN DE ACESSO\n");
            Console.ResetColor();
            
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
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    string senhaLogin = EsconderSenha();

                    if (senhas[indexParaLogar] == senhaLogin)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCarregando informações da conta..." +
                            " Digite qualquer tecla para continuar.\n");
                        Console.ReadKey();
                        Console.Clear();
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
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

              }
              while (entrada == "s"); 

        }

        public static void LogoutConta(int indexParaLogar, List<string> titulares)
        {
            Console.Clear();

            TituloSistema();

            string entrada = "s";

            do
            {
                Console.Write("Deseja realmente sair da conta (S/N)? ");
                entrada = Console.ReadLine().ToLower();

                if (entrada == "s")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\nAté mais {0}!", titulares[indexParaLogar]);
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else if (entrada == "n")
                {
                    Console.WriteLine("\nOk. Digite qualquer tecla para voltar ao menu anterior");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nCódigo digitado inválido. Tente novamente.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (entrada != "s");
        }          

        public static void ExcluirConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<int> contas)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEXCLUIR CONTA\n");
            Console.ResetColor();

            string entrada = "n";
            int indexParaExcluir;

            do
            {
                Console.Write("Digite o CPF: ");
                string cpfParaExcluir = Console.ReadLine();
                indexParaExcluir = cpfs.FindIndex(cpf => cpf == cpfParaExcluir);
                

                if (indexParaExcluir == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("CPF não encontrado. Você quer tentar novamente (S/N)? ");
                    Console.ResetColor();
                    entrada = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    string senhaExcluir = EsconderSenha();

                    if (senhas[indexParaExcluir] == senhaExcluir)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Conta:");
                        Console.ResetColor();
                        Console.WriteLine($"{contas[indexParaExcluir]} | Titular: {titulares[indexParaExcluir]} | CPF: {cpfs[indexParaExcluir]}\n");
                                               
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nNão foi possível excluir a conta.\n");
                        Console.Write("Senha não compatível. Você quer tentar novamente (S/N)? ");
                        Console.ResetColor();
                        entrada = Console.ReadLine().ToLower();
                        Console.WriteLine();
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
            while (entrada == "s");

            do
            {                                
                Console.Write("\nDeseja realmente excluir a conta (S/N)? ");
                entrada = Console.ReadLine().ToLower();

                if (entrada == "s")
                {
                    cpfs.Remove(cpfs[indexParaExcluir]);
                    titulares.RemoveAt(indexParaExcluir);
                    senhas.RemoveAt(indexParaExcluir);
                    saldos.RemoveAt(indexParaExcluir);
                    contas.RemoveAt(indexParaExcluir);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nConta deletada com sucesso.\n");
                    Console.ResetColor();                    

                }
                else if (entrada == "n")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nConta não foi deletada!\n");
                    Console.ResetColor();
                    Console.WriteLine("< Digite qualquer tecla para voltar ao Menu");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nCódigo digitado inválido. Tente novamente.\n");
                    Console.ResetColor();
                }

            } while (entrada != "s");

            Console.ReadKey();
            Console.Clear();
            
        }

        public static void DetalhesConta(int indexParaLogar, List<int> contas, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDETALHES DA CONTA\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Conta:");
            Console.ResetColor();
            Console.WriteLine($"{contas[indexParaLogar]} | CPF:  {cpfs[indexParaLogar]} | Titular: {titulares[indexParaLogar]} | Saldo: R${saldos[indexParaLogar]:F2}\n");
            Console.ResetColor();

            Console.WriteLine("< Pressione qualquer tecla para retornar");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Deposito(int indexParaLogar, List<double> saldos)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDEPÓSITO\n");
            Console.ResetColor();

            double valorDeposito;

            do
            {
                Console.WriteLine();
                Console.Write("\nForneça o valor que deseja depositar na conta: ");
                double.TryParse(Console.ReadLine(), out valorDeposito);


                if (valorDeposito <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nValor inválido. Tente outro valor\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nDepósito realizado com sucesso!\n");
                    Console.ResetColor();

                    saldos[indexParaLogar] += valorDeposito;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nSaldo atual após o depósito: R$ {saldos[indexParaLogar]:f2}\n");
                    Console.ResetColor();
                }

            } while (valorDeposito <= 0);

            Console.WriteLine("< Pressione qualquer tecla para retornar ao Menu");
            Console.ReadKey();
            Console.Clear();

        }

        public static void Saque(int indexParaLogar, List<double> saldos)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSAQUE\n");
            Console.ResetColor();

            double valorSaque;

            do
            {
                Console.WriteLine();
                Console.Write("\nForneça o valor que deseja sacar da conta: ");
                double.TryParse(Console.ReadLine(), out valorSaque);


                if (valorSaque <= 0 || valorSaque > saldos[indexParaLogar])
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nValor inválido. Tente outro valor\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSaque realizado com sucesso!\n");
                    Console.ResetColor();

                    saldos[indexParaLogar] -= valorSaque;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nSaldo atual após saque: R$ {saldos[indexParaLogar]:f2}\n");
                    Console.ResetColor();
                    
                }

            } while (valorSaque <= 0 || valorSaque > saldos[indexParaLogar]);

            Console.WriteLine("< Pressione qualquer tecla para retornar ao Menu");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Transferencia(int indexParaLogar, List<string> cpfs, List<int> contas, List<string> titulares, List<double> saldos)
        {
            Console.Clear();

            TituloSistema();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nTRANSFERÊNCIA ENTRE CONTAS\n");
            Console.ResetColor();

            int indexContaDestino;
            string entrada = "n";

            do
            {
                Console.WriteLine();
                Console.Write("\nForneça o CPF da conta na qual deseja realizar a transferência: ");
                string cpfContaDestino = Console.ReadLine();
                indexContaDestino = cpfs.FindIndex(cpf => cpf == cpfContaDestino);

                if (indexContaDestino == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nConta de destino não encontrada. Tente novamente.\n");
                    Console.ResetColor();
                    
                }
                else if (indexContaDestino == indexParaLogar)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nContas equivalentes.Tente novamente.\n");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nConta Destino encontrada:\n" +
                        $"Conta nº {contas[indexContaDestino]} | CPF:  {cpfs[indexContaDestino]} | Titular: {titulares[indexContaDestino]}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

            }while(indexContaDestino == -1 || indexContaDestino == indexParaLogar);

            double valorTransferencia;

            do
            {
                Console.Write("\nDigite o valor que deseja transferir para a Conta de destino: ");
                double.TryParse(Console.ReadLine(), out valorTransferencia);

                if (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar])
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nValor inválido. Tente outro valor\n");
                    Console.ResetColor();
                }
                else
                {
                    saldos[indexParaLogar] -= valorTransferencia;
                    saldos[indexContaDestino] += valorTransferencia;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTransferência realizada com sucesso!\n");
                    Console.WriteLine($"\nSaldo atual Conta Remetente após transferência: R$ {saldos[indexParaLogar]:f2}\n");
                    Console.ResetColor();                    
                }

            } while (valorTransferencia <= 0 || valorTransferencia > saldos[indexParaLogar]);

            Console.WriteLine("< Pressione qualquer tecla para retornar ao Menu");
            Console.ReadKey();
            Console.Clear();
        }
               
                
    }
}
