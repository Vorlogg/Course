using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Interface
{
   public interface IFileWorker
    {
        Task<byte[]> OpenFile();
        Task SaveFileTxt(byte[] fileData);
        Task SaveFileWord(byte[] fileData);

    }
}
