using App3.Models;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using App3.View;
using System.IO;
using System;
using App3.Interface;
using System.Text;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using App3.ViewModels;

namespace App3.ViewModels
{
    public class BaseViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CommandOpenFileTxt { protected set; get; }
        public ICommand CommandOpenFileWord { protected set; get; }
        public ICommand CommandSaveFileTxt { protected set; get; }
        public ICommand CommandSaveFileWord { protected set; get; }
        public ICommand CommandCrypto { protected set; get; }
        public ICommand CommandDecrypto { protected set; get; }

        private Сipher сipher;

        private VigCipher vig;
        public string CryptoText
        {
            get { return сipher.CryptoText; }
            set
            {
               
                    сipher.CryptoText = value;
                    OnPropertyChanged(nameof(CryptoText));
               

            }
        }

        public string DecryptoText
        {
            get { return сipher.DecryptoText; }
            set
            {

                сipher.DecryptoText = value;
                OnPropertyChanged(nameof(DecryptoText));

            }
        }
        public string Key
        {
            get { return сipher.Key; }
            set
            {
                
                    сipher.Key = value;
                    OnPropertyChanged(nameof(Key));
               

            }
        }


        public BaseViewModels()
        {
            vig = new VigCipher();
            сipher = new Сipher();
            CommandOpenFileTxt = new Command(OpenFileTxt);
            CommandOpenFileWord = new Command(OpenFileWord);
            CommandSaveFileTxt = new Command(SaveFileTxt);
            CommandSaveFileWord = new Command(SaveFileWord);
            CommandCrypto = new Command(Crypto);
            CommandDecrypto = new Command(Decrypto);


        }

        private void Decrypto()
        {
            if (CryptoText != null && CryptoText != "")
            {
                if (Key != null && Key != "")
                {
                    DecryptoText = vig.Decrypt(CryptoText, Key);

                }
                else
                {
                    DecryptoText = "Ошибка вы не ввели ключ ";
                }

            }
            else
            {
                DecryptoText = "Ошибка вы не ввели текст ";

            }
        }

        private void Crypto()
        {
            if (CryptoText != null && CryptoText !="")
            {
                if (Key != null && Key !="")
                {
                    DecryptoText = vig.Encrypt(CryptoText, Key);

                }
                else
                {
                    DecryptoText = "Ошибка вы не ввели ключ ";
                }

            }
            else
            {
                DecryptoText = "Ошибка вы не ввели текст ";

            }
        }

       
        public async void OpenFileTxt()
        {

            byte[] data = await Xamarin.Forms.DependencyService.Get<IFileWorker>().OpenFile();


            using (WordDocument document = new WordDocument(new MemoryStream(data), FormatType.Txt))
            {
                CryptoText = document.GetText().Substring(61);
            }


        }
        public async void OpenFileWord()
        {

            byte[] data = await Xamarin.Forms.DependencyService.Get<IFileWorker>().OpenFile();


            using (WordDocument document = new WordDocument(new MemoryStream(data), FormatType.Automatic))
            {
                CryptoText = document.GetText().Substring(61);
            }


        }

        public async void SaveFileWord()
        {
            try
            {
                string text = DecryptoText;
                MemoryStream data = new MemoryStream();
                using (WordDocument document = new WordDocument())
                {
                    document.EnsureMinimal();
                    document.LastParagraph.AppendText(text);
                    document.Save(data, FormatType.Docx);

                }

                await Xamarin.Forms.DependencyService.Get<IFileWorker>().SaveFileWord(data.ToArray());
                DecryptoText = "Файл успешно сохранен";
            }
            catch (Exception)
            {

                DecryptoText = "Не удалось сохрать файл";

            }

        }

        public async void SaveFileTxt()
        {
            try
            {
                string text = DecryptoText;
                byte[] data = null;
                data = Encoding.UTF8.GetBytes(text);
                await Xamarin.Forms.DependencyService.Get<IFileWorker>().SaveFileTxt(data);

                DecryptoText = "Файл успешно сохранен";
            }
            catch (Exception)
            {

                DecryptoText = "Не удалось сохрать файл";

            }
           

        }



        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
