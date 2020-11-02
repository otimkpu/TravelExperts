using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public static class Product_supplierDB
    {
        public static List<suppliers> GetSuppliers()
        {
            List<suppliers> productSuppliers = new List<suppliers>(); //empty list
            suppliers pro; //just for reading
            //create the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            //create the command  for SELECT query to get the product suppliers
            string query = "SELECT * FROM Products_Suppliers ";

            SqlCommand selectCmd = new SqlCommand(query, connection);
            try
            {
                //open the connection
                    connection.Open();
                //run the command
                SqlDataReader reader = selectCmd.ExecuteReader(); //built-in

                //each state data returned, make state object and add to the list
                    while (reader.Read()) //while there still is data to read
                {
                    pro = new suppliers
                    {
                        ProductSupplierId = (int)reader["ProductSupplierId"],
                        ProductId = (int)reader["ProductId"],
                        SupplierId = (int)reader["SupplierId"]
                    };

                    productSuppliers.Add(pro);
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
            //return the list of product suppliers
            return productSuppliers;
        }

        public static int AddProductSupplier(suppliers prosup)
            {
                int prosupID = 0;

                // create connection
                SqlConnection connection = TravelExpertsDB.GetConnection();

                // create INSERT command
                // ProductSupplierID is IDENTITY so no value provided
                string insertStatement =
                    "INSERT INTO Products_Suppliers(ProductId, SupplierId) OUTPUT inserted.ProductSupplierId VALUES(@ProductId, @SupplierId)";
                SqlCommand cmd = new SqlCommand(insertStatement, connection);
                cmd.Parameters.AddWithValue("@ProductId", prosup.ProductId);
                cmd.Parameters.AddWithValue("@SupplierId", prosup.SupplierId);
                try
                {
                    connection.Open();

                    // execute insert command and get inserted ID
                    prosupID = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

                return prosupID;
            }

            public static bool DeleteProductSupplier(suppliers prosup)
            {
                bool success = false;

                // create connection
                SqlConnection connection = TravelExpertsDB.GetConnection();

                // create DELETE command
                string deleteStatement =
                    "DELETE FROM Products_Suppliers " +
                    "WHERE ProductSupplierId = @ProductSupplierId " + // needed for identification
                    "AND ProductId  = @ProductId " + // the rest - for optimistic concurrency
                    "AND SupplierId = @SupplierId ";
                SqlCommand cmd = new SqlCommand(deleteStatement, connection);
                cmd.Parameters.AddWithValue("@ProductSupplierId", prosup.ProductSupplierId);
                cmd.Parameters.AddWithValue("@ProductId", prosup.ProductId);
                cmd.Parameters.AddWithValue("@SupplierId", prosup.SupplierId);

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

            public static bool UpdateProductSupplier(suppliers oldprosup, suppliers newprosup)
            {
                bool success = false; // did not update

                // connection
                SqlConnection connection = TravelExpertsDB.GetConnection();
                // update command
                string updateStatement =
                    "UPDATE Products_Suppliers SET ProductId = @NewProductId, SupplierId = @NewSupplierId " +
                    "WHERE ProductId = @OldProductId " +
                    "AND SupplierId = @OldSupplierId";

                SqlCommand cmd = new SqlCommand(updateStatement, connection);
                cmd.Parameters.AddWithValue("@NewProductId ", newprosup.ProductId);
                cmd.Parameters.AddWithValue("@NewSupplierId", newprosup.SupplierId);
                cmd.Parameters.AddWithValue("@OldProductId", oldprosup.ProductId);
                cmd.Parameters.AddWithValue("@OldSupplierId", oldprosup.SupplierId);

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

        public static suppliers GetSuppliers(string Id, string Id1)
        {
            suppliers productSuppliers;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create SELECT command
            string query = "SELECT ProductId, SupplierId FROM Products_Suppliers WHERE ProductId  = @ProductId AND SupplierId = @SupplierId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            productSuppliers = new suppliers();
            cmd.Parameters.AddWithValue("@ProductId", int.Parse(Id));
            cmd.Parameters.AddWithValue("@SupplierId", int.Parse(Id1));

            try
            {
                connection.Open();

                // execute the command
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                // check if successful
                if (reader.Read())
                {
                    productSuppliers.ProductId = (int)reader["ProductId"];
                    productSuppliers.SupplierId = (int)reader["SupplierId"];
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

            return productSuppliers;
        }

    }
}




