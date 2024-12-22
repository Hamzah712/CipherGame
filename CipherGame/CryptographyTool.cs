using System;
using System.Text;

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

                Console.Write("\nEnter your choice (1-10): ");
                string choice = Console.ReadLine();

                if (choice == "10")
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
            Console.WriteLine("║  1. Additive Cipher                    ║");
            Console.WriteLine("║  2. Multiplicative Cipher              ║");
            Console.WriteLine("║  3. Affine Cipher                      ║");
            Console.WriteLine("║  4. AutoKey Cipher                     ║");
            Console.WriteLine("║  5. Playfair Cipher                    ║");
            Console.WriteLine("║  6. Vigenère Cipher                    ║");
            Console.WriteLine("║  7. Hill Cipher                        ║");
            Console.WriteLine("║  8. Keyless Transposition Cipher       ║");
            Console.WriteLine("║  9. Keyed Transposition Cipher         ║");
            Console.WriteLine("║  10. Exit                              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }

        static bool IsValidChoice(string choice)
        {
            if (int.TryParse(choice, out int choiceNum))
            {
                return choiceNum >= 1 && choiceNum <= 10;
            }
            return false;
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
                "1" => "Additive Cipher",
                "2" => "Multiplicative Cipher",
                "3" => "Affine Cipher",
                "4" => "AutoKey Cipher",
                "5" => "Playfair Cipher",
                "6" => "Vigenère Cipher",
                "7" => "Hill Cipher",
                "8" => "Keyless Transposition Cipher",
                "9" => "Keyed Transposition Cipher",
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

            // Get the key based on the cipher choice
            string key = GetCipherKey(cipherChoice);

            // Perform the cipher operation
            string result = cipherChoice switch
            {
                "1" => operationChoice == "1" ? EncryptAdditiveCipher(text, key) : DecryptAdditiveCipher(text, key),
                "2" => operationChoice == "1" ? EncryptMultiplicativeCipher(text, key) : DecryptMultiplicativeCipher(text, key),
                "3" => operationChoice == "1" ? EncryptAffineCipher(text, key) : DecryptAffineCipher(text, key),
                "4" => operationChoice == "1" ? EncryptAutoKeyCipher(text, key) : DecryptAutoKeyCipher(text, key),
                "5" => operationChoice == "1" ? EncryptPlayfairCipher(text, key) : DecryptPlayfairCipher(text, key),
                "6" => operationChoice == "1" ? EncryptVigenereCipher(text, key) : DecryptVigenereCipher(text, key),
                "7" => operationChoice == "1" ? EncryptHillCipher(text, key) : DecryptHillCipher(text, key),
                "8" => operationChoice == "1" ? EncryptKeylessTranspositionCipher(text) : DecryptKeylessTranspositionCipher(text),
                "9" => operationChoice == "1" ? EncryptKeyedTranspositionCipher(text, key) : DecryptKeyedTranspositionCipher(text, key),
                _ => throw new ArgumentException("Invalid cipher choice")
            };

            Console.WriteLine($"\nResult: {result}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        // Cipher methods
        public static string EncryptAdditiveCipher(string text, string key)
        {
            int shift = int.Parse(key);
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)(((letter + shift - offset) % 26) + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
        public static string DecryptAdditiveCipher(string text, string key)
        {
            int shift = int.Parse(key);
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)(((letter - shift - offset + 26) % 26) + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }

        public static string EncryptMultiplicativeCipher(string text, string key)
        {
            if (!int.TryParse(key, out int multiplicativeKey))
                throw new ArgumentException("Key must be a valid integer.");
            if (GCD(multiplicativeKey, 26) != 1)
                throw new ArgumentException("Key must be coprime with 26.");

            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    // Convert to uppercase like in code 1
                    letter = char.ToUpper(letter);
                    // Use the same formula as code 1: (x * key) mod 26
                    letter = (char)(((letter - 'A') * multiplicativeKey) % 26 + 'A');
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
        public static string DecryptMultiplicativeCipher(string text, string key)
        {
            if (!int.TryParse(key, out int multiplicativeKey))
                throw new ArgumentException("Key must be a valid integer.");
            if (GCD(multiplicativeKey, 26) != 1)
                throw new ArgumentException("Key must be coprime with 26.");

            // Use MultiplicativeInverse instead of pow(key, -1, 26)
            int inverseKey = MultiplicativeInverse(multiplicativeKey, 26);

            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    // Convert to uppercase like in code 1
                    letter = char.ToUpper(letter);
                    // Use the same formula as code 1: (x * key_inverse) mod 26
                    letter = (char)(((letter - 'A') * inverseKey) % 26 + 'A');
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
        // Helper methods
        static int MultiplicativeInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }
            throw new ArgumentException("No multiplicative inverse found.");
        }
        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static string EncryptAffineCipher(string text, string key)
        {
            string[] keys = key.Split();
            if (keys.Length != 2 || !int.TryParse(keys[0], out int additiveKey) || !int.TryParse(keys[1], out int multiplicativeKey))
                throw new ArgumentException("Key must consist of two integers separated by a space.");
            if (GCD(multiplicativeKey, 26) != 1)
                throw new ArgumentException("Multiplicative key must be coprime with 26.");

            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((((multiplicativeKey * (letter - offset)) + additiveKey) % 26) + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
        public static string DecryptAffineCipher(string text, string key)
        {
            string[] keys = key.Split();
            if (keys.Length != 2 || !int.TryParse(keys[0], out int additiveKey) || !int.TryParse(keys[1], out int multiplicativeKey))
                throw new ArgumentException("Key must consist of two integers separated by a space.");
            if (GCD(multiplicativeKey, 26) != 1)
                throw new ArgumentException("Multiplicative key must be coprime with 26.");

            int inverseKey = MultiplicativeInverse(multiplicativeKey, 26);

            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((inverseKey * ((letter - offset - additiveKey + 26)) % 26) + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }

        public static string EncryptAutoKeyCipher(string text, string key)
        {
            text = text.ToUpper();
            key = key.ToUpper();

            string newKey = key + text;
            newKey = newKey.Substring(0, text.Length);

            char[] buffer = text.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    int first = letter - 'A';
                    int second = newKey[i] - 'A';
                    int total = (first + second) % 26;
                    buffer[i] = (char)(total + 'A');
                }
            }
            return new string(buffer);
        }
        public static string DecryptAutoKeyCipher(string text, string key)
        {
            text = text.ToUpper();
            key = key.ToUpper();

            char[] buffer = text.ToCharArray();
            string currentKey = key;

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    int get1 = letter - 'A';
                    int get2 = currentKey[i] - 'A';
                    int total = (get1 - get2) % 26;
                    total = (total < 0) ? total + 26 : total;
                    buffer[i] = (char)(total + 'A');
                    currentKey += (char)(total + 'A');
                }
            }
            return new string(buffer);
        }

        public static string EncryptPlayfairCipher(string text, string key)
        {
            text = text.ToLower();
            key = key.ToLower();
            text = new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            key = new string(key.Where(c => !char.IsWhiteSpace(c)).ToArray());

            // Generate the 5x5 matrix
            char[,] matrix = GeneratePlayfairMatrix(key);

            text = PrepareTextForPlayfair(text);

            char[] result = text.ToCharArray();

            for (int i = 0; i < text.Length; i += 2)
            {
                int[] positions = new int[4];
                (positions[0], positions[1]) = FindPositionInMatrix(matrix, result[i]);
                (positions[2], positions[3]) = FindPositionInMatrix(matrix, result[i + 1]);

                if (positions[0] == positions[2]) // Same row
                {
                    result[i] = matrix[positions[0], (positions[1] + 1) % 5];
                    result[i + 1] = matrix[positions[2], (positions[3] + 1) % 5];
                }
                else if (positions[1] == positions[3]) // Same column
                {
                    result[i] = matrix[(positions[0] + 1) % 5, positions[1]];
                    result[i + 1] = matrix[(positions[2] + 1) % 5, positions[3]];
                }
                else // Rectangle case
                {
                    result[i] = matrix[positions[0], positions[3]];
                    result[i + 1] = matrix[positions[2], positions[1]];
                }
            }

            return new string(result);
        }
        public static string DecryptPlayfairCipher(string text, string key)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return string.Empty;

            // Prepare the text (remove non-letters, handle 'j', ensure even length)
            text = PrepareTextForPlayfair(text.ToLower());
            key = PrepareTextForPlayfair(key.ToLower());

            char[,] matrix = GeneratePlayfairMatrix(key);
            string result = string.Empty;

            // Process pairs of characters
            for (int i = 0; i < text.Length - 1; i += 2)
            {
                (int row1, int col1) = FindPositionInMatrix(matrix, text[i]);
                (int row2, int col2) = FindPositionInMatrix(matrix, text[i + 1]);

                if (row1 == row2)
                {
                    result += matrix[row1, Mod5(col1 - 1)];  // Shift left
                    result += matrix[row2, Mod5(col2 - 1)];
                }
                else if (col1 == col2)
                {
                    result += matrix[Mod5(row1 - 1), col1];  // Shift up
                    result += matrix[Mod5(row2 - 1), col2];
                }
                else
                {
                    result += matrix[row1, col2];  // Rectangle case
                    result += matrix[row2, col1];
                }
            }

            return result;
        }
        // Helper method to handle negative modulo properly
        private static int Mod5(int n)
        {
            return ((n % 5) + 5) % 5;
        }
        // Helping methods for Playfair cipher
        static char[,] GeneratePlayfairMatrix(string key)
        {
            char[,] matrix = new char[5, 5];
            int[] used = new int[26];
            used['j' - 'a'] = 1;

            // Fill in the key first
            int row = 0, col = 0;
            foreach (char c in key)
            {
                if (c != 'j' && used[c - 'a'] != 2)
                {
                    matrix[row, col] = c;
                    used[c - 'a'] = 2;
                    col++;
                    if (col == 5)
                    {
                        col = 0;
                        row++;
                    }
                }
            }

            // Fill remaining spots with unused letters
            for (int k = 0; k < 26; k++)
            {
                if (used[k] == 0)
                {
                    matrix[row, col] = (char)(k + 'a');
                    col++;
                    if (col == 5)
                    {
                        col = 0;
                        row++;
                    }
                }
            }

            return matrix;
        }
        static string PrepareTextForPlayfair(string text)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    char currentChar = text[i] == 'j' ? 'i' : text[i];
                    result.Append(currentChar);
                }
            }

            if (result.Length % 2 != 0)
            {
                result.Append('z');
            }

            return result.ToString();
        }
        static (int, int) FindPositionInMatrix(char[,] matrix, char c)
        {
            if (c == 'j') c = 'i';

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == c)
                    {
                        return (i, j);
                    }
                }
            }
            throw new ArgumentException($"Character {c} not found in matrix");
        }
        // End of Playfair cipher helping methods

        public static string EncryptVigenereCipher(string text, string key)
        {
            key = key.ToUpper();
            char[] buffer = text.ToCharArray();
            int keyIndex = 0;

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    int keyShift = key[keyIndex % key.Length] - 'A';
                    letter = (char)(((letter + keyShift - offset) % 26) + offset);
                    keyIndex++;
                }
                buffer[i] = letter;
            }

            return new string(buffer);
        }
        public static string DecryptVigenereCipher(string text, string key)
        {
            key = key.ToUpper();
            char[] buffer = text.ToCharArray();
            int keyIndex = 0;

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    int keyShift = key[keyIndex % key.Length] - 'A';
                    letter = (char)(((letter - keyShift - offset + 26) % 26) + offset);
                    keyIndex++;
                }
                buffer[i] = letter;
            }

            return new string(buffer);
        }

        public static string EncryptHillCipher(string text, string key)
        {
            // Fixed size for 3x3 matrix
            int blockSize = 3;

            // Validate inputs
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                return string.Empty;

            // Ensure key is long enough for 3x3 matrix (9 characters)
            if (key.Length < blockSize * blockSize)
                throw new ArgumentException($"Key must be at least {blockSize * blockSize} characters long for {blockSize}x{blockSize} matrix.");

            // Initialize key matrix
            int[,] keyMatrix = new int[blockSize, blockSize];
            string processedKey = key.ToUpper().Replace(" ", "");
            int k = 0;

            for (int i = 0; i < blockSize; i++)
            {
                for (int j = 0; j < blockSize; j++)
                {
                    keyMatrix[i, j] = (processedKey[k++] - 'A') % 26;
                }
            }

            // Prepare text
            text = PrepareTextForHill(text, blockSize);
            char[] buffer = text.ToCharArray();

            // Process text in blocks
            for (int i = 0; i < buffer.Length; i += blockSize)
            {
                // Create message vector
                int[] messageVector = new int[blockSize];
                for (int j = 0; j < blockSize; j++)
                {
                    messageVector[j] = buffer[i + j] - 'A';
                }

                // Encrypt block
                int[] encryptedBlock = MultiplyMatrixVector(keyMatrix, messageVector);

                // Convert back to characters
                for (int j = 0; j < blockSize; j++)
                {
                    buffer[i + j] = (char)((encryptedBlock[j] % 26) + 'A');
                }
            }

            return new string(buffer);
        }
        public static string DecryptHillCipher(string text, string key)
        {
            // Fixed size for 3x3 matrix
            int blockSize = 3;

            // Generate and get inverse of key matrix
            int[,] keyMatrix = GenerateHillKeyMatrix(key);
            int[,] inverseKeyMatrix = InverseMatrix(keyMatrix);

            // Prepare text
            text = PrepareTextForHill(text, blockSize);
            char[] buffer = text.ToCharArray();

            // Process text in blocks
            for (int i = 0; i < buffer.Length; i += blockSize)
            {
                // Create cipher vector
                int[] cipherVector = new int[blockSize];
                for (int j = 0; j < blockSize; j++)
                {
                    cipherVector[j] = buffer[i + j] - 'A';
                }

                // Decrypt block using inverse matrix
                int[] decryptedBlock = MultiplyMatrixVector(inverseKeyMatrix, cipherVector);

                // Convert back to characters and ensure positive values
                for (int j = 0; j < blockSize; j++)
                {
                    int value = decryptedBlock[j] % 26;
                    if (value < 0)
                        value += 26;
                    buffer[i + j] = (char)(value + 'A');
                }
            }

            return new string(buffer);
        }
        // Helping methods for Hill cipher
        static int[,] GenerateHillKeyMatrix(string key)
        {
            int size = 3; // Fixed size for 3x3 matrix

            // Validate key length
            if (key.Length < size * size)
                throw new ArgumentException($"Key must be at least {size * size} characters long for {size}x{size} matrix.");

            int[,] matrix = new int[size, size];
            string processedKey = key.ToUpper().Replace(" ", "");
            int k = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (processedKey[k++] - 'A') % 26;
                }
            }

            return matrix;
        }
        static string PrepareTextForHill(string text, int blockSize)
        {
            text = text.ToUpper().Replace(" ", "");
            while (text.Length % blockSize != 0)
            {
                text += 'X';
            }
            return text;
        }
        static int[] MultiplyMatrixVector(int[,] matrix, int[] vector)
        {
            int size = vector.Length;
            int[] result = new int[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = 0;
                for (int j = 0; j < size; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }
        static int[,] InverseMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[,] adj = new int[n, n];
            int[,] inv = new int[n, n];
            int det = Determinant(matrix, n);
            int detInv = MultiplicativeInverse(det, 26);

            if (det == 0 || detInv == -1)
            {
                throw new InvalidOperationException("Matrix is not invertible.");
            }

            Adjoint(matrix, adj);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inv[i, j] = (adj[i, j] * detInv) % 26;
                    if (inv[i, j] < 0)
                    {
                        inv[i, j] += 26;
                    }
                }
            }

            return inv;
        }
        // Helping methods for Inverse Matrix calculation
        static int Determinant(int[,] matrix, int n)
        {
            if (n == 1)
            {
                return matrix[0, 0];
            }

            int det = 0;
            int[,] temp = new int[n, n];
            int sign = 1;

            for (int f = 0; f < n; f++)
            {
                GetCofactor(matrix, temp, 0, f, n);
                det += sign * matrix[0, f] * Determinant(temp, n - 1);
                sign = -sign;
            }

            return det % 26;
        }
        static void GetCofactor(int[,] matrix, int[,] temp, int p, int q, int n)
        {
            int i = 0, j = 0;

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = matrix[row, col];

                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }
        static void Adjoint(int[,] matrix, int[,] adj)
        {
            int n = matrix.GetLength(0);
            if (n == 1)
            {
                adj[0, 0] = 1;
                return;
            }

            int sign = 1;
            int[,] temp = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    GetCofactor(matrix, temp, i, j, n);
                    sign = ((i + j) % 2 == 0) ? 1 : -1;
                    adj[j, i] = (sign * Determinant(temp, n - 1)) % 26;
                    if (adj[j, i] < 0)
                    {
                        adj[j, i] += 26;
                    }
                }
            }
        }
        // End of Hill cipher helping methods

        public static string EncryptKeylessTranspositionCipher(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Input text cannot be null or empty.");

            text = text.Replace(" ", "").ToUpper();
            int length = text.Length;

            int numRows = (int)Math.Ceiling(Math.Sqrt(length));
            int numCols = (int)Math.Ceiling((double)length / numRows);

            char[,] grid = new char[numRows, numCols];
            int k = 0;

            // Fill the grid row by row
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (k < length)
                    {
                        grid[i, j] = text[k++];
                    }
                    else
                    {
                        grid[i, j] = 'X'; // Padding character
                    }
                }
            }

            // Read the grid column by column
            var resultBuilder = new StringBuilder();
            for (int j = 0; j < numCols; j++)
            {
                for (int i = 0; i < numRows; i++)
                {
                    resultBuilder.Append(grid[i, j]);
                }
            }

            return resultBuilder.ToString();
        }
        public static string DecryptKeylessTranspositionCipher(string text)
        {
            int length = text.Length;
            int numCols = (int)Math.Ceiling(Math.Sqrt(length));
            int numRows = (int)Math.Ceiling((double)length / numCols);

            char[,] grid = new char[numRows, numCols];
            int k = 0;

            // Fill the grid column by column
            for (int j = 0; j < numCols; j++)
            {
                for (int i = 0; i < numRows; i++)
                {
                    if (k < length)
                    {
                        grid[i, j] = text[k++];
                    }
                }
            }

            // Read the grid row by row to reconstruct the original message
            string result = string.Empty;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (grid[i, j] != '\0' && grid[i, j] != 'X')
                    {
                        result += grid[i, j];
                    }
                }
            }

            return result;
        }

        public static string EncryptKeyedTranspositionCipher(string text, string key)
        {
            text = text.Replace(" ", "").ToUpper();
            key = key.ToUpper();
            int numCols = key.Length;
            int numRows = (int)Math.Ceiling((double)text.Length / numCols);

            char[,] grid = new char[numRows, numCols];
            int k = 0;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (k < text.Length)
                    {
                        grid[i, j] = text[k++];
                    }
                    else
                    {
                        grid[i, j] = 'X';
                    }
                }
            }

            int[] keyOrder = GetKeyOrder(key);
            string result = string.Empty;

            for (int col = 0; col < numCols; col++)
            {
                int j = keyOrder[col];
                for (int i = 0; i < numRows; i++)
                {
                    result += grid[i, j];
                }
            }

            return result;
        }
        public static string DecryptKeyedTranspositionCipher(string text, string key)
        {
            key = key.ToUpper();
            int numCols = key.Length;
            int numRows = (int)Math.Ceiling((double)text.Length / numCols);

            char[,] grid = new char[numRows, numCols];
            int[] keyOrder = GetKeyOrder(key);
            int k = 0;

            for (int col = 0; col < numCols; col++)
            {
                int j = keyOrder[col];
                for (int i = 0; i < numRows; i++)
                {
                    if (k < text.Length)
                    {
                        grid[i, j] = text[k++];
                    }
                }
            }

            string result = string.Empty;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (grid[i, j] != 'X')
                    {
                        result += grid[i, j];
                    }
                }
            }

            return result;
        }
        public static int[] GetKeyOrder(string key) // Helper method for Keyed Transposition Cipher
        {
            char[] sortedKey = key.ToCharArray();
            Array.Sort(sortedKey);

            int[] keyOrder = new int[key.Length];
            bool[] used = new bool[key.Length]; // To handle duplicates

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < sortedKey.Length; j++)
                {
                    if (!used[j] && key[i] == sortedKey[j])
                    {
                        keyOrder[i] = j;
                        used[j] = true;
                        break;
                    }
                }
            }

            return keyOrder;
        }
        // End of cipher methods

        static string GetCipherKey(string cipherChoice)
        {
            return cipherChoice switch
            {
                "1" => GetAdditiveCipherKey(),
                "2" => GetMultiplicativeCipherKey(),
                "3" => GetAffineCipherKey(),
                "4" => GetAutoKeyCipherKey(),
                "5" => GetPlayfairCipherKey(),
                "6" => GetVigenereCipherKey(),
                "7" => GetHillCipherKey(),
                "8" => GetKeylessTranspositionCipherKey(),
                "9" => GetKeyedTranspositionCipherKey(),
                _ => throw new ArgumentException("Invalid cipher choice")
            };
        }
        // Separate methods for key validation
        static string GetAdditiveCipherKey()
        {
            while (true)
            {
                Console.Write("Enter the shift value (1-25): ");
                if (int.TryParse(Console.ReadLine(), out int shift) && shift >= 1 && shift <= 25)
                {
                    return shift.ToString();
                }
                Console.WriteLine("Invalid input. Please enter a number between 1 and 25.");
            }
        }
        static string GetMultiplicativeCipherKey()
        {
            while (true)
            {
                Console.Write("Enter the multiplicative key (must be coprime with 26): ");
                if (int.TryParse(Console.ReadLine(), out int key) && IsCoprimeWith26(key))
                {
                    return key.ToString();
                }
                Console.WriteLine("Invalid input. Please enter a number that is coprime with 26.");
            }
        }
        static string GetAffineCipherKey()
        {
            while (true)
            {
                Console.Write("Enter additive and multiplicative keys (space-separated): ");
                string[] keys = Console.ReadLine().Split();
                if (keys.Length == 2 && int.TryParse(keys[0], out int additive) && int.TryParse(keys[1], out int multiplicative) && IsCoprimeWith26(multiplicative))
                {
                    return $"{additive} {multiplicative}";
                }
                Console.WriteLine("Invalid input. Please enter two numbers, where the second is coprime with 26.");
            }
        }
        static string GetAutoKeyCipherKey()
        {
            Console.Write("Enter the initial key value: ");
            return Console.ReadLine();
        }
        static string GetPlayfairCipherKey()
        {
            Console.Write("Enter the key word: ");
            return Console.ReadLine();
        }
        static string GetVigenereCipherKey()
        {
            Console.Write("Enter the key word: ");
            return Console.ReadLine();
        }
        static string GetHillCipherKey()
        {
            Console.Write("Enter the key matrix values (space-separated): ");
            return Console.ReadLine();
        }
        static string GetKeylessTranspositionCipherKey()
        {
            while (true)
            {
                Console.Write("Enter the number of columns: ");
                if (int.TryParse(Console.ReadLine(), out int columns) && columns > 0)
                {
                    return columns.ToString();
                }
                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }
        static string GetKeyedTranspositionCipherKey()
        {
            Console.Write("Enter the key word: ");
            return Console.ReadLine();
        }
        // Helping methods
        static bool IsCoprimeWith26(int number)
        {
            return GCD(number, 26) == 1;
        }
    }
}