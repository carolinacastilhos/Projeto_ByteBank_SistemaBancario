using System;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank
{
    public class Program
    {
        public static void MenuPrincipal()
        {
            Console.WriteLine("O que você precisa?\n");
            Console.WriteLine("1. Adicionar nova conta");
            Console.WriteLine("2. Listar todas as contas registradas");
            Console.WriteLine("3. Acessar/Manipular uma conta");
            Console.WriteLine("4. Excluir uma conta");
            Console.WriteLine("5. Finalizar programa\n");
            Console.Write("Digite a opção desejada: ");
            
        }

        public static void ManipularConta(int indexParaLogar,List<string> cpfs, List<string> titulares, List<string> chavesPix, List<string> senhas, List<double> saldos)
        {
            int opcaoMenuManipular;

            do
            {
                Console.WriteLine("O que você precisa?\n");
                Console.WriteLine("1. Detalhes da conta");
                Console.WriteLine("2. Realizar transações");
                Console.WriteLine("3. Sair da conta (Logout)\n");
                Console.Write("Digite a opção desejada: ");

                int.TryParse(Console.ReadLine(), out opcaoMenuManipular);

                switch (opcaoMenuManipular)
                {
                    case 1:
                        //DetalhesConta(contas, cpfs, titulares, chavesPix, saldos);
                        break;
                    case 2:
                        MenuTransacoes();
                        int.TryParse(Console.ReadLine(), out int opcaoMenuTransacoes);
                        switch (opcaoMenuTransacoes)
                        {
                            case 1:
                                //saque
                                break;
                            case 2:
                                Deposito();
                                break;
                            case 3:
                                //fazer pix
                                break;
                            case 4:
                                //receber pix
                                break;
                            case 5:
                                return;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                                Console.ResetColor();
                                break;
                        }
                        break;
                    case 3:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        break;
                }

            } while (opcaoMenuManipular != 3);
                       
        }

        private static void MenuTransacoes()
        {
            Console.WriteLine("O que você precisa?\n");
            Console.WriteLine("1. Saque");
            Console.WriteLine("2. Depósito");
            Console.WriteLine("3. Fazer Pix");
            Console.WriteLine("4. Receber Pix");
            Console.WriteLine("5. Voltar ao menu anterior\n");
            Console.Write("Digite a opção desejada: ");
        }

        public static void RegistrarNovoUsuario(List<string> titulares, List<string> cpfs, List<string> chavesPix, List<string> senhas, List<int> contas, List<double> saldos)
        {
            Console.Write("Digite o nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Adicione uma chave Pix (uma palavra ou email): ");
            chavesPix.Add(Console.ReadLine());
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
                    Console.WriteLine("As senhas não conferem. Por favor, digite novamente.\n");
                    Console.ResetColor();
                }
            }
            while (senha != senha2);

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Senha salva.");
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

        public static void ListarTodasAsContas(List<int> contas, List<string> titulares, List<string> cpfs, List<string> chavesPix)
        {
            if (cpfs.Count > 0)
            {
                for (int i = 0; i < cpfs.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Conta: {contas[i]} | Titular: {titulares[i]} | CPF: {cpfs[i]} | Chave Pix: {chavesPix[i]}\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Não há contas registradas até o momento.");
                Console.ResetColor();
            }
        }
                
        public static void LoginDeAcesso(List<string> cpfs, List<string> titulares, List<string> chavesPix, List<string> senhas, List<double> saldos)
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
                        Console.WriteLine("Senha válida");
                        ManipularConta(indexParaLogar, cpfs, titulares, chavesPix, senhas, saldos); 
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
                
        public static void ExcluirConta(List<string> cpfs, List<string> titulares, List<string> chavesPix, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Não foi possível deletar esta conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
                Console.ResetColor();
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            chavesPix.RemoveAt(indexParaDeletar);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nConta deletada com sucesso.\n");
            Console.ResetColor();
        }
                
        /*public static void DetalhesConta(List<int> contas, List<string> cpfs, List<string> titulares, List<string> chavesPix, List<double> saldos)
        {
                       
            Console.WriteLine($"Conta nº {contas[index]} | CPF:  {cpfs[index]} | Titular: {titulares[index]} | Chave Pix: {chavesPix[index]} | Saldo: R${saldos[index]:F2}");
        }*/

        public static void Deposito()
        {
            //Console.WriteLine($"Este é o seu saldo atual: R$ {saldo:f2}");
            Console.WriteLine();
            Console.Write("Digite o valor que deseja depositar na sua conta: ");
            double.TryParse(Console.ReadLine(), out double valorDeposito);

            if (valorDeposito <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Valor inválido. Tente outro valor");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Depósito realizado com sucesso!");
                Console.ResetColor();
            }
            
            //Console.WriteLine($"Saldo atual após o depósito: R$ {(saldo + valorDeposito):f2}");
        }

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Olá! Bem-Vindo(a) ao ByteBank!\n");
            Console.ResetColor();

            List<int> contas = new List<int>();
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> chavesPix = new List<string>();
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
                        RegistrarNovoUsuario(titulares, cpfs, chavesPix, senhas, contas, saldos);
                        break;
                    case 2:
                        ListarTodasAsContas(contas, titulares, cpfs, chavesPix);    
                        break;
                    case 3:
                        LoginDeAcesso(cpfs, titulares, chavesPix, senhas, saldos);              
                        break;
                    //case 4:
                      //  LoginDeAcesso(cpfs, titulares, chavesPix, senhas, saldos);
                       // ExcluirConta(cpfs, titulares, chavesPix, senhas, saldos);
                       // break;
                    case 5:
                        Console.WriteLine("Finalizando o programa... Até mais!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.\n");
                        Console.ResetColor();
                        break;

                }
                                
            }
            while (opcao != 5);

            


        }
                
    }
}
