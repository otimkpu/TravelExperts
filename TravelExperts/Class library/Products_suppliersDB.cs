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
        public static suppliers GetProductSupplierId(int prosupID)
        {
            suppliers prosup = null;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create SELECT command 
            string query = " SELECT * FROM Products_Suppliers WHERE ProductSupplierId = @ProductSupplierId";
           // string query =
                   //"SELECT p.ProductId, p.ProdName, s.SupplierId, s.SupName, ps.ProductSupplierId FROM Products as p JOIN Products_Suppliers as ps on p.ProductId = ps.ProductId join Suppliers s on ps.SupplierId = s.SupplierId WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand cmd = new SqlCommand(query, connection);
            // supply parameter value
            cmd.Parameters.AddWithValue("@ProductSupplierId", prosupID);

            // run the SELECT query
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // build customer object to return
                if (reader.Read()) // if there is a customer with this ID
                {
                    prosup = new suppliers
                    {
                        ProductSupplierId = (int)reader["ProductSupplierId"],
                        ProductId = (int)reader["ProductId"],
                        SupplierId = (int)reader["SupplierId"]
                        //ProdName = (string)reader["ProdName"],
                        //SupName = (string)reader["SupName"]
                    };
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

            return prosup;
        }
        //public static class Products_suppliersDB
        //{
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
            //return the list of product
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
                    "WHERE ProductSupplierId = @OldProductSupplierId " +
                    "AND ProductId = @OldProductId " +
                    "AND SupplierId = @OldSupplierId";

                SqlCommand cmd = new SqlCommand(updateStatement, connection);
                cmd.Parameters.AddWithValue("@NewProductId ", newprosup.ProductId);
                cmd.Parameters.AddWithValue("@NewSupplierId", newprosup.SupplierId);
                cmd.Parameters.AddWithValue("@OldProductSupplierId", oldprosup.ProductSupplierId);
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
        }
    }




