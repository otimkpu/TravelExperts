using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public static class Products_suppliers_packagesDB
    {
        // retrieve customer with given ID
        public static List<Products_suppliers_packages> GetProducts_suppliers_packages()
        {

            List<Products_suppliers_packages> products_suppliers_packages_list = new List<Products_suppliers_packages>();

            Products_suppliers_packages pSpack = null;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create SELECT command
            string query = "SELECT PackageId, ProductSupplierId " +
                           "FROM Packages_Products_Suppliers "; //+
                                                                
            SqlCommand cmd = new SqlCommand(query, connection);

            // run the SELECT query
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // build product supplier package object to return
                while (reader.Read()) // if there is a package  with this ID
                {
                    pSpack = new Products_suppliers_packages();
                    pSpack.packageId = (int)reader["PackageId"];
                    pSpack.productSupplierId = (int)reader["ProductSupplierId"];
                    products_suppliers_packages_list.Add(pSpack);
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

            return products_suppliers_packages_list;
        }

        // insert new row to table Product Supplier Packages

        public static int AddProducts_suppliers_packages(Products_suppliers_packages pSpack)
        {
            int pSpackID = 0;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create INSERT command
            // PacketID is IDENTITY so no value provided
            string insertStatement =
                "INSERT INTO Packages_Products_Suppliers (PackageId, ProductSupplierId) " +
                "VALUES(@PackageId, @ProductSupplierId)";
            SqlCommand cmd = new SqlCommand(insertStatement, connection);
            cmd.Parameters.AddWithValue("@PackageId", pSpack.packageId);
            cmd.Parameters.AddWithValue("@ProductSupplierId", pSpack.productSupplierId);
            try
            {
                connection.Open();

                // execute insert command and get PackageID
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

        // delete customer
        // return indicator of success
        public static bool DeleteProducts_suppliers_packages(Products_suppliers_packages pSpack)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create DELETE command
            string deleteStatement =
                "DELETE FROM Products_suppliers_packages " +
                 "WHERE PackageId = @PackageId " + // needed for identification
                "AND ProductSupplierId = @ProductSupplierId";  // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatement, connection);
            cmd.Parameters.AddWithValue("@PackageId", pSpack.packageId);
            cmd.Parameters.AddWithValue("@ProductSupplierId", pSpack.productSupplierId);

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

        // update product supplier package
        // return indicator of success
        public static bool UpdateProducts_suppliers_packages(Products_suppliers_packages oldPack, Products_suppliers_packages newPack)
        {
            bool success = false; // did not update

            // connection
            SqlConnection connection = TravelExpertsDB.GetConnection();
            // update command
            string updateStatement =
                "UPDATE Packages_Products_Suppliers SET " +
                "PackageId = @NewPackageId, " +
               "ProductSupplierId = @NewProductSupplierId " +
                "WHERE PackageId = @OldPackageId " + // identifies packages
                "AND ProductSupplierId = @OldProductSupplierId ";// remaining - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatement, connection);
            cmd.Parameters.AddWithValue("@NewPackageId", newPack.packageId);
            cmd.Parameters.AddWithValue("@NewProductSupplierId", newPack.productSupplierId);
            cmd.Parameters.AddWithValue("@OldPackageId", oldPack.packageId);
           cmd.Parameters.AddWithValue("@OldProductSupplierId", oldPack.productSupplierId);


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
        public static Products_suppliers_packages GetProducts_suppliers_package(string id, string id1)
        {
            Products_suppliers_packages products_suppliers_packages;

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string query = "SELECT PackageId," +
                            "ProductSupplierId  FROM Packages_Products_Suppliers WHERE PackageId =@Pid AND ProductSupplierId =@PSid ;";
            SqlCommand cmd = new SqlCommand(query, connection);
            products_suppliers_packages = new Products_suppliers_packages();
            cmd.Parameters.AddWithValue("@Pid", int.Parse(id));
            cmd.Parameters.AddWithValue("@PSid", int.Parse(id1));
            try
            {
                //open the connection
                connection.Open();
                //run the command
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow); //built-in

                //each state data returned, make state object and add to the list
                if (reader.Read())
                {

                    products_suppliers_packages.packageId = (int)reader["PackageId"];  
                    products_suppliers_packages.productSupplierId = (int)reader["ProductSupplierId"];
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

            return products_suppliers_packages;

        }

    }
}

