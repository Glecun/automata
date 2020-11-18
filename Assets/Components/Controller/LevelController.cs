using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject backgroundGrid = null;
    [SerializeField] private GameObject tree = null;
    [SerializeField] private GameObject human = null;
    [SerializeField] private GameObject townHall = null;

    private void Start()
    {
        InstantiateUtils.Instantiate(backgroundGrid, new Vector3(0, 0), Quaternion.identity, transform);

        //Trees
        InstantiateUtils.Instantiate(tree, new Vector3(20, 3), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(tree, new Vector3(20, 4), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(tree, new Vector3(20, 5), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(tree, new Vector3(19, 4), Quaternion.identity, transform);

        //TownHall
        InstantiateUtils.Instantiate(townHall, new Vector3(8, 3), Quaternion.identity, transform);

        //Human
        InstantiateUtils.Instantiate(human, new Vector3(9.5f, 2), Quaternion.identity, transform);
    }
}