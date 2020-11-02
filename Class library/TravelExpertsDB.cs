﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()  //method which needs a call and return   this will be called in the StateDB (when connection needs to be made)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = TravelExperts; Integrated Security = True";  //@ allows you to put the entire path 
            //regardless of special characters
            return new SqlConnection(connectionString);
            
        }
    }
}
