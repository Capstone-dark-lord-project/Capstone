using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObjectsOnCollision : MonoBehaviour
{
    public List<MergeResult> mergeResults; // Assign prefabs in the Unity Editor
    private ObjectType currentType = ObjectType.None;

    void Start()
    {
        // Assign the current object's type based on its tag
        currentType = TagToObjectType(gameObject.tag);
    }

    void OnCollisionEnter(Collision collision)
    {
        MergeObjectsOnCollision otherObjectComponent = collision.gameObject.GetComponent<MergeObjectsOnCollision>();
        if (otherObjectComponent != null)
        {
            ObjectType otherType = otherObjectComponent.currentType;
            foreach (var result in mergeResults)
            {
                if ((result.Type1 == currentType && result.Type2 == otherType) || (result.Type2 == currentType && result.Type1 == otherType))
                {
                    Instantiate(result.ResultingObjectPrefab, transform.position, Quaternion.identity);
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    break; // Exit the loop once a matching merge result is found
                }
            }
        }
    }

    private ObjectType TagToObjectType(string tag)
    {
        return tag switch
        {
            "Wood" => ObjectType.Wood,
            "Food" => ObjectType.Food,
            "Scrap" => ObjectType.Scrap,
            "Junk" => ObjectType.Junk,
            "Plank" => ObjectType.Plank,
            "Fishing Rod" => ObjectType.FishingRod,
            "Weapon" => ObjectType.Weapon,
            "Toy"  => ObjectType.Toy,
            "HealthPack" => ObjectType.HealthPack,
            "CannedFood" => ObjectType.CannedFood,
            "PoisonFood" => ObjectType.PoisonFood,
            "Metal" => ObjectType.Metal,
            "Trap" => ObjectType.Trap,
            "Bomb" => ObjectType.Bomb
        };
    }

    [System.Serializable]
    public struct MergeResult
    {
        public ObjectType Type1;
        public ObjectType Type2;
        public GameObject ResultingObjectPrefab; // Assign the prefab for the resulting object in Unity Editor
    }
}

public enum ObjectType
{
    Wood,
    Food,
    Scrap,
    Junk,
    Plank, // Result of A + A
    FishingRod, // Result of A + B
    Weapon, // Result of A + C
    Toy, // Result of A + D
    HealthPack, // Result of B + B
    CannedFood, // Result of B + C
    PoisonFood, // Result of B + D
    Metal, // Result of C + C
    Trap, // Result of C + D
    Bomb, // Result of D + D
    None
}