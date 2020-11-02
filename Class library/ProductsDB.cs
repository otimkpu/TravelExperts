using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_library;


namespace Class_library
{
    public static class ProductDB
    {
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            Product P;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string Statement = "SELECT * FROM Products ORDER BY ProdName";
            SqlCommand cmd = new SqlCommand(Statement, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there are products
                {
                    P = new Product();
                    P.ProductID = (int)reader["ProductID"];
                    P.ProdName = reader["ProdName"].ToString();
                    products.Add(P);

                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public static int AddProduct(Product pro)
        {

            int proID = 0;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string insertStatement = "INSERT INTO Products (prodname) Output Inserted.ProductID VALUES (@ProdName) ";
            SqlCommand cmd = new SqlCommand(insertStatement, con);
            cmd.Parameters.AddWithValue("@ProdName", pro.ProdName);

            try
            {
                con.Open();
                proID = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return proID;
        }

        public static bool UpdateProduct(Product newprodname, Product oldprodname)
        {
            bool success = false;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Products SET ProdName = @newprodname WHERE ProductID = @oldprodID; ";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            cmd.Parameters.AddWithValue("@newprodname", newprodname.ProdName);
            cmd.Parameters.AddWithValue("@oldprodname", oldprodname.ProdName);
            cmd.Parameters.AddWithValue("@oldprodID", oldprodname.ProductID);
            try
            {
                con.Open();
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
                con.Close();
            }
            return success;
        }

        public static Product GetAllProducts(string Id)
        {
            Product product;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();
            // create SELECT command
            string query = "SELECT ProductID, ProdName FROM Products WHERE ProductId = @ProductID ";
            //"ProdName = @ProdName;";
            SqlCommand cmd = new SqlCommand(query, connection);
            product = new Product();
            cmd.Parameters.AddWithValue("@ProductId", int.Parse(Id));

            try
            {
                connection.Open();
                // execute the command
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                // check if successful
                if (reader.Read())
                {
                    product.ProductID = (int)reader["ProductId"];
                    product.ProdName = reader["ProdName"].ToString();
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
            return product;
        }

    }
}
