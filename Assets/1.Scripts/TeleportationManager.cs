using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] Vector3 IslandTPPos = new Vector3(464,99,438);
    [SerializeField] Vector3 AtherTPPos = new Vector3(-12,1,7992);
    [SerializeField] Vector3 MyceilaTPPos= new Vector3(7988,1,13);
    [SerializeField] Vector3 IrisTPPos = new Vector3(-2980,83,-33);

    /// <summary>
    /// Teleports the player to a specific map location based on the given map index.
    /// </summary>
    /// <param name="mapInt">
    /// The index of the map to teleport to:
    /// 0 = Island, 1 = Aether, 2 = Mycelia, 3 = Iris.
    /// </param>
    public void TeleportPlayer(int mapInt)
    {
        switch (mapInt)
        {
            case 0: // Island
                PlayerController.Instance.gameObject.transform.position = IslandTPPos;
                break;
            case 1: // Aether
                PlayerController.Instance.gameObject.transform.position = AtherTPPos;
                break;
            case 2:// Mycelia
                PlayerController.Instance.gameObject.transform.position = MyceilaTPPos;
                break;
            case 3: // Iris
                PlayerController.Instance.gameObject.transform.position = IrisTPPos;
                break;
            default:
                break;
        }
    }
}
