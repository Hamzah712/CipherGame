using CipherGame;
using CipherGameTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CipherGameTest
{
    public class CryptographyToolTests
    {
        [Fact]
        public void TestEncryptAdditiveCipher()
        {
            // Arrange
            string text = "HELLO";
            string key = "3";
            string expectedEncryptedText = "KHOOR";

            // Act
            string encryptedText = CryptographyTool.EncryptAdditiveCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptAdditiveCipher()
        {
            // Arrange
            string encryptedText = "KHOOR";
            string key = "3";
            string expectedDecryptedText = "HELLO";

            // Act
            string decryptedText = CryptographyTool.DecryptAdditiveCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptMultiplicativeCipher()
        {
            // Arrange
            string text = "GEEKSFORGEEKS";
            string key = "7";
            string expectedEncryptedText = "QCCSWJUPQCCSW";

            // Act
            string encryptedText = CryptographyTool.EncryptMultiplicativeCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptMultiplicativeCipher()
        {
            // Arrange
            string encryptedText = "QCCSWJUPQCCSW";
            string key = "7";
            string expectedDecryptedText = "GEEKSFORGEEKS";

            // Act
            string decryptedText = CryptographyTool.DecryptMultiplicativeCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptAffineCipher()
        {
            // Arrange
            string text = "AFFINE CIPHER";
            string key = "20 17";
            string expectedEncryptedText = "UBBAHK CAPJKX";

            // Act
            string encryptedText = CryptographyTool.EncryptAffineCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptAffineCipher()
        {
            // Arrange
            string encryptedText = "UBBAHK CAPJKX";
            string key = "20 17";
            string expectedDecryptedText = "AFFINE CIPHER";

            // Act
            string decryptedText = CryptographyTool.DecryptAffineCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptAutoKeyCipher()
        {
            // Arrange
            string text = "HELLO";
            string key = "n";
            string expectedEncryptedText = "ULPWZ";

            // Act
            string encryptedText = CryptographyTool.EncryptAutoKeyCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptAutoKeyCipher()
        {
            // Arrange
            string encryptedText = "ULPWZ";
            string key = "n";
            string expectedDecryptedText = "HELLO";

            // Act
            string decryptedText = CryptographyTool.DecryptAutoKeyCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptPlayfairCipher()
        {
            // Arrange
            string text = "instruments";
            string key = "Monarchy";
            string expectedEncryptedText = "gatlmzclrqtx";

            // Act
            string encryptedText = CryptographyTool.EncryptPlayfairCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptPlayfairCipher()
        {
            // Arrange
            string encryptedText = "gatlmzclrqtx";
            string key = "Monarchy";
            string expectedDecryptedText = "instrumentsz";

            // Act
            string decryptedText = CryptographyTool.DecryptPlayfairCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptVigenereCipher()
        {
            // Arrange
            string text = "HELLO";
            string key = "KEY";
            string expectedEncryptedText = "RIJVS";

            // Act
            string encryptedText = CryptographyTool.EncryptVigenereCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptVigenereCipher()
        {
            // Arrange
            string encryptedText = "RIJVS";
            string key = "KEY";
            string expectedDecryptedText = "HELLO";

            // Act
            string decryptedText = CryptographyTool.DecryptVigenereCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptHillCipher()
        {
            // Arrange
            string text = "HELLO World";
            string key = "G Y B N Q K U R P"; 
            string expectedEncryptedText = "TFJIPIJSGVNQ";

            // Act
            string encryptedText = CryptographyTool.EncryptHillCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptHillCipher()
        {
            // Arrange
            string encryptedText = "TFJIPIJSGVNQ";
            string key = "G Y B N Q K U R P";
            string expectedDecryptedText = "HELLOWORLDXX";

            // Act
            string decryptedText = CryptographyTool.DecryptHillCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptKeylessTranspositionCipher()
        {
            // Arrange
            string text = "HELLO WORLD";
            string expectedEncryptedText = "HLODEORXLWLX";

            // Act
            string encryptedText = CryptographyTool.EncryptKeylessTranspositionCipher(text);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptKeylessTranspositionCipher()
        {
            // Arrange
            string encryptedText = "SUTERYCIX";
            string expectedDecryptedText = "SECURITY";

            // Act
            string decryptedText = CryptographyTool.DecryptKeylessTranspositionCipher(encryptedText);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }

        [Fact]
        public void TestEncryptKeyedTranspositionCipher()
        {
            // Arrange
            string text = "HELLOWORLD";
            string key = "Monarch";
            string expectedEncryptedText = "LXWXOXHROXELLD";

            // Act
            string encryptedText = CryptographyTool.EncryptKeyedTranspositionCipher(text, key);

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }

        [Fact]
        public void TestDecryptKeyedTranspositionCipher()
        {
            // Arrange
            string encryptedText = "LXWXOXHROXELLD";
            string key = "Monarch";
            string expectedDecryptedText = "HELLOWORLD";

            // Act
            string decryptedText = CryptographyTool.DecryptKeyedTranspositionCipher(encryptedText, key);

            // Assert
            Assert.Equal(expectedDecryptedText, decryptedText);
        }
    }
}