using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    Transform my;

    public Rigidbody _rb;
    private int _acceleration;

    public int _lifepts;

    public Transform _muzzle;

    public GameObject _bullet;

    public Transform _spawnP;

    public Animator m_Animator;

    public MenuSound _menuSound;
    public AudioManager _audM;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        my = GetComponent<Transform>();

        _lifepts = 5; //NB de points de vie du joueur

        _menuSound = FindObjectOfType<MenuSound>();
        _audM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Moves();
        LookAtMouse();
        Shoot();
        Death();

        //Caméra suit le joueur sur l'axe X et Y
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10f);
    }

    public void Moves()
    {
        if (Input.GetKey(KeyCode.Z)) //Déplacement vers le haut
        {
            m_Animator.SetTrigger("WalkTop");
            Debug.Log("Vers Haut");
            _rb.velocity = new Vector3(_rb.velocity.x, 5, 0);
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
            m_Animator.SetTrigger("Idle");
        }

        if (Input.GetKey(KeyCode.S)) //Déplacement vers le bas
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("WalkDown");
            Debug.Log("Vers Bas");
            _rb.velocity = new Vector3(_rb.velocity.x, -5, 0);
        }

        if (Input.GetKey(KeyCode.D)) //Déplacement vers la droite
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("WalkRight");
            Debug.Log("Vers Droite");
            _rb.velocity = new Vector3(5, _rb.velocity.y, 0);
        }

        if (Input.GetKey(KeyCode.Q)) //Déplacement vers la gauche
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("WalkLeft");
            Debug.Log("Vers Gauche");
            _rb.velocity = new Vector3(-5, _rb.velocity.y, 0);
        }
    }

    public void LookAtMouse() //Orientation vers le curseur de la souris
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Shoot() //Instanciement des balles = Système de tir
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(_bullet, _muzzle.transform.position, transform.rotation) as GameObject;
        }
    }

    public void Death()
    {
        if (_lifepts <= 0)
        {
            //m_Animator.SetTrigger("Shake");
            transform.position = _spawnP.transform.position;
            _lifepts = 5;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "BulletEn")
        {
            _audM._dmg.clip = _menuSound._clips[0];
            _audM._dmg.Play();
            Debug.Log("Player Touched");
            _lifepts--;
            Debug.Log("Player Life = " + _lifepts);
        }
    }
}
