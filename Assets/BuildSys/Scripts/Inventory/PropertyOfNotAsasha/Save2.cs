using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FileSystem
{
    [CreateAssetMenu(fileName = "GameFileHandler", menuName = "Not/Game Files Handler")]
    public class GameFileHandler : MonoBehaviour
    {
        [SerializeField] private List<GameFile> avaibleFiles;
        private const string encryptionCodeWord = "NotTheBestSaveSystem";

        /// <summary>
        /// Loads all avaible files
        /// </summary>
        /// 
        private void Start()
        {
            LoadAll(avaibleFiles);
            foreach (var file in avaibleFiles)
            {
                Save(file, false);
            }
        }
        public void LoadAll(List<GameFile> GivenFile)
        {
            foreach (GameFile file in GivenFile)
            {
                Load(file, false);
            }
        }

        /// <summary>
        /// Tries to load all data from the file
        /// </summary>
        public void Load(GameFile GivenFile, bool useDecryption)
        {
            if (GivenFile == null) { Debug.LogWarning("No files to load :("); return; }

            string dirPath = GivenFile.Directory;
            string filePath = GivenFile.GetFullPath();
            if (!Directory.Exists(dirPath)) { Directory.CreateDirectory(dirPath); }
            if (!File.Exists(filePath)) { File.Create(filePath); }
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new(filePath, FileMode.Open))
                {
                    using StreamReader reader = new(stream);
                    dataToLoad = reader.ReadToEnd();
                    reader.Close();
                }
                if (useDecryption) dataToLoad = DataEncryptDecrypt(dataToLoad);
                GivenFile.ProcessData(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + filePath + "\n" + e);
            }
        }
        /// <summary>
        /// Tries to save some data to the file
        /// </summary>
        public void Save(GameFile GivenFile, bool useEncryption)
        {
            string dirPath = GivenFile.Directory;
            string filePath = Path.Combine(Application.persistentDataPath, GivenFile.GetFullPath());
            try
            {
                // create the directory the file will be written to if it doesn't already exist
                if (!Directory.Exists(dirPath)) { Directory.CreateDirectory(dirPath); }
                if (!File.Exists(filePath)) { File.Create(filePath); }

                string dataToStore = GivenFile.GetData();

                // optionally encrypt the data
                if (useEncryption)
                {
                    dataToStore = DataEncryptDecrypt(dataToStore);
                }

                // write the serialized data to the file
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
                Debug.LogError("Error occured when trying to save data to file: " + filePath + "\n" + e);
            }
        }
        public string DataEncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
            }
            return modifiedData;
        }
    }
}