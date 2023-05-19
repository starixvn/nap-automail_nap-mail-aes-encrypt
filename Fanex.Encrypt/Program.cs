using Fanex.Encrypt.Business;
using System;

namespace Fanex.Encrypt.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var plainText = "Folder=01-DropTOByLic&FileName=Weekly_Dropped_Turnover_by_Licensee_Report_From_2022_05_23_To_2022_05_29.xlsx&ReportId=04&Time=5/11/2023 3:33:30 pm&mailid=2451&mailaddress=charles.nguyen@starixsoft.com&Title=Auto Generated Report Administrator&Subject=Automail Report - Weekly Dropped Turnover by Licensee Report  - 2023/05/11";
            string cipherText = AesEncrypt.EncryptDataWithAes(plainText, out string keyBase64, out string vectorBase64);

            var text = AesEncrypt.DecryptDataWithAes(cipherText, keyBase64, vectorBase64);
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}