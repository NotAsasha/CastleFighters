using UnityEngine;

namespace Assets.Build.Scripts.Inventory
{
    public class Pickup : MonoBehaviour
    {
        private Inventory _inventory;
        public GameObject _slotButton;

        private void Start()
        {
            _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                for (int i = 0; i < _inventory._slots.Length; i++)
                {
                    if (_inventory._isFull[i] == false)
                    {
                        _inventory._isFull[i] = true;
                        Instantiate(_slotButton, _inventory._slots[i].transform);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }


    }
}
