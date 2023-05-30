using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BirdHouseProject.Views;


namespace BirdHouseProject
{
    static class Program
    {
        private const string ConnectionString = "Data Source=**TO-DO**;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateDatabase();
            CreateLadyGouldianFinchTable();
            CreateCageTable();
            Application.Run(new Login());    
        }

        /// <summary>
        /// Handels the Project data-base functionallty (Creates the table if needed)
        /// </summary>
        public static void CreateDatabase()
        {
            string serverName = "**TO-DO**";
            string databaseName = "BirdHouseProjectDb";
            string connectionString = $"Data Source={serverName};Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the database already exists
                string checkQuery = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    int databaseCount = (int)checkCommand.ExecuteScalar();

                    // Database already exists
                    if (databaseCount > 0)
                    {
                        Console.WriteLine("Database already exists.");
                        return;
                    }
                }

                // Create the database if it doesn't exist
                string createQuery = $"CREATE DATABASE {databaseName}";

                using (SqlCommand createCommand = new SqlCommand(createQuery, connection))
                {
                    createCommand.ExecuteNonQuery();
                    Console.WriteLine("Database created successfully.");
                }
            }
        }

        /// <summary>
        /// Handels the Cage data-base functionallty (Creates the table if needed)
        /// </summary>
        public static void CreateCageTable()
        {
            string tableName = "Cage";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Check if the table already exists
                string checkQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    int tableCount = (int)checkCommand.ExecuteScalar();

                    // Cage Table already exists
                    if (tableCount > 0)
                    {
                        return;
                    }
                }

                // Create the table if it doesn't exist
                string createQuery = $@"CREATE TABLE {tableName} (
                                Serial_Number INT IDENTITY(120000,1) PRIMARY KEY,
                                Length FLOAT NOT NULL,
                                Width FLOAT NOT NULL,
                                Height FLOAT NOT NULL,
                                Material NVARCHAR(50) NOT NULL
                            )";

                using (SqlCommand createCommand = new SqlCommand(createQuery, connection))
                {
                    createCommand.ExecuteNonQuery();
                }

                // Insert default values for 5 cages
                string insertQuery = $"INSERT INTO {tableName} (Length, Width, Height, Material) VALUES ";

                for (int i = 1; i <= 5; i++)
                {
                    if (i % 2 == 0)
                    {
                        insertQuery += $"(20.0, 30.0, 40.0, 'Steel')";
                    }
                    else if (i % 3 == 0)
                    {
                        insertQuery += $"(40.0, 50.0, 60.0, 'Wood')";
                    }
                    else
                    {
                        insertQuery += $"(70.0, 80.0, 90.0, 'Plastic')";
                    }

                    if (i < 5)
                    {
                        insertQuery += ",";
                    }
                }

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Handels the LadyGouldianFinch data-base functionallty (Creates the table if needed)
        /// </summary>
        public static void CreateLadyGouldianFinchTable()
        {
            string tableName = "LadyGouldianFinch";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Check if the table already exists
                string checkQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    int tableCount = (int)checkCommand.ExecuteScalar();

                    // LadyGouldianFinch Table already exists
                    if (tableCount > 0)
                    {
                        return;
                    }
                }

                // Create the table if it doesn't exist
                string createQuery = $@"CREATE TABLE {tableName} (
            Serial_Number INT IDENTITY(100000,1) PRIMARY KEY,
            Species NVARCHAR(50) NOT NULL,
            Sub_Species NVARCHAR(50) NOT NULL,
            Hatch_Date NVARCHAR(50) NOT NULL,
            Gender NVARCHAR(50) NOT NULL,
            Cage_Number NVARCHAR(50) NOT NULL,
            F_Serial_Number INT NOT NULL,
            M_Serial_Number INT NOT NULL,
            Head_Color NVARCHAR(50) NOT NULL,
            Breast_Color NVARCHAR(50) NOT NULL,
            Body_Color NVARCHAR(50) NOT NULL
        )";

                using (SqlCommand createCommand = new SqlCommand(createQuery, connection))
                {
                    createCommand.ExecuteNonQuery();
                }

                // Insert default values for 6 birds
                string insertQuery = $@"INSERT INTO {tableName} (Species, Sub_Species, Hatch_Date, Gender, Cage_Number, F_Serial_Number, M_Serial_Number, Head_Color, Breast_Color, Body_Color) 
                                VALUES 
                                    ('Gouldian American', 'North America', '01/01/2023', 'Male', '120000', 110000, 110001, 'Red', 'Purple', 'Green'),
                                    ('Gouldian American', 'Central America', '02/01/2023', 'Female', '120000', 110002, 110003, 'Red', 'Lilac', 'Green'),
                                    ('Gouldian European', 'East Europe', '01/02/2023', 'Male', '120001', 110004, 110005, 'Black', 'White', 'Blue'),
                                    ('Gouldian European', 'Western Europe', '02/02/2023', 'Female', '120001', 110006, 110007, 'Red', 'Lilac', 'Green'),
                                    ('Gouldian Australian', 'Central Australia', '01/03/2023', 'Male', '120002', 110008, 110009, 'Black', 'White', 'Blue'),
                                    ('Gouldian Australian', 'Coastal Cities', '02/03/2023', 'Female', '120002', 110010, 110011, 'Orange', 'White', 'Yellow')";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}