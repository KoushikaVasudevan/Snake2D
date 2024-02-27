using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FoodController foodController;
    [SerializeField] private MassBurnerController massBurnerController;
    [SerializeField] private ShieldPowerupController sheildPowerupController;
    [SerializeField] private DoubleScoreController doubleScoreController;
    [SerializeField] private IncreaseSpeedPowerup increaseSpeedPowerup;

    [SerializeField] private float timeBetweenSpawns;

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
                objectToSpawn = Instantiate(foodController.gameObject, new Vector2(spawnPosition.x, spawnPosition.y), foodController.gameObject.transform.rotation);
            }
            else if (randomNumber == 1)
            {
                objectToSpawn = Instantiate(massBurnerController.gameObject, new Vector2(spawnPosition.x, spawnPosition.y), massBurnerController.gameObject.transform.rotation);
            }
            else if (randomNumber == 2)
            {
                objectToSpawn = Instantiate(sheildPowerupController.gameObject, new Vector2(spawnPosition.x, spawnPosition.y), sheildPowerupController.gameObject.transform.rotation);
            }
            else if (randomNumber == 3)
            {
                objectToSpawn = Instantiate(doubleScoreController.gameObject, new Vector2(spawnPosition.x, spawnPosition.y), doubleScoreController.gameObject.transform.rotation);
            }
            else if (randomNumber == 4)
            {
                objectToSpawn = Instantiate(increaseSpeedPowerup.gameObject, new Vector2(spawnPosition.x, spawnPosition.y), increaseSpeedPowerup.gameObject.transform.rotation);
            }

            Destroy(objectToSpawn, 10);

            StartCoroutine(RandomSpawner(timeBetweenSpawns));
        }
    }
}
