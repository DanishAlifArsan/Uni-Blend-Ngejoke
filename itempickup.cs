using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itempickup : MonoBehaviour
{
    public Camera playerCamera;
    public Transform playerHand;
    private bool isItemInHand = false;

    void Start()
    {
        playerCamera = Camera.main;
        //playerHand = transform.Find("Hand"); // Ganti "Hand" dengan nama objek tangan pada karakter

        if (playerHand == null)
        {
            Debug.LogError("Hand object not found! Make sure the hand object is a child of the player.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Menggunakan tombol kiri mouse untuk pickup
        {
            if (!isItemInHand)
            {
                TryPickupItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f))
        {
            // Cek apakah objek yang terkena memiliki komponen "Item"
            Item item = hit.collider.GetComponent<Item>();
            
            if (item != null)
            {
                // Pindahkan item ke tangan dan atur parent menjadi tangan
                item.Pickup(playerHand);
                isItemInHand = true;
            }
        }
    }

    void DropItem()
    {
        // Cari item di tangan
        Item item = playerHand.GetComponentInChildren<Item>();

        if (item != null)
        {
            // Lepaskan item dari tangan
            item.Drop();
            isItemInHand = false;
        }
    }
}