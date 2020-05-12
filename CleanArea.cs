using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanArea : MonoBehaviour
{

    Spawner _sp;

    public GameObject _decor;
    public Material m_Material;

    public GameObject _N1;
    public GameObject _N2;

    //public Text _zC;

    public int e_Deaths;

    private bool _played;

    public MenuSound _menuSound;
    public AudioManager _audM;

    // Start is called before the first frame update
    void Start()
    {
        _sp = GameObject.Find("GameManager").GetComponent<Spawner>();
        m_Material = GetComponent<Renderer>().material;
        e_Deaths = 0;

        _N1.SetActive(true);
        _N2.SetActive(false);

        _menuSound = FindObjectOfType<MenuSound>();
        _audM = FindObjectOfType<AudioManager>();

        //_zC.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (e_Deaths == _sp._totalValor)
        {
            _N1.SetActive(false);
            _N2.SetActive(true);
            //_zC.gameObject.SetActive(true);
            m_Material.color = Color.green;

            if (_played == false)
            {
                _audM._ZC.clip = _menuSound._clips[9];
                _audM._ZC.Play();
                _played = true;
            }

        }

    }
}
