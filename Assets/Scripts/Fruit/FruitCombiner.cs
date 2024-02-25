using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCombiner : MonoBehaviour
{
    private int _layerIndex;

    private FruitInfo _info;

    private void Awake()
    {
        _info = GetComponent<FruitInfo>();
        _layerIndex = gameObject.layer;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is a flame
        if (collision.gameObject.CompareTag("flame")) // Make sure your flame has the tag "Flame"
        {
            // Destroy the fruit it collides with (assuming this script is attached to a fruit)
            Destroy(gameObject);
            // Also, destroy the flame
            Destroy(collision.gameObject);
            return; // Exit early so no further collision handling is done
        }

        // Handle collisions between fruits
        if (collision.gameObject.layer == _layerIndex)
        {
            FruitInfo info = collision.gameObject.GetComponent<FruitInfo>();
            if (info != null && _info != null) // Ensure both FruitInfo components are not null
            {
                if (info.FruitIndex == _info.FruitIndex)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();

                    if (thisID > otherID)
                    {
                        GameManager.instance.IncreaseScore(_info.PointsWhenAnnihilated);

                        if (_info.FruitIndex != FruitSelector.instance.Fruits.Length - 1) // Check if it's not the flame
                        {
                            Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                            GameObject go = Instantiate(SpawnCombinedFruit(_info.FruitIndex), GameManager.instance.transform);
                            go.transform.position = middlePosition;

                            ColliderInformer informer = go.GetComponent<ColliderInformer>();
                            if (informer != null)
                            {
                                informer.WasCombinedIn = true;
                            }
                        }

                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private GameObject SpawnCombinedFruit(int index)
    {
        if (index + 1 < FruitSelector.instance.Fruits.Length - 1) 
        {
            GameObject go = FruitSelector.instance.Fruits[index + 1];
            return go;
        }

        return null; 
    }

}
