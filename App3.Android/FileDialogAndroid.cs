using Android;
using Android.Support.V4.App;
using Java.IO;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using App3.Interface;

[assembly: Dependency(typeof(FileDialogAndroid))]
class FileDialogAndroid : IFileWorker
{
    

    public async Task<byte[]> OpenFile()
    {
        try
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();
            if (fileData == null)
            {
                return new byte[0];
            }
            return fileData.DataArray;
           
        }
        catch (Exception ex)
        {
            return new byte[0];
        }
    }

    

    public async Task SaveFileTxt(byte[] fileData)
    {
          await Task.Run(() =>
            {
                string filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDocuments);
                
                string fullName = Path.Combine(filePath, "Документ.txt");
                if (System.IO.File.Exists(fullName))
                {
                    System.IO.File.Delete(fullName);
                }
                System.IO.File.WriteAllBytes(fullName, fileData);
            });
           
    }

    public async Task SaveFileWord(byte[] fileData)
    {
        await Task.Run(() =>
        {
            string filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDocuments);
            
            string fullName = Path.Combine(filePath, "Документ.docx");
            if (System.IO.File.Exists(fullName))
            {
                System.IO.File.Delete(fullName);
            }
            System.IO.File.WriteAllBytes(fullName, fileData);
        });
    }
}