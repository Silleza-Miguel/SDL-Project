using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.IO;
using System.Data.SqlClient;

namespace WpfApp4.Services
{
    public class FilePathService
    {
        private static string CurrentWorkingFolder = Environment.CurrentDirectory;
        
        private static string CurrentProjectFolder = Directory.GetParent(CurrentWorkingFolder).Parent.Parent.FullName;
        
        private static string? folderPath;
        
        private static int resourceIndex;

        public static string GetImagePathRelative(string filepath)
        {
            resourceIndex = filepath.IndexOf(@"\Resources\");
            folderPath = filepath.Substring(0, resourceIndex);

            filepath = filepath.Substring(resourceIndex, filepath.Length - resourceIndex);

            return filepath;
        }

        public static string GetVideoPathRelative(string filepath)
        {
            resourceIndex = filepath.IndexOf(@"\Resources\");
            folderPath = filepath.Substring(0, resourceIndex);

            resourceIndex += 1;
            filepath = filepath.Substring(resourceIndex, filepath.Length - resourceIndex);

            return filepath;
        }

        public static string GetImageNewPath(string filepath)
        {
            resourceIndex = filepath.IndexOf(@"\Resources\");
            folderPath = filepath.Substring(0, resourceIndex);

            filepath = $"{CurrentProjectFolder}{filepath.Substring(resourceIndex, filepath.Length - resourceIndex)}";
            
            return filepath;
        }

        public static string GetVideoNewPath(string filepath)
        {
            resourceIndex = filepath.IndexOf(@"\Resources\");
            folderPath = filepath.Substring(0, resourceIndex);

            resourceIndex += 1;
            filepath = $"{CurrentProjectFolder}{filepath.Substring(resourceIndex, filepath.Length - resourceIndex)}";
            
            return filepath;
        }

        public static bool CheckFilePath(string filepath)
        {
            resourceIndex = filepath.IndexOf(@"\Resources\");
            folderPath = filepath.Substring(0, resourceIndex);

            if(folderPath == CurrentProjectFolder)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
