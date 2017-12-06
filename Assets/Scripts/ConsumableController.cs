using UnityEngine;
using System.Collections;

public class ConsumableController : MonoBehaviour {
    CharacterStats stats;

	// Use this for initialization
	void Start () {
        stats = GetComponent<Player>().characterStats;
	}

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
            itemToSpawn.GetComponent<IConsumable>().Consume(stats);
    }
	
}
