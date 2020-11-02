using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public class SuppliersDB
    {

        public static List<Suppliers> GetSuppliers()
        {
            List<Suppliers> suppliers = new List<Suppliers>();  //empty list
            Suppliers s; //just for reading      variables expressed before the commands!
            //create the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            //create the command  for SELECT query to get the states
            string query = "SELECT SupplierId, SupName FROM Suppliers ";

            SqlCommand cmd = new SqlCommand(query, connection);
            try
            {
                //open the connection
                connection.Open();
                //run the command
                SqlDataReader reader = cmd.ExecuteReader(); //built-in

                //each state data returned, make state object and add to the list
                while (reader.Read()) //while there still is data to read
                {
                    s = new Suppliers();
                    s.SuppliersId = (int)reader["SupplierId"];  //[]  indexer from chapter 13
                    s.SupName = reader["SupName"].ToString();
                    suppliers.Add(s);
                }
                reader.Close();

            }
            catch (Exception ex)  //error   
            {
                throw ex;
            }
            finally  //executes always
            {
                connection.Close();
            }

            //return the list of Suppliers
            return suppliers;
        }

        //public static int AddSupplier(global::TravelExperts.Supplier sup)
        //{
        //    throw new NotImplementedException();
        //}

        //public static int AddSupplier(global::TravelExperts.Supplier sup)
        //{
        //    throw new NotImplementedException();
        //}

        public static int AddSuppliers(Suppliers sup)
        {
            int supID = 0;

            //object TravelExpertDB = null;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatement =
                "INSERT INTO Suppliers(SupplierId, SupName) " +
                "OUTPUT inserted.SupplierId " +
                "VALUES(@SupplierId, @SupName)";
            SqlCommand cmd = new SqlCommand(insertStatement, connection);
            cmd.Parameters.AddWithValue("@SupplierId", sup.SuppliersId);
            cmd.Parameters.AddWithValue("@SupName", sup.SupName);
            try
            {
                connection.Open();

                // execute insert command and get inserted ID
                supID = (int)cmd.ExecuteScalar();
                //cmd.ExecuteNonQuery();

                // retrieve generate customer ID to return
                //string selectStatement =
                //    "SELECT IDENT_CURRENT('Customers')";
                //SqlCommand selectCmd = new SqlCommand(selectStatement, connection);
                //custID = Convert.ToInt32(selectCmd.ExecuteScalar()); // returns single value
                //         // (int) does not work in this case
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return supID;
        }

        public static bool UpdateSupplier(object modifysup, Suppliers newSupplier)
        {
            throw new NotImplementedException();
        }

        public static bool UpdateSupplier(Suppliers supplier, suppliers newSupplier)
        {
            throw new NotImplementedException();
        }

        public static int AddSupplier(Suppliers supplier)
        {
            throw new NotImplementedException();
        }

        //public static bool UpdateSupplier(Suppliers supplier, suppliers newSupplier)
        //{
        //    throw new NotImplementedException();
        //}

        //public static int AddSupplier(Suppliers supplier)
        //{
        //    throw new NotImplementedException();
        //}

        //public static object GetSupplier(string rowId)
        //{
        //    throw new NotImplementedException();
        //}

        public static bool UpdateSuppliers(Suppliers oldSup, Suppliers newSup)
        {
            bool success = false; // did not update

            // connection
            SqlConnection connection = TravelExpertsDB.GetConnection();
            // update command
            string updateStatement =
                "UPDATE Suppliers SET " +
                "SupplierId = @NewSupID, " +
                "SupName = @SupName, " +
                "WHERE SupplierId = @OldSupplierId " + // identifies Suppliers
                "AND SupName = @OldSupName "; // remaining - for otimistic concurrency

            SqlCommand cmd = new SqlCommand(updateStatement, connection);
            cmd.Parameters.AddWithValue("@NewSupID", newSup.SuppliersId);
            cmd.Parameters.AddWithValue("@NewSupName", newSup.SupName);

            try
            {
                connection.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    success = true; // updated
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return success;
        }

        public static bool DeleteSupplier(Suppliers supplier)
        {
            throw new NotImplementedException();
        }
    }


}
