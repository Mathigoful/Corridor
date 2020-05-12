using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{

    Player p1;

    CleanArea cA;

    public GameObject _player;

    public float _speed;

    public int _life;
    //public float _range;

    public bool _inR;
    public bool _doing;

    public Animator m_Animator;

    public MenuSound _menuSound;
    public AudioManager _audM;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 2.0f;
        _life = 3;
        _player = GameObject.FindWithTag("Player");
        p1 = GameObject.Find("Player").GetComponent<Player>();
        cA = GameObject.Find("Ground").GetComponent<CleanArea>();

        _menuSound = FindObjectOfType<MenuSound>();
        _audM = FindObjectOfType<AudioManager>();

        _audM._Spawn.clip = _menuSound._clips[5];
        _audM._Spawn.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GoToPlayer();
        SlapPlayer();
        Death();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Bullet") //Lorsqu'une balle entre en contact avec l'ennemi
        {
            Debug.Log("Bullet touched");
            _life--;
            Debug.Log("Vies restantes = " + _life);
        }
    }

    void OnTriggerStay(Collider other) //Lorsque le joueur est à portée de l'ennemi
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player in range");
            _inR = true;
        }
    }

    void OnTriggerExit(Collider other) //Lorsque le joueur n'est plus à portée de l'ennemi
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player hors range");
            _inR = false;
        }
    }

    public void GoToPlayer() //Se diriger vers le joueur tant qu'il n'est pas à portée
    {
        if (_inR == false)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
        }

        if (_player.transform.position.y > 1)
        {
            m_Animator.SetTrigger("Top");
        }
        else m_Animator.ResetTrigger("Top");

        if (_player.transform.position.y < 1)
        {
            m_Animator.SetTrigger("Down");
        }
        else m_Animator.ResetTrigger("Down");
    }

    public void SlapPlayer() //Taper le joueur quand il est à portée
    {
        if (_inR == true && _doing == false)
        {
            StartCoroutine("ArmPlayer");
        }
    }

    public void Death() //Détruire l'ennemi quand il n'a plus de vie
    {
        if (_life <= 0)
        {
            _audM._despawn.clip = _menuSound._clips[1];
            _audM._despawn.Play();
            cA.e_Deaths++;
            Destroy(gameObject);
        }
    }

    IEnumerator ArmPlayer() //Faire des dégâts toute les 3 secondes tant que l'ennemi est à portée du joueur
    {
        _doing = true;
        _audM._Torch.clip = _menuSound._clips[8];
        _audM._Torch.Play();
        yield return new WaitForSeconds(2);
        p1._lifepts--;
        Debug.Log("J1 Lifes = " + p1._lifepts);
        _doing = false;
    }
}
