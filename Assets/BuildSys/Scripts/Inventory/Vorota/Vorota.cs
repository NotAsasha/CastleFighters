using UnityEngine;

public class Vorota : MonoBehaviour
{
    public int _randomNumber;
    private bool _ifYourAttack;

    public Animator _anim_p1;
    public Animator _anim_p2;


    public GameObject p1;
    public GameObject p2;



    private void Awake()
    {
        _anim_p1 = p1.GetComponent<Animator>();
        _anim_p2 = p2.GetComponent<Animator>();


        _randomNumber = UnityEngine.Random.Range(1, 3);
    }


    private void Update()
    {
        _anim_p1.SetInteger("p1", _randomNumber);
        _anim_p2.SetInteger("p1", _randomNumber);

        if (_randomNumber == 1)
        {
            _anim_p1.SetInteger("p1", 1);
            _anim_p2.SetInteger("p1", 2);

        }
        else
        {
            _anim_p1.SetInteger("p1", 2);
            _anim_p2.SetInteger("p1", 1);

        }


    }


}
