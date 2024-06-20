﻿using System.Data.SqlClient;

namespace RepositoryLayer.Models
{
    public class ApplicationDbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
