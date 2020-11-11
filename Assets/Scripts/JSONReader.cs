
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public static EmissionList emissionJson;

    void Start()
    {
        emissionJson = JsonUtility.FromJson<EmissionList>(jsonFile.text);
    }
}