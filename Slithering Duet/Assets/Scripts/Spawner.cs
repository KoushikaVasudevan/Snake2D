using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Food;
    public GameObject MassBurner;
    public GameObject SheildPowerup;
    public GameObject DoubleScorePowerup;
    public GameObject IncreaseSpeedPowerup;

    public float timeBetweenSpawns;

    public GameObject Snake1;
    public GameObject Snake2;

    private void Start()
    {
        StartCoroutine(RandomSpawner(timeBetweenSpawns));
    }

    private void Update()
    {
    }

    private IEnumerator RandomSpawner(float timeBetweenSpawns)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);


        if ((Snake1 == null) || (Snake2 == null))
        {
            yield break;
        }

        else
        {
            Vector2 spawnPosition;

            spawnPosition.x = Random.Range(-30, 30);
            spawnPosition.y = Random.Range(-19, 19);

            int randomNumber = Random.Range(0, 5);

            GameObject objectToSpawn = null;

            if (randomNumber == 0)
            {
                objectToSpawn = Instantiate(Food, new Vector2(spawnPosition.x, spawnPosition.y), Food.transform.rotation);
            }
            else if (randomNumber == 1)
            {
                objectToSpawn = Instantiate(MassBurner, new Vector2(spawnPosition.x, spawnPosition.y), MassBurner.transform.rotation);
            }
            else if (randomNumber == 2)
            {
                objectToSpawn = Instantiate(SheildPowerup, new Vector2(spawnPosition.x, spawnPosition.y), SheildPowerup.transform.rotation);
            }
            else if (randomNumber == 3)
            {
                objectToSpawn = Instantiate(DoubleScorePowerup, new Vector2(spawnPosition.x, spawnPosition.y), DoubleScorePowerup.transform.rotation);
            }
            else if (randomNumber == 4)
            {
                objectToSpawn = Instantiate(IncreaseSpeedPowerup, new Vector2(spawnPosition.x, spawnPosition.y), IncreaseSpeedPowerup.transform.rotation);
            }

            Destroy(objectToSpawn, 10);

            StartCoroutine(RandomSpawner(timeBetweenSpawns));
        }
    }
}
