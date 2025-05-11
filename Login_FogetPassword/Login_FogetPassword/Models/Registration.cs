using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Login_FogetPassword.Models
{
    public class Registration
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool SaveRegistration()
        {
            bool isSaved = false;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Registration (FirstName, LastName, UserName, Email, Password, Gender, Role, RegistrationDate) " +
                                   "VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @Gender, @Role, @RegistrationDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", this.FirstName);
                        command.Parameters.AddWithValue("@LastName", this.LastName);
                        command.Parameters.AddWithValue("@UserName", this.UserName);
                        command.Parameters.AddWithValue("@Email", this.Email);
                        command.Parameters.AddWithValue("@Password", this.Password);
                        command.Parameters.AddWithValue("@Gender", this.Gender ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Role", this.Role ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RegistrationDate", this.RegistrationDate);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            isSaved = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving registration: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }

            return isSaved;
        }


        public static List<Registration> GetAllUsers()
        {
            List<Registration> users = new List<Registration>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT FirstName,LastName,Email,Gender,Role FROM REGISTRATION";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Registration user = new Registration
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Role = reader["Role"].ToString(),
                            };
                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return users;
        }
    }
}
