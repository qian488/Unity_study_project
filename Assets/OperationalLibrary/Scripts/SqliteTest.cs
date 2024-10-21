using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

namespace Operation
{
    public class SqliteTest : MonoBehaviour
    {
        SqliteConnection connection;
        SqliteCommand command;

        void Start()
        {
            SQLOpen();
            SQLCreateTable();
            SQLDelete();
            SQLInsert();
            SQLSelect();
        }

        void SQLSelect()
        {
            command = new SqliteCommand("SELECT * FROM PERSON", connection);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int age = reader.GetInt32(2);
                Debug.Log("id: " + id + " name: " + name + " age: " + age);
            }
        }

        void SQLInsert()
        {
            command = new SqliteCommand("INSERT INTO PERSON(NAME,AGE) VALUES('rikka',20)", connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        void SQLDelete()
        {
            command = new SqliteCommand("DELETE FROM PERSON WHERE NAME = 'rikka' ", connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        void SQLCreateTable()
        {
            command = new SqliteCommand("CREATE TABLE IF NOT EXISTS PERSON(ID INTEGER PRIMARY KEY AUTOINCREMENT,NAME TEXT,AGE INTEGER)", connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        void SQLOpen()
        {
            string path = "Data Source=" + Application.persistentDataPath + "/data.db";
            Debug.Log(path);
            connection = new SqliteConnection(path);
            connection.Open();
        }

        private void OnDestroy()
        {
            SQLClose();
        }

        void SQLClose()
        {
            connection.Close();
        }
    }
}

