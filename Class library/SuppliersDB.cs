using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public static class SuppliersDB
    {
        // retrieve supplier with given ID
        public static List<Suppliers> GetSuppliers()
        {
            List<Suppliers> suppliers = new List<Suppliers>();

            Suppliers pSpack = null;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create SELECT command
            string query = "SELECT SupplierId, SupName " +
                           "FROM Suppliers ";

            SqlCommand cmd = new SqlCommand(query, connection);

            // run the SELECT query
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // build supplier object to return
                while (reader.Read()) // if there is a supplier  with this ID
                {
                    pSpack = new Suppliers();
                    pSpack.SuppliersId = (int)reader["SupplierId"];
                    pSpack.SupName = reader["SupName"].ToString();
                    suppliers.Add(pSpack);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return suppliers;
        }

        public static object GetSuppliers(string rowId, string rowId1)
        {
            throw new NotImplementedException();
        }

        public static void AddSuppliers(Suppliers pSpack)
        {
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create INSERT command
            // PacketID is IDENTITY so no value provided
            string insertStatement =
                "INSERT INTO Suppliers (SupplierId, SupName) " +
                "VALUES(@SupplierId, @SupName)";
            SqlCommand cmd = new SqlCommand(insertStatement, connection);
            cmd.Parameters.AddWithValue("@SupplierId", pSpack.SuppliersId);
            cmd.Parameters.AddWithValue("@SupName", pSpack.SupName);
            try
            {
                connection.Open();

                // execute insert command and get supplierID
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return;
        }

        // insert new row to table Suppliers
        // return new SupplierID
        public static int AddSupplier(Suppliers pSpack)
        {
            int pSpackID = 0;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create INSERT command
            // PacketID is IDENTITY so no value provided
            string insertStatement =
                "INSERT INTO Suppliers (SupplierId, SupName) " +
                "VALUES(@SupplierId, @SupName)";
            SqlCommand cmd = new SqlCommand(insertStatement, connection);

            cmd.Parameters.AddWithValue("@SupName", pSpack.SupName);
            try
            {
                connection.Open();

                // execute insert command and get PacketID
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return pSpackID;
        }

        // delete supplier
        // return indicator of success
        public static bool Delete(Suppliers pSpack)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create DELETE command
            string deleteStatement =
                "DELETE FROM Suppliers " +
                 "WHERE SupplierId = @SupplierId " + // needed for identification
                "AND SupName = @SupName";  // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatement, connection);
            cmd.Parameters.AddWithValue("@SupName", pSpack.SupName);

            try
            {
                connection.Open();

                // execute the command
                int count = cmd.ExecuteNonQuery();
                // check if successful
                if (count > 0)
                    success = true;
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

        // update suppliers
        // return indicator of success
        public static bool UpdateSuppliers(Suppliers oldPack, Suppliers newPack)
        {
            bool success = false; // did not update

            // connection
            SqlConnection connection = TravelExpertsDB.GetConnection();
            // update command
            string updateStatement =
                "UPDATE Suppliers SET " +
                "SupplierId = @NewSupplierId, " +
               "SupName = @NewSupName " +
                "WHERE SupplierId = @OldSupplierId " + // identifies packages
                "AND SupName = @OldSupName";// remaining - for otimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatement, connection);
            cmd.Parameters.AddWithValue("@NewSupplierId", newPack.SuppliersId);
            cmd.Parameters.AddWithValue("@NewSupName", newPack.SupName);
            cmd.Parameters.AddWithValue("@OldSupplierId", oldPack.SuppliersId);
            cmd.Parameters.AddWithValue("@OldSupName", oldPack.SupName);


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
        public static Suppliers GetSuppliers(string id)
        {
            Suppliers suppliers;

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string query = "SELECT SupplierId," +
                            "SupName  FROM Suppliers WHERE SupplierId =@Pid;";
            SqlCommand cmd = new SqlCommand(query, connection);
            suppliers = new Suppliers();
            cmd.Parameters.AddWithValue("@Pid", int.Parse(id));

            try
            {
                //open the connection
                connection.Open();
                //run the command
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow); //built-in

                //each state data returned, make state object and add to the list
                if (reader.Read())
                {

                    suppliers.SuppliersId = (int)reader["SupplierId"];
                    suppliers.SupName = reader["SupName"].ToString();

                }
            }
            catch (Exception ex)  //error   
            {
                throw ex;
            }
            finally  //executes always
            {
                connection.Close();
            }

            return suppliers;

        }

    }
}