using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    CleanArea cA;

    public GameObject _player;

    public GameObject[] _bullet;

    public GameObject[] _muzzles;

    public Rigidbody _rb;

    public float _speed;

    public int _life;

    public bool _inR;

    public Animator m_Animator;

    //AudioClip[] _clips;
    //AudioSource _source;

    public MenuSound _menuSound;
    public AudioManager _audM;
    //public AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _menuSound = FindObjectOfType<MenuSound>();
        _audM = FindObjectOfType<AudioManager>();
        //_audioSource = Camera.main.GetComponent<AudioSource>();
        
        _speed = 2.0f;
        _player = GameObject.FindWithTag("Player");
        _life = 6;
        cA = GameObject.Find("Ground").GetComponent<CleanArea>();
        _rb = GetComponent<Rigidbody>();
        //_source = //GameObject.Find("AudioManager").GetComponent<AudioSource>();
        //_clips = GameObject.Find("AudioManager").GetComponent<AudioClip>()._clips;

        //_source.Play(_clips[7]);
        _audM._Spawn.clip = _menuSound._clips[5];
        _audM._Spawn.Play();

        StartCoroutine("ShootBehave1");
        StartCoroutine("AwayOfSpawn");

        //_audioSource.clip = _menuSound._clips[5];
        //_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        FarFromPlayer();
        Death();
    }

    public void FarFromPlayer()
    {
        if (_inR == true)
        {
            float step = -1 * _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
        }
    }

    public void Death()
    {
        if (_life <= 0)
        {
            _audM._despawn.clip = _menuSound._clips[1];
            _audM._despawn.Play();
            cA.e_Deaths++;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Bullet")
        {
            _life--;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player trop près");
            _inR = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player assez loin");
            _inR = false;
        }
    }

    IEnumerator ShootBehave1()
    {
        _audM._fireball.clip = _menuSound._clips[2];
        _audM._fireball.Play();
        Debug.Log("Shoot Coro Launched");
        yield return new WaitForSeconds(1);
        GameObject bullet1 = Instantiate(_bullet[0], _muzzles[0].transform.position, transform.rotation) as GameObject;
        GameObject bullet2 = Instantiate(_bullet[1], _muzzles[1].transform.position, transform.rotation) as GameObject;
        GameObject bullet3 = Instantiate(_bullet[2], _muzzles[2].transform.position, transform.rotation) as GameObject;
        GameObject bullet4 = Instantiate(_bullet[3], _muzzles[3].transform.position, transform.rotation) as GameObject;
        StartCoroutine("ShootBehave1");
    }

    IEnumerator AwayOfSpawn()
    {
        m_Animator.SetTrigger("Top");
        _rb.velocity = new Vector3(0, 3, 0);
        yield return new WaitForSeconds(2);
        StartCoroutine("Moves");
    }

    IEnumerator Moves()
    {
        m_Animator.ResetTrigger("Top");
        m_Animator.SetTrigger("Right");
        _rb.velocity = new Vector3(3, 0, 0);
        yield return new WaitForSeconds(2);
        m_Animator.ResetTrigger("Right");
        m_Animator.SetTrigger("Down");
        _rb.velocity = new Vector3(0, -3, 0);
        yield return new WaitForSeconds(2);
        m_Animator.ResetTrigger("Down");
        m_Animator.SetTrigger("Left");
        _rb.velocity = new Vector3(-3, 0, 0);
        yield return new WaitForSeconds(2);
        m_Animator.ResetTrigger("Left");
        m_Animator.SetTrigger("Top");
        _rb.velocity = new Vector3(0, 3, 0);
        StartCoroutine("Moves2");
    }

    IEnumerator Moves2()
    {
        m_Animator.ResetTrigger("Top");
        m_Animator.SetTrigger("Left");
        _rb.velocity = new Vector3(-3, 0, 0);
        yield return new WaitForSeconds(2);
        m_Animator.ResetTrigger("Left");
        m_Animator.SetTrigger("Top");
        _rb.velocity = new Vector3(0, 3, 0);
        yield return new WaitForSeconds(2);
        m_Animator.ResetTrigger("Top");
        _rb.velocity = new Vector3(3, 0, 0);
        yield return new WaitForSeconds(2);
        m_Animator.SetTrigger("Down");
        _rb.velocity = new Vector3(0, -3, 0);
        StartCoroutine("Moves");
    }

}
