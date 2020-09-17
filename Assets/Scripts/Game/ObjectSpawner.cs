using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public PlayerFPSController PlayerController;
    public PlayerTeleporter Teleporter;
    public GameObject SpawnedObject;
    private List<GameObject> _Gos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Teleporter = Teleporter ? Teleporter : FindObjectOfType<PlayerTeleporter>();
        PlayerController = PlayerController ? PlayerController : FindObjectOfType<PlayerFPSController>();
        Teleporter.OnTeleportFinish += SpawnObject;
        PlayerController.OnSpacePress += ChangeColors;
    }

    private void OnDestroy()
    {
        Teleporter.OnTeleportFinish -= SpawnObject;
        PlayerController.OnSpacePress -= ChangeColors;
    }

    private void SpawnObject(Vector3 position)
    {
        GameObject go = Instantiate(SpawnedObject, position, Quaternion.identity);
        SetRandomColor(go);
        _Gos.Add(go);
    }

    private void ChangeColors()
    {
        foreach (GameObject go in _Gos)
        {
            SetRandomColor(go);
        }
    }

    private void SetRandomColor(GameObject go)
    {
        Color randColor = new Color(Random.value, Random.value, Random.value);
        // Set random color to spawned object
        go.GetComponentInChildren<Renderer>().material.SetColor("_Color", randColor);
    }

}
