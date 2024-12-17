using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FileSystem
{
    [CreateAssetMenu(fileName = "GameFileHandler", menuName = "Not/Game Files Handler")]
    public class Save1 : MonoBehaviour
    {
        public List<GameFile> availableFiles;
        public List<string> availableFssiles;
        private const string encryptionCodeWord = "NotTheBestSaveSystem";

        /// <summary>
        /// Loads all available files.
        /// </summary>
        /// 
        private void Start()
        {
            LoadAll(availableFiles);
            foreach (var file in availableFiles)
            {
                Save(file, false);
            }
        }
        public void LoadAll(List<GameFile> filesToLoad)
        {
            if (filesToLoad == null || filesToLoad.Count == 0)
            {
                Debug.LogWarning("No files to load :(");
                return;
            }

            foreach (GameFile file in filesToLoad)
            {
                Load(file, false);
            }
        }

        /// <summary>
        /// Tries to load data from the given file.
        /// </summary>
        public void Load(GameFile fileToLoad, bool useDecryption)
        {
            if (fileToLoad == null)
            {
                Debug.LogWarning("File to load is null :(");
                return;
            }

            string dirPath = fileToLoad.Directory;
            string filePath = Path.Combine(Application.persistentDataPath + fileToLoad.GetFullPath());

            CreateDirectoryIfNotExist(dirPath);

            try
            {
                string dataToLoad;
                using (StreamReader reader = new(filePath))
                {
                    dataToLoad = reader.ReadToEnd();
                }

                if (useDecryption)
                {
                    dataToLoad = DataEncryptDecrypt(dataToLoad);
                }

                fileToLoad.ProcessData(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data from file: {filePath}\n{e}");
            }
        }

        /// <summary>
        /// Tries to save data to the given file.
        /// </summary>
        public void Save(GameFile fileToSave, bool useEncryption)
        {
            if (fileToSave == null)
            {
                Debug.LogWarning("File to save is null :(");
                return;
            }

            string dirPath = fileToSave.Directory;
            string filePath = Path.Combine(Application.persistentDataPath + fileToSave.GetFullPath());
            CreateDirectoryIfNotExist(dirPath);

            try
            {
                string dataToStore = fileToSave.GetData();

                if (useEncryption)
                {
                    dataToStore = DataEncryptDecrypt(dataToStore);
                }

                using FileStream stream = new(filePath, FileMode.Create);
                using (StreamWriter writer = new(stream))
                {
                    writer.Write(dataToStore);
                    writer.Close();
                }
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving data to file: {Application.persistentDataPath},{filePath}\n{e}");
            }
        }

        /// <summary>
        /// Encrypts or decrypts data using a simple XOR encryption.
        /// </summary>
        private string DataEncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
            }
            return modifiedData;
        }

        /// <summary>
        /// Checks existence of directory within the given path, creates if not exist.
        /// </summary>
        private void CreateDirectoryIfNotExist(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string fullPath = Path.Combine(Application.persistentDataPath + path);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
    }
}