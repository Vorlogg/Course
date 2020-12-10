using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace App3.ViewModels
{
    public class VigCipher
    {

        private int keyIndex;
        public int temp = 0;

        public string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public string Encrypt(string text, string key)
        {
          
            return Encryptor(text, key, true); 
        }

        public string Decrypt(string text, string key)
        {
            
            return Encryptor(text, key, false);
        }

        private string Encryptor(string text, string key, bool encrypt)
        {
            int k;
            string result = "";
            int alph = alphabet.Length;
            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.IndexOf(char.ToLower(text[i])) > -1)
                {
                    if (temp == 0)
                    {
                        keyIndex = 0;
                    }
                    else
                    {

                       keyIndex = (int)(temp % (key.Length));
                    }
                    if (encrypt)
                    {
                        k = 1;
                    }
                    else
                    {
                        k = -1;
                    }
                    int codeIndex = alphabet.IndexOf(key[keyIndex]);
                    int letterIndex = alphabet.IndexOf(text[i]);
                    int resultIndex = (alph + letterIndex + (k * codeIndex)) % alph;
                    result += alphabet[resultIndex];
                    temp++;
                }
                else
                {
                    result += text[i];
                }
            }
            temp = 0;
            return result;
        }
    }
}


