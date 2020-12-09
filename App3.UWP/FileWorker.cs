using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using App3.Interface;
using Windows.Storage.Provider;
using Windows.Storage.Pickers;

using System.IO;


[assembly: Dependency(typeof(App3.UWP.FileWorker))]
namespace App3.UWP
{
    public class FileWorker : IFileWorker
    {
        public async Task<byte[]> OpenFile()
        {
            var openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".docx");

            StorageFile selectedFile = await openPicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                using (StreamReader reader = new StreamReader(await selectedFile.OpenStreamForReadAsync()))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        reader.BaseStream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
            return null;
        }

        public async Task SaveFileTxt(byte[] fileData)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Документ", new List<string>() { ".txt" });

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteBytesAsync(file, fileData);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
        public async Task SaveFileWord(byte[] fileData)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Документ", new List<string>() { ".docx" });

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteBytesAsync(file, fileData);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }

     
    }
}
