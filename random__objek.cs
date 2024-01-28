using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Tambahkan ini untuk menggunakan metode OrderBy

public class random__objek : MonoBehaviour
{
    public GameObject[] paperToPlace; // Objek yang akan ditempatkan
    public Transform[] paperspawnPositions;  // Posisi spawn yang telah ditentukan
    public GameObject[] humanToPlace; // Objek yang akan ditempatkan
    public Transform humanspawnPositions;  // Posisi spawn yang telah ditentukan

    void Start()
    {
        PlacepaperRandomly();
        PlacehumanRandomly();
    }

    void PlacepaperRandomly()
    {
        if (paperToPlace.Length == 0 || paperspawnPositions.Length == 0)
        {
            Debug.LogError("Harap atur objek dan posisi spawn terlebih dahulu!");
            return;
        }

        // Mengacak urutan posisi spawn
        System.Random random = new System.Random();
        paperspawnPositions = paperspawnPositions.OrderBy(pos => random.Next()).ToArray();

        // Menempatkan objek ke posisi spawn yang telah diacak
        for (int i = 0; i < Mathf.Min(paperToPlace.Length, paperspawnPositions.Length); i++)
        {
            Instantiate(paperToPlace[i], paperspawnPositions[i].position, Quaternion.identity);
        }
    }

    void PlacehumanRandomly()
    {
        if (humanToPlace.Length == 0)
        {
            Debug.LogError("Harap atur objek dan posisi spawn terlebih dahulu!");
            return;
        }

         // Mengacak urutan objek
        System.Random random = new System.Random();
        humanToPlace = humanToPlace.OrderBy(obj => random.Next()).ToArray();
        
        Instantiate(humanToPlace[0], humanspawnPositions.position, Quaternion.identity);
        
    }
}