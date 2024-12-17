using System;
using UnityEngine;

namespace Assets.Build.Castle
{
    public class Castle : MonoBehaviour
    {

        private string _castleName = "Default Name";
        public GameObject PointParent;
        public GameObject BarParent;
        public string _castleIconPath;
        [SerializeField] private int MaxCharactersNumber = 15;




        public string CastleName
        {

            get => _castleName;
            set
            {
                if (value.Length < 1)
                {
                    throw new Exception("Not Enough Characters");
                }
                if (value.Length > MaxCharactersNumber)
                {
                    throw new Exception("Too Much Characters");
                }
                _castleName = value;
            }
        }
        public string CastleIcon
        {
            get => _castleIconPath;
            set => _castleIconPath = value;
        }

    }
}
