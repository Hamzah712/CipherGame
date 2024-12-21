using System;

namespace CipherGame
{
    public class CryptographyTool
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                DisplayMainMenu();

                Console.Write("\nEnter your choice (1-7): ");
                string choice = Console.ReadLine();

                if (choice == "7")
                {
                    Console.WriteLine("\nThank you for using the Cryptography Tool!");
                    break;
                }

                if (IsValidChoice(choice))
                {
                    ProcessCipherChoice(choice);
                }
                else
                {
                    Console.WriteLine("\nInvalid choice! Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        Cryptography Tool Menu          ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  1. Caesar Cipher                      ║");
            Console.WriteLine("║  2. Monoalphabetic Substitution Cipher ║");
            Console.WriteLine("║  3. Playfair Cipher                    ║");
            Console.WriteLine("║  4. Hill Cipher                        ║");
            Console.WriteLine("║  5. Vigenère Cipher                    ║");
            Console.WriteLine("║  6. Rail Fence Cipher                  ║");
            Console.WriteLine("║  7. Exit                               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }

        static bool IsValidChoice(string choice)
        {
            return choice.Length == 1 && "1234567".Contains(choice);
        }
        
        static void ProcessCipherChoice(string cipherChoice)
        {
            string cipherName = GetCipherName(cipherChoice);

            Console.Clear();
            Console.WriteLine($"╔════════════════════════════════════════╗");
            Console.WriteLine($"║           {cipherName,-26}   ║");
            Console.WriteLine($"╠════════════════════════════════════════╣");
            Console.WriteLine($"║  1. Encrypt                            ║");
            Console.WriteLine($"║  2. Decrypt                            ║");
            Console.WriteLine($"║  3. Return to Main Menu                ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");

            Console.Write("\nEnter your choice (1-3): ");
            string operationChoice = Console.ReadLine();

            if (operationChoice == "3")
                return;

            if (operationChoice == "1" || operationChoice == "2")
            {
                PerformCipherOperation(cipherChoice, operationChoice);
            }
            else
            {
                Console.WriteLine("\nInvalid choice! Press any key to try again...");
                Console.ReadKey();
            }
        }

        static string GetCipherName(string choice)
        {
            return choice switch
            {
                "1" => "Caesar Cipher",
                "2" => "Monoalphabetic Cipher",
                "3" => "Playfair Cipher",
                "4" => "Hill Cipher",
                "5" => "Vigenère Cipher",
                "6" => "Rail Fence Cipher",
                _ => "Unknown Cipher"
            };
        }

        static void PerformCipherOperation(string cipherChoice, string operationChoice)
        {
            Console.Clear();
            string operation = operationChoice == "1" ? "Encryption" : "Decryption";
            string cipherName = GetCipherName(cipherChoice);

            Console.WriteLine($"=== {cipherName} {operation} ===\n");
            Console.Write("Enter the text: ");
            string text = Console.ReadLine();

            // Get additional parameters based on cipher type
            string key = GetCipherKey(cipherChoice);

            // Here you'll implement the actual cipher operations
            string result = ""; // This will be replaced with actual cipher implementation

            Console.WriteLine($"\nResult: {result}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static string GetCipherKey(string cipherChoice)
        {
            switch (cipherChoice)
            {
                case "1": // Caesar
                    Console.Write("Enter the shift value (1-25): ");
                    break;
                case "2": // Monoalphabetic
                    Console.Write("Enter the substitution alphabet: ");
                    break;
                case "3": // Playfair
                    Console.Write("Enter the key word: ");
                    break;
                case "4": // Hill
                    Console.Write("Enter the key matrix values: ");
                    break;
                case "5": // Vigenère
                    Console.Write("Enter the key word: ");
                    break;
                case "6": // Rail Fence
                    Console.Write("Enter the number of rails: ");
                    break;
            }
            return Console.ReadLine();
        }
    }
}