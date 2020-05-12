using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] _enemies;

    public Transform[] _spawns;

    public List<GameObject> enemies;

    public int _totalSpawned;
    public int _totalValor;
    private int _random;
    private int _rSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        _totalSpawned = 0;
        StartCoroutine("Aleatoire");
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Aleatoire()
    {
        _random = Random.Range(0, 2); //Fait un random pour sélectionner Ennemi1 ou Ennemi2
        _rSpawn = Random.Range(0, 3); //Fait un random pour sélectionner un spawner sur les 4
        yield return new WaitForSeconds(1);
        StartCoroutine("Aleatoire");
    }

    IEnumerator SpawnEnemies()
    {
        if (_totalSpawned < _totalValor) //Tant que le nb d'ennemis ayant spawn n'est pas égal au nb d'ennemis de la vague, continue de spawn des ennemis
        {
            Instantiate(_enemies[_random], _spawns[_rSpawn]); //Fait spawn l'ennemi choisi au random sur le spawner choisit de la même façon
            _totalSpawned++;
            yield return new WaitForSeconds(3);
            StartCoroutine("SpawnEnemies");
        }
    }
}
