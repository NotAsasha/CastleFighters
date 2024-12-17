using UnityEngine;

namespace Assets.Build.Scripts.Inventory
{
    public class Slot : MonoBehaviour
    {
        private Inventory _inventory;
        public short i;

        private void Start()
        {
            _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        private void Update()
        {
            if (transform.childCount <= 0)
            {
                _inventory._isFull[i] = false;
            }
        }



    }
}
