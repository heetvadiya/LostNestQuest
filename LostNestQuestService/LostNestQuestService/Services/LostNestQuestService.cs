using LostNestQuestService.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostNestQuestService.Services
{
    public class LostNestQuestService : ILostNestQuestService
    {
        private readonly string connectionString = "Data Source=LAPTOP-DKAJFNA4\\SQLEXPRESS;Initial Catalog=LostNestQuestDB;Integrated Security=True"; // Replace with your actual connection string

        public LostNestQuestService()
        {
            EnsureDatabaseSchema();
        }

        private void EnsureDatabaseSchema()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create LostItems table if it doesn't exist
                string createLostItemsTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LostItems')
                    BEGIN
                        CREATE TABLE LostItems (
                            Id INT PRIMARY KEY IDENTITY,
                            Image VARBINARY(MAX),
                            Name NVARCHAR(100),
                            Description NVARCHAR(MAX),
                            Location NVARCHAR(100),
                            ContactNumber NVARCHAR(20)
                        )
                    END";
                SqlCommand createLostItemsTableCommand = new SqlCommand(createLostItemsTableQuery, connection);
                createLostItemsTableCommand.ExecuteNonQuery();

                // Create FoundItems table if it doesn't exist
                string createFoundItemsTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FoundItems')
                    BEGIN
                        CREATE TABLE FoundItems (
                            Id INT PRIMARY KEY IDENTITY,
                            Image VARBINARY(MAX),
                            Name NVARCHAR(100),
                            Description NVARCHAR(MAX),
                            Location NVARCHAR(100),
                            ContactNumber NVARCHAR(20)
                        )
                    END";
                SqlCommand createFoundItemsTableCommand = new SqlCommand(createFoundItemsTableQuery, connection);
                createFoundItemsTableCommand.ExecuteNonQuery();
            }
        }

        public string ReportLostItem(LostItem lostItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO LostItems (Image, Name, Description, Location, ContactNumber) VALUES (@Image, @Name, @Description, @Location, @ContactNumber)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Image", lostItem.Image);
                    command.Parameters.AddWithValue("@Name", lostItem.Name);
                    command.Parameters.AddWithValue("@Description", lostItem.Description);
                    command.Parameters.AddWithValue("@Location", lostItem.Location);
                    command.Parameters.AddWithValue("@ContactNumber", lostItem.ContactNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Lost item reported successfully." : "Failed to report lost item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public string ReportFoundItem(FoundItem foundItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO FoundItems (Image, Name, Description, Location, ContactNumber) VALUES (@Image, @Name, @Description, @Location, @ContactNumber)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Image", foundItem.Image);
                    command.Parameters.AddWithValue("@Name", foundItem.Name);
                    command.Parameters.AddWithValue("@Description", foundItem.Description);
                    command.Parameters.AddWithValue("@Location", foundItem.Location);
                    command.Parameters.AddWithValue("@ContactNumber", foundItem.ContactNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Found item reported successfully." : "Failed to report found item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public List<LostItem> GetAllLostItems()
        {
            List<LostItem> lostItems = new List<LostItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Image, Name, Description, Location, ContactNumber FROM LostItems";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LostItem lostItem = new LostItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Image = (byte[])reader["Image"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Location = reader["Location"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString()
                        };
                        lostItems.Add(lostItem);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error: " + ex.Message);
            }
            return lostItems;
        }

        public List<FoundItem> GetAllFoundItems()
        {
            List<FoundItem> foundItems = new List<FoundItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Image, Name, Description, Location, ContactNumber FROM FoundItems";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        FoundItem foundItem = new FoundItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Image = (byte[])reader["Image"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Location = reader["Location"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString()
                        };
                        foundItems.Add(foundItem);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error: " + ex.Message);
            }
            return foundItems;
        }

        //update lost items
        public string UpdateLostItem(LostItem lostItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE LostItems SET Image = @Image, Name = @Name, Description = @Description, Location = @Location, ContactNumber = @ContactNumber WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Image", lostItem.Image);
                    command.Parameters.AddWithValue("@Name", lostItem.Name);
                    command.Parameters.AddWithValue("@Description", lostItem.Description);
                    command.Parameters.AddWithValue("@Location", lostItem.Location);
                    command.Parameters.AddWithValue("@ContactNumber", lostItem.ContactNumber);
                    command.Parameters.AddWithValue("@Id", lostItem.Id); // Assuming Id is the primary key
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Lost item updated successfully." : "Failed to update lost item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        //update found item 

        public string UpdateFoundItem(FoundItem foundItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE FoundItems SET Image = @Image, Name = @Name, Description = @Description, Location = @Location, ContactNumber = @ContactNumber WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Image", foundItem.Image);
                    command.Parameters.AddWithValue("@Name", foundItem.Name);
                    command.Parameters.AddWithValue("@Description", foundItem.Description);
                    command.Parameters.AddWithValue("@Location", foundItem.Location);
                    command.Parameters.AddWithValue("@ContactNumber", foundItem.ContactNumber);
                    command.Parameters.AddWithValue("@Id", foundItem.Id); // Assuming Id is the primary key
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Found item updated successfully." : "Failed to update found item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // delete lost item
        public string DeleteLostItem(int lostItemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM LostItems WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", lostItemId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Lost item deleted successfully." : "Failed to delete lost item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // delete found item
        public string DeleteFoundItem(int foundItemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM FoundItems WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", foundItemId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Found item deleted successfully." : "Failed to delete found item.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public LostItem GetLostItemById(int itemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM LostItems WHERE Id = @ItemId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        LostItem lostItem = new LostItem
                        {
                            Id = (int)reader["Id"],
                            Image = (byte[])reader["Image"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Location = reader["Location"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString()
                        };
                        return lostItem;
                    }
                    else
                    {
                        return null; // Item not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public FoundItem GetFoundItemById(int itemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM FoundItems WHERE Id = @ItemId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        FoundItem foundItem = new FoundItem
                        {
                            Id = (int)reader["Id"],
                            Image = (byte[])reader["Image"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Location = reader["Location"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString()
                        };
                        return foundItem;
                    }
                    else
                    {
                        return null; // Item not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

    }
}