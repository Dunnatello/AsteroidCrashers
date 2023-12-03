/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: High Score Manager
 * Script Objective: Manages Saving/Loading High Scores List and Adding New Scores
 * 
 * Source(s):
 * 		https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptostream?view=net-8.0
 *		https://videlais.com/2021/02/28/encrypting-game-data-with-unity/
 *      https://www.youtube.com/watch?v=32zOlthWX2s
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace TeamBracket.HighScores {

    [System.Serializable]
    public class HighScore {

        public float timeSurvived;
        public int asteroidsDestroyed;
        public float overallScore;
        public string date;

        public HighScore( int asteroidsDestroyed, float timeSurvived ) {

            this.asteroidsDestroyed = asteroidsDestroyed;
            this.timeSurvived = timeSurvived;
            date = DateTime.Now.ToString( "MM/dd/yy" );

        }

    }

    [System.Serializable]
    public class HighScoresWrapper {

        public List<HighScore> scores;

        public HighScoresWrapper( List<HighScore> scores ) {

            this.scores = scores;

        }

    }

    public class HighScoreManager : MonoBehaviour {

        // FIXME: Unserialize this after testing is completed.
        [SerializeField] private List<HighScore> sortedScores = new( );

        [Header( "Points Allocation" )]
        [SerializeField] private float survivalPointsPerSecond = 1f;
        [SerializeField] private float pointsPerAsteroidDestroyed = 10f;


        private readonly string saveFileName = "scores.json";
        private bool isLoaded = false;

        // Readonly Public Variables. The targeted variables should NOT be changed during runtime and only should be assigned via the Unity Inspector window.
        public bool IsLoaded => isLoaded;
        public float SurvivalPointsPerSecond => survivalPointsPerSecond;
        public float PointsPerAsteroidDestroyed => survivalPointsPerSecond;
        public List<HighScore> HighScores => sortedScores;

        private void Start( ) {

            LoadScores( );


        }


        // Create HighScore object and calculate overall score based on the specified formula.
        private HighScore CreateHighScore( int asteroidsDestroyed, float timeSurvived ) {

            HighScore newHighScore = new( asteroidsDestroyed, timeSurvived ) {
                overallScore = ( timeSurvived * survivalPointsPerSecond ) + ( asteroidsDestroyed * pointsPerAsteroidDestroyed )
            };

            return newHighScore;

        }

        // This function is called by GameManager after the game is over.
        public void AddScore( int asteroidsDestroyed, float timeSurvived ) {

            // Add Score to List
            sortedScores.Add( CreateHighScore( asteroidsDestroyed, timeSurvived ) );

            // Sort List From Highest To Lowest
            sortedScores.Sort( ( a, b ) => b.overallScore.CompareTo( a.overallScore ) );

            if ( sortedScores.Count > 10 ) {

                sortedScores.RemoveRange( 10, sortedScores.Count - 10 );

            }

            // Save Scores to File
            SaveScores( );

        }

        public HighScore GetScore( int asteroidsDestroyed, float timeSurvived ) {

            HighScore foundScore = sortedScores.Find( score => score.timeSurvived == timeSurvived && score.asteroidsDestroyed == asteroidsDestroyed );

            return foundScore;

        }

        // Save Score List to File
        private void SaveScores( ) {

            try {
                string filePath = Path.Combine( Application.persistentDataPath, saveFileName );

                //Convert data into json which hold all info and structure of data
                string json = JsonUtility.ToJson( new HighScoresWrapper( sortedScores ), true );

                //Encrypt json
                string encryptedJson = EncryptString( json, "hardcodedKey" );

                //Write to encrypted json while open
                using ( StreamWriter writer = new( filePath ) ) {
                    writer.Write( encryptedJson );
                }

                Debug.Log( "Scores saved successfully." );

            }
            catch ( Exception e ) {
                Debug.LogError( "Error saving scores: " + e.Message );
            }
        }

        private string EncryptString( string plainText, string key ) {

            //Advanced Encryption Standard
            //creating new instance of aes class and aesAlg variable
            using Aes aesAlg = Aes.Create( );
            //key is converting key string to byte array using UTF-8
            aesAlg.Key = Encoding.UTF8.GetBytes( key.PadRight( 32 ) );
            aesAlg.IV = new byte[ 16 ];

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor( aesAlg.Key, aesAlg.IV );

            //Using MS CryptoStream to encrypt the string with Key and IV defined.
            using MemoryStream msEncrypt = new( );
            using ( CryptoStream csEncrypt = new( msEncrypt, encryptor, CryptoStreamMode.Write ) ) {
                using StreamWriter swEncrypt = new( csEncrypt );
                // Write all data to the stream.
                swEncrypt.Write( plainText );
            }

            return Convert.ToBase64String( msEncrypt.ToArray( ) );

        }


        // Load Score List From File
        private void LoadScores( ) {

            string filePath = Application.persistentDataPath + "/" + saveFileName;


            if ( File.Exists( filePath ) ) {

                try {

                    using StreamReader reader = new( filePath );

                    //Read encrypted JSON from file
                    string encryptedJson = reader.ReadToEnd( );

                    //Decrypt JSON before sending any info to screen
                    string json = DecryptString( encryptedJson, "hardcodedKey" );

                    HighScoresWrapper wrapper = JsonUtility.FromJson<HighScoresWrapper>( json );

                    if ( wrapper != null ) {

                        sortedScores = wrapper.scores;

                    }

                }
                catch ( Exception e ) {

                    Debug.Log( "Error when loading saved scores: " + e.Message );

                }

            }

            isLoaded = true;

        }

        private string DecryptString( string cipherText, string key ) {

            using Aes aesAlg = Aes.Create( );
            aesAlg.Key = Encoding.UTF8.GetBytes( key.PadRight( 32 ) );
            aesAlg.IV = new byte[ 16 ];

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor( aesAlg.Key, aesAlg.IV );

            //Using MS CryptoStream to decrypt the data
            using MemoryStream msDecrypt = new( Convert.FromBase64String( cipherText ) );
            using CryptoStream csDecrypt = new( msDecrypt, decryptor, CryptoStreamMode.Read );
            using StreamReader srDecrypt = new( csDecrypt );
            //read decrypted bytes and return to string format to be returned
            return srDecrypt.ReadToEnd( );

        }

    }

}