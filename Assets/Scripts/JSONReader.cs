
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public static EmissionList emissionJson;

    void Start()
    {
        Debug.Log(jsonFile.text);
        emissionJson = JsonUtility.FromJson<EmissionList>(jsonFile.text);
    }
}