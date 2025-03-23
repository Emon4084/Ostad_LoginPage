using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Login_FogetPassword.Models
{
    public class BaseMember
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<BaseMember> GetLoginMembers(string userName, string password)
        {
            List<BaseMember> members = new List<BaseMember>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Users WHERE LoginID = '" + userName + "' AND LoginPassword = '" + password + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember member = new BaseMember();
                    member.UserName = reader["LoginID"].ToString();
                    member.Password = reader["LoginPassword"].ToString();
                    members.Add(member);
                }
            }
            connection.Close();
            return members;
        }

        public bool UpdateNewPassword(string userName, string newPassword)
        {
            bool isUpdated = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE Users SET LoginPassword = @NewPassword WHERE LoginID = @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewPassword", newPassword);
            command.Parameters.AddWithValue("@UserName", userName);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                isUpdated = true;
            }

            connection.Close();
            return isUpdated;
        }


        public List<BaseMember> GetMembers(string userName)
        {
            List<BaseMember> members = new List<BaseMember>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT LoginID FROM Users WHERE LoginID = '" + userName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember member = new BaseMember();
                    member.UserName = reader["LoginID"].ToString();
                    members.Add(member);
                }
            }
            connection.Close();
            return members;
        }

    }
}