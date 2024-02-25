using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitSelector : MonoBehaviour
{
    public static FruitSelector instance;

    public GameObject[] Fruits;
    public GameObject[] NoPhysicsFruits;
    // This index is no longer used since we have a specific list of indices
    // public int HighestStartingIndex = 3;

    [SerializeField] private Image _nextFruitImage;
    [SerializeField] private Sprite[] _fruitSprites;

    public GameObject NextFruit { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PickNextFruit();
    }

    public GameObject PickRandomFruitForThrow()
    {

        Debug.Log("NoPhysicsFruits Length: " + NoPhysicsFruits.Length);
        Debug.Log("Fruits Length: " + Fruits.Length);

        // The valid indices for picking fruits, assuming 11 is the index for the flame
        int[] validIndices = new int[] { 0, 1, 2, 3, 11 };
        int randomChoiceIndex = Random.Range(0, validIndices.Length);
        int randomChoice = validIndices[randomChoiceIndex];

        // Debug log to check what is being picked
        Debug.Log("Picked index: " + randomChoice);

        // Map the randomChoice to the actual index in NoPhysicsFruits
        int actualIndex = (randomChoice == 11) ? 4 : randomChoice; // Assuming the flame is at index 4 in NoPhysicsFruits

        // Now use actualIndex to access the NoPhysicsFruits array
        GameObject chosenPrefab = NoPhysicsFruits[actualIndex];

        if (chosenPrefab != null)
        {
            Debug.Log("Chosen prefab: " + chosenPrefab.name);
            return chosenPrefab;
        }

        Debug.LogError("No prefab found for the chosen index.");
        return null;
    }



    public void PickNextFruit()
    {

        //  _nextFruitImage is not null
        if (_nextFruitImage == null)
        {
            Debug.LogError("NextFruitImage is not assigned in the inspector");
            return;
        }

        // Ensure that the _fruitSprites array is not empty and has the right size
        if (_fruitSprites == null || _fruitSprites.Length == 0)
        {
            Debug.LogError("FruitSprites array is not properly assigned in the inspector");
            return;
        }

        // The valid indices for picking fruits, assuming 11 is the index for the flame
        int[] validIndices = new int[] { 0, 1, 2, 3, 11 };
        int randomChoice = validIndices[Random.Range(0, validIndices.Length)];

        // If a flame is selected
        if (randomChoice == 11)
        {
            NextFruit = Fruits[Fruits.Length - 1];
            _nextFruitImage.sprite = _fruitSprites[11]; 
        }
        else
        {
            NextFruit = Fruits[randomChoice];
            _nextFruitImage.sprite = _fruitSprites[randomChoice];
        }
    }
}
