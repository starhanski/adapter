using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Npgsql;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace lab_06
{

    public interface IData
    {
        void Connect(string connectionString);
        void ExecuteQuery(string query);
        DataTable GetResults();
    }

    // Адаптер для роботи з базою даних формату MySQL
    public class MySQLAdapter : IData
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;

        public void Connect(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        public void ExecuteQuery(string query)
        {
            _command.CommandText = query;
            _command.ExecuteNonQuery();
        }

        public DataTable GetResults()
        {
            var data = new DataTable();
            using (var reader = _command.ExecuteReader())
            {
                data.Load(reader);
            }
            return data;
        }
    }

    // Адаптер для роботи з базою даних формату PostgreSQL
    public class PostgreSQLAdapter : IData
    {
        private NpgsqlConnection _connection;
        private NpgsqlCommand _command;

        public void Connect(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        public void ExecuteQuery(string query)
        {
            _command.CommandText = query;
            _command.ExecuteNonQuery();
        }

        public DataTable GetResults()
        {
            var data = new DataTable();
            using (var reader = _command.ExecuteReader())
            {
                data.Load(reader);
            }
            return data;
        }
    }

 
    public class Client
    {
        private IData _data;

        public Client(IData data)
        {
            _data = data;
        }

        public void ConnectToDatabase(string connectionString)
        {
            _data.Connect(connectionString);
        }

        public void QueryDatabase(string query)
        {
            _data.ExecuteQuery(query);
        }

        public DataTable GetResults()
        {
            return _data.GetResults();
        }
    }

}
