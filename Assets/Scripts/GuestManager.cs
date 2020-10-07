﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GameObject GuestPrefab; //guest gameobject to be instantiated

    public float EntranceRate = 0.5f; //the rate at which guests will enter

    private List<Guest> _guest = new List<Guest>(); //list of guests
    private List<Destination> _destinations = new List<Destination>(); //list of destinations
    private List<Guest> _exitedGuests = new List<Guest>(); //guests that will exit

    private float _lastEntrance = 0; //time since last entrant
    private int _occupancyLimit = 0; //occupancy limit maximum

    // Start is called before the first frame update
    private void Start()
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Bath");

        foreach (GameObject go in destinations)
        {
            Destination destination = go.GetComponent<Destination>(); //getting the destination script from game object
            _destinations.Add(destination); //adding the destination script to the list
            _occupancyLimit += destination.OccupancyLimit; //increasing the occupancy limit maximum
        }
        AdmitGuest();
    }

    private void AdmitGuest()
    {
        //guard statement, if bath house is full
        //if (_occupancyLimit <= _guest.Count) return;
        if (_guest.Count >= _occupancyLimit) return;

        foreach (Destination bath in _destinations)
        {
            //if bath is full guard statement
            if (bath.IsFull()) continue; //continue goes to the next line

            //add guest to path
            GameObject guest = Instantiate(GuestPrefab, transform.position, Quaternion.identity); //adding our gameobject to scene
            _guest.Add(guest.GetComponent<Guest>()); //adding our gameobject guest script to the guest list
            guest.GetComponent<Guest>().Destination = bath; //setting the player destination
            bath.AddGuest(guest.GetComponent<Guest>()); //adding guest to the bath so it is occupied

            break;
        }
        //break goes to this line
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: Add a guest each EntranceRate
        //AdmitGuest();
    }
}