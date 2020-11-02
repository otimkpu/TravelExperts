using System;
using System.Collections.Generic;
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
            Product P = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string Statement = "SELECT * FROM Products where prodname NOT LIKE 'inactive%' ORDER BY ProdName";
            SqlCommand cmd = new SqlCommand(Statement, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // while there are customers
                {
                    P = new Product();
                    P.ProductID = (int)reader["ProductID"];
                    P.ProdName = reader["ProdName"].ToString();
                    products.Add(P);
                }
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

        public static void AddProduct(string prodName)
        {
           SqlConnection con = TravelExpertsDB.GetConnection();
           string insertStatement = "INSERT INTO Products (prodname) VALUES (@ProdName) ";
           SqlCommand cmd = new SqlCommand(insertStatement, con);
           cmd.Parameters.AddWithValue("@ProdName", prodName);
           con.Open();
           cmd.ExecuteNonQuery();                        
           con.Close();
        }
        public static void UpdateProduct(string newprodname, string oldprodname)
        {
            try
            {
                SqlConnection con = TravelExpertsDB.GetConnection();
                string insertStatement = "UPDATE Products SET ProdName = @prodname WHERE @oldprodname = ProdName ; ";
                SqlCommand cmd = new SqlCommand(insertStatement, con);
                cmd.Parameters.AddWithValue("@prodname", newprodname);
                cmd.Parameters.AddWithValue("@oldprodname", oldprodname);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception p)
            { 
              throw p;
            }

        }
       
    }
}
    