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
   public class BaseViewModels: INotifyPropertyChanged
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
        { string str= "бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!! " +
                "у ъящэячэц ъэюоык, едщ бдв саэацкшгнбяр гчеа кчфцшубп цу ьгщпя вщвсящ, эвэчрысй юяуъщнщхо шпуъликугбз " +
                "чъцшья с цощъвчщ ъфмес ю лгюлэ ёъяяр! с моыящш шпмоец щаярдш цяэубфъ аьгэотызуа дщ, щръ кй юцкъщчьуац уыхэцэ ясч" +
                " юбюяуяг ыовзсгюамщщ.внютвж тхыч эядкъябе цн юкъль, мэсццогл шяьфыоэьь ть эщсщжнашанэ ыюцен, уёюяыцчан мах гъъьуун шпмоыъй " +
                "ч яяьпщъхэтпык яущм бпйэае!чэьюмуд, оээ скфч саьбрвчёыа эядуцйт ъ уьгфщуяяёу фси а эацэтшцэч юпапёи, ьь уъубфмч ысь хффы ужц чьяцнааущ эгъщйаъф," +
                " ч п эиттпьк ярвчг гмубзньцы!щб ьшяо шачюрэсч FirstLineSoftware ц ешчтфщацдпбр шыыь, р ыоф ячцсвкрщве бттй а ядсецсцкюкх эшашёрэсуъ якжще увюгщр в# уфн ысвчюпжзцж!" +
                " чй ёюычъ бщххыибй еьюхечр п хкъмэншёцч юятщвфцшчщ с хчю ъэ ч аачсюсчыщачрняун в шъюьэжцясиьццч агфуо ацаьяычсцы .Net, чэбф ыуюбпьщо с чыдпяхбцйг щктрж!";
            DecryptoText = vig.Decrypt(str,"скорпион");
        }

        private async void Crypto()
        {
            throw new NotImplementedException();
        }

        public  string CryptoText
        {
            get { return сipher.CryptoText; }
            set
            {
                    сipher.CryptoText = value;
                    OnPropertyChanged(nameof(CryptoText));
                
            }
        }

        public  string DecryptoText
        {
            get { return сipher.DecryptoText; }
            set
            {
                    сipher.DecryptoText = value;
                    OnPropertyChanged(nameof(DecryptoText));
                
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

            string text = "sass";
            MemoryStream data = new MemoryStream();
            using (WordDocument document = new WordDocument())
            {
                document.EnsureMinimal();
                document.LastParagraph.AppendText(text);
                document.Save(data, FormatType.Docx);

            }

            await Xamarin.Forms.DependencyService.Get<IFileWorker>().SaveFileWord(data.ToArray());

        }

        public async void SaveFileTxt()
        {

            string text = "sass";
            MemoryStream data = new MemoryStream();
            using (WordDocument document = new WordDocument())
            {
                document.EnsureMinimal();
                document.LastParagraph.AppendText(text);
                document.Save(data, FormatType.Txt);


            }

            await Xamarin.Forms.DependencyService.Get<IFileWorker>().SaveFileTxt(data.ToArray());

        }
      


        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
