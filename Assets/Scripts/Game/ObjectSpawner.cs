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
        PlayerController.OnQPress += DeleteSpawned;
    }

    private void OnDestroy()
    {
        Teleporter.OnTeleportFinish -= SpawnObject;
        PlayerController.OnSpacePress -= ChangeColors;
        PlayerController.OnQPress -= DeleteSpawned;
    }

    private void SpawnObject(Vector3 position, Vector3 normal)
    {
        GameObject go = Instantiate(SpawnedObject, position, Quaternion.FromToRotation(Vector3.up, normal));
        go.transform.parent = transform;
        SetRandomColor(go);
        go.AddComponent<SpawnedObject>().OnDelete += RemoveFromList;
        _Gos.Add(go);
    }

    private void ChangeColors()
    {
        _Gos.RemoveAll(go => go == null);
        foreach (GameObject go in _Gos)
        {
            if (go != null)
            {
                SetRandomColor(go);
            }
        }
    }

    private void SetRandomColor(GameObject go)
    {
        Color randColor = new Color(Random.value, Random.value, Random.value);
        // Set random color to spawned object
        go.GetComponentInChildren<Renderer>().material.SetColor("_Color", randColor);
    }

    private void DeleteSpawned()
    {
        foreach (GameObject go in _Gos)
        {
            Destroy(go);
        }
        _Gos.Clear();
    }

    private void RemoveFromList(GameObject go)
    {
        _Gos.Remove(go);
        go.GetComponent<SpawnedObject>().OnDelete -= RemoveFromList;
    }
}
