using UnityEngine;

public class RandomSkinLongTall : MonoBehaviour
{
    public GameObject[] skins;

    void Start()
    {
        int index = Random.Range(0, skins.Length);

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(i == index);
        }
    }
}
