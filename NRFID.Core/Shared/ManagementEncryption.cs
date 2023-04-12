using BSMediator.Core.Models;
using Newtonsoft.Json;
using System;
using System.Text;
namespace NFC.Core.Shared
{
    
    public static class ManagementEncryption
    {
        private readonly static string encryptionKey = "1111111111111111";

        #region Encrypt and Decrypt
        public static string EncryptPassword(EncriptedModel encriptedModel)
        {
            var json = JsonConvert.SerializeObject(encriptedModel);
            var encryptedPassword = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(encryptedPassword);
        }
        public static EncriptedModel DecryptPassword(string encryptedPassword)
        {
            
            var baasEncodeBytes = Convert.FromBase64String(encryptedPassword);
            var result = Encoding.UTF8.GetString(baasEncodeBytes);
            EncriptedModel encriptedModel = JsonConvert.DeserializeObject<EncriptedModel>(result);
            return encriptedModel;
        }
        #endregion

    }
}
