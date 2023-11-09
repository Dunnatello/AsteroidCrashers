/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Time String Creator
 * Script Objective: Used to create a formatted time string by taking a float value as an argument and returning a string.
 * 
 */
using UnityEngine;

public class TimeStringCreator
{

    private readonly float sixtyDivisor = 1f / 60f; // Doing this calculation here since multiplication is more performant than division.

    public string GetNewTimeString( float newTime ) {


        int hours = Mathf.FloorToInt( newTime * sixtyDivisor * sixtyDivisor );
        int minutes = Mathf.FloorToInt( newTime * sixtyDivisor );
        int seconds = Mathf.FloorToInt( newTime % 60 );
        int milliseconds = Mathf.FloorToInt( newTime * 100f % 100 );

        return string.Format( "{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds % 100 );

    }

}
