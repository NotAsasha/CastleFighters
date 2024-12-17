using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;

namespace FileSystem
{
    [CreateAssetMenu(fileName = "GameFile", menuName = "Not/GameFile")]
    public class GameFile : ScriptableObject
    {
        // Displayed name of a file
        [SerializeField] private string _fileName = "Default";
        // File directory. Application.persistentDataPath is automatically added before it.
        [SerializeField] private string _directory = "/Default/";
        [SerializeField] private string _fileExtention = ".NotA";

        public string FileName => _fileName;
        public string Directory => _directory;
        public string FileExtention => _fileExtention;
        /// <summary>
        /// Returns full file path (WITHOUT Application.persistentDataPath!)  
        /// </summary>
        public string GetFullPath()
        {
            string fullPath = Path.Combine(Directory, FileName + FileExtention);
            return fullPath;
        }
        public void ProcessData(string inputJSON)
        {
            Debug.Log(inputJSON);
        }
        public string GetData()
        {
            return "TESTdddd";
        }
    }
}
