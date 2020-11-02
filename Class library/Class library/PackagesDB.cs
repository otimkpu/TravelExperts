using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_library
{
    public class PackagesDB
    {
        public static List<Packages> GetPackages()
        {
            List<Packages> packages = new List<Packages>();  //empty list
            Packages pk; //just for reading      variables expressed before the commands!
            //create the connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            //create the command  for SELECT query to get the states
            string query = "SELECT PackageId, PkgName, PkgStartDate, " +
                            "PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission " +
                           "FROM Packages ";

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
                    pk = new Packages();
                    pk.PackageId = (int)reader["PackageId"];  //[]  indexer from chapter 13
                    pk.PkgName = reader["PkgName"].ToString();
                    pk.PkgStartDate = ((DateTime)reader["PkgStartDate"]).Date;
                    pk.PkgEndDate = ((DateTime)reader["PkgEndDate"]).Date;
                    pk.PkgDesc = reader["PkgDesc"].ToString();
                    pk.PkgBasePrice = (decimal)reader["PkgBasePrice"];
                    pk.PkgAgencyCommission = (decimal)reader["PkgAgencyCommission"];

                    packages.Add(pk);
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

            //return the list of states
            return packages;
        }

        public static int AddPackage(Packages pack)
        {
            int packID = 0;

            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatement =
                "INSERT INTO Packages(PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) " +
                "OUTPUT inserted.PackageId " +
                "VALUES(@PName, @PSD, @PED, @PDesc, @PBP, @PAC)";
            SqlCommand cmd = new SqlCommand(insertStatement, connection);
            cmd.Parameters.AddWithValue("@PName", pack.PkgName);
            cmd.Parameters.AddWithValue("@PSD", pack.PkgStartDate);
            cmd.Parameters.AddWithValue("@PED", pack.PkgEndDate);
            cmd.Parameters.AddWithValue("@PDesc", pack.PkgDesc);
            cmd.Parameters.AddWithValue("@PBP", pack.PkgBasePrice);
            cmd.Parameters.AddWithValue("@PAC", pack.PkgAgencyCommission);
            try
            {
                connection.Open();

                // execute insert command and get inserted ID
                packID = (int)cmd.ExecuteScalar();
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

            return packID;
        }

        public static bool UpdatePackage(Packages oldPack, Packages newPack)
        {
            bool success = false; // did not update

            // connection
            SqlConnection connection = TravelExpertsDB.GetConnection();
            // update command
            string updateStatement =
                "UPDATE Packages SET " +
                "PkgName = @NewPName, " +
                "PkgStartDate = @NewPSD, " +
                "PkgEndDate = @NewPED, " +
                "PkgDesc = @NewPdesc, " +
                "PkgBasePrice = @NewPBP, " +
                "PkgAgencyCommission = @NewPAC " +
                "WHERE PackageId = @OldPackageID " + // identifies ccustomer
                "AND PkgName = @OldPName " + // remaining - for otimistic concurrency
                "AND PkgStartDate = @OldPSD " +
                "AND PkgEndDate = @OldPED " +
                "AND PkgDesc = @OldPdesc " +
                "AND PkgBasePrice = @OldPBP " +
                "AND PkgAgencyCommission = @OldPAC";

            SqlCommand cmd = new SqlCommand(updateStatement, connection);
            cmd.Parameters.AddWithValue("@NewPName", newPack.PkgName);
            cmd.Parameters.AddWithValue("@NewPSD", newPack.PkgStartDate);
            cmd.Parameters.AddWithValue("@NewPED", newPack.PkgEndDate);
            cmd.Parameters.AddWithValue("@NewPdesc", newPack.PkgDesc);
            cmd.Parameters.AddWithValue("@NewPBP", newPack.PkgBasePrice);
            cmd.Parameters.AddWithValue("@NewPAC", newPack.PkgAgencyCommission);
            cmd.Parameters.AddWithValue("@OldPackageID", oldPack.PackageId);
            cmd.Parameters.AddWithValue("@OldPName", oldPack.PkgName);
            cmd.Parameters.AddWithValue("@OldPSD", oldPack.PkgStartDate);
            cmd.Parameters.AddWithValue("@OldPED", oldPack.PkgEndDate);
            cmd.Parameters.AddWithValue("@OldPdesc", oldPack.PkgDesc);
            cmd.Parameters.AddWithValue("@OldPBP", oldPack.PkgBasePrice);
            cmd.Parameters.AddWithValue("@OldPAC", oldPack.PkgAgencyCommission);
            
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

        public static Packages GetPackage(string id)
        {
            Packages package;
            
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string query = "SELECT PackageId, PkgName, PkgStartDate, " +
                            "PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission  FROM Packages WHERE PackageId =@Pid;";
            SqlCommand cmd = new SqlCommand(query, connection);
            package = new Packages();
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

                    package.PackageId = (int)reader["PackageId"];  //[]  indexer from chapter 13
                    package.PkgName = reader["PkgName"].ToString();
                    package.PkgStartDate = (DateTime)reader["PkgStartDate"];
                    package.PkgEndDate = (DateTime)reader["PkgEndDate"];
                    package.PkgDesc = reader["PkgDesc"].ToString();
                    package.PkgBasePrice = (decimal)reader["PkgBasePrice"];
                    package.PkgAgencyCommission = (decimal)reader["PkgAgencyCommission"];
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

            return package;

        }

    }
}
