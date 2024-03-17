using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class ScratchMono_EncryptedVariableHolder : AbstractScratchMono_VariableHolderAsString
{
    [TextArea(0, 5)]
    public string m_variableStoredDescription = "";
    [TextArea(0, 5)]
    public string m_textToEncrypt;
    public string m_playerPrefGuid = "";

    private void Reset()
    {
        m_playerPrefGuid = Guid.NewGuid().ToString();
    }


    byte[] GenerateKey(string computerID)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(computerID));
        }
    }


    [ContextMenu("Encrypt Text In Inspector")]
    public void EncryptTextInInspector()
    {
        EncryptGivenText(m_textToEncrypt);
    }
    [ContextMenu("Encrypt Text In Inspector and delete")]
    public void EncryptTextInInspectorAndDelete()
    {
        EncryptGivenText(m_textToEncrypt);
        m_textToEncrypt = "";
    }
    public void EncryptGivenText(string text)
    {
        string path = GetPath();
        if (!File.Exists(path))
            File.WriteAllText(path,EncryptText( text));
        else File.WriteAllText(path, EncryptText(text));
    }
    public string GetEncryptedTextInMemory()
    {

        string path = GetPath();
        if (!File.Exists(path))
            File.WriteAllText(path, "");
        return DecryptText(File.ReadAllText(path));
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, m_playerPrefGuid + ".dat");
    }

    [ContextMenu("Display In Log Encrypted Text In Memory")]
    public void DisplayInLogEncryptedTextInMemory()
    {

       Debug.Log( GetVariableAsString());
    }

    // Encrypt text using AES
    string EncryptText(string text)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GenerateKey(SystemInfo.deviceUniqueIdentifier); ;
            aesAlg.IV = new byte[aesAlg.BlockSize / 8]; // You may change IV for better security

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Decrypt text using AES
    string DecryptText(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GenerateKey(SystemInfo.deviceUniqueIdentifier); ;
            aesAlg.IV = new byte[aesAlg.BlockSize / 8]; // You may change IV for better security

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public override string GetVariableAsString()
    {
        return GetEncryptedTextInMemory().Trim();
    }
}
