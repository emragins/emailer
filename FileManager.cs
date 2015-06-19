using System;
using System.IO;

namespace EmailTestApp
{
    public class FileManager
    {
        public string CreateTestFile()
        {
            string name = Guid.NewGuid() + ".txt";
           FileStream stream =  File.Create(name);
            MyLogger.Info("File created: " + name);

            //relinquish our access to the stream!
            stream.Dispose();
            return name;
        }

        public void DeleteFile(string name)
        {
            if (File.Exists(name))
            {
                File.Delete(name);
                MyLogger.Info("Deleted file: " + name);

            }
            else
            {
                MyLogger.Error("File not found: " + name, null);
            }
        }

        //public void ArchiveParsedFile(string filePath)
        //{
        //    try
        //    {
        //        //archive file
        //        if (File.Exists(filePath))
        //        {
        //            string archiveFolder = Path.Combine(Path.GetDirectoryName(filePath), ARCHIVE);
        //            if (!Directory.Exists(archiveFolder))
        //            {
        //                System.IO.Directory.CreateDirectory(archiveFolder);
        //            }
        //            string moveToPath = Path.Combine(archiveFolder, System.IO.Path.GetFileName(filePath) + string.Format(".{0}", DateTime.Now.ToString("hhmmss")));

        //            File.Move(filePath, moveToPath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MyLogger.Error(null, string.Format("Error archiving file: {0}", filePath), ex);
        //    }
        //}
    }
}