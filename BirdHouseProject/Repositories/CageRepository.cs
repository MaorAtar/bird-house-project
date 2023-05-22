using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Repositories
{
    /// <summary>
    /// Repository class for managing cages in the BirdHouseProject application.
    /// </summary>
    class CageRepository : BaseRepository, ICageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CageRepository"/> class with the specified SQL connection string.
        /// </summary>
        /// <param name="connectionString">The SQL connection string.</param>
        public CageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Adds a new cage to the database.
        /// </summary>
        /// <param name="cageModel">The cage model to add.</param>
        public void Add(CageModel cageModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into Cage values (@length, @width, @height, @material)";
                command.Parameters.Add("@length", SqlDbType.Float).Value = cageModel.Length;
                command.Parameters.Add("@width", SqlDbType.Decimal).Value = cageModel.Width;
                command.Parameters.Add("@height", SqlDbType.Decimal).Value = cageModel.Height;
                command.Parameters.Add("@material", SqlDbType.NVarChar).Value = cageModel.Material;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a cage from the database based on the specified serial number.
        /// </summary>
        /// <param name="serial_number">The serial number of the cage to delete.</param>
        public void Delete(int serial_number)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            DELETE FROM LadyGouldianFinch
            WHERE Cage_Number = @serial_number;
            
            DELETE FROM Cage
            WHERE Serial_Number = @serial_number;";
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = serial_number;
                command.ExecuteNonQuery();
            }
        }



        /// <summary>
        /// Updates the information of an existing cage in the database.
        /// </summary>
        /// <param name="cageModel">The cage model to update.</param>
        public void Edit(CageModel cageModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Cage set Length=@length, Width=@width, Height=@height, Material=@material where Serial_Number=@serial_number";
                command.Parameters.Add("@length", SqlDbType.Decimal).Value = cageModel.Length;
                command.Parameters.Add("@width", SqlDbType.Decimal).Value = cageModel.Width;
                command.Parameters.Add("@height", SqlDbType.Decimal).Value = cageModel.Height;
                command.Parameters.Add("@material", SqlDbType.NVarChar).Value = cageModel.Material;
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = cageModel.Serial_number;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves all cages from the database.
        /// </summary>
        /// <returns>A collection of all cages in the database.</returns>
        public IEnumerable<CageModel> GetAll()
        {
            var cageList = new List<CageModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from Cage order by Serial_Number desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cageModel = new CageModel();
                        cageModel.Serial_number = (int)reader[0];
                        cageModel.Length = Convert.ToDouble(reader[1]);
                        cageModel.Width = Convert.ToDouble(reader[2]);
                        cageModel.Height = Convert.ToDouble(reader[3]);
                        cageModel.Material = reader[4].ToString();
                        cageList.Add(cageModel);
                    }
                }
            }
            if (cageList.Count == 1)
            {
                var cageView = new CageDataView(cageList[0].Serial_number, cageList[0].Length, cageList[0].Width, cageList[0].Height, cageList[0].Material);
                cageView.Show();
            }
            return cageList;
        }

        /// <summary>
        /// Retrieves cages from the database based on the specified search value.
        /// </summary>
        /// <param name="value">The search value to match against serial number or material.</param>
        /// <returns>A collection of cages that match the search value.</returns>
        public IEnumerable<CageModel> GetByValue(string value)
        {
            var cageList = new List<CageModel>();
            int serial_number = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string material = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Cage
                                        where Serial_Number=@serial_number
                                        or Material like @material+'%'
                                        order by Serial_Number desc";
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = serial_number;
                command.Parameters.Add("@material", SqlDbType.NVarChar).Value = material;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cageModel = new CageModel();
                        cageModel.Serial_number = (int)reader[0];
                        cageModel.Length = Convert.ToDouble(reader[1]);
                        cageModel.Width = Convert.ToDouble(reader[2]);
                        cageModel.Height = Convert.ToDouble(reader[3]);
                        cageModel.Material = reader[4].ToString();
                        cageList.Add(cageModel);
                    }
                }
            }
            return cageList;
        }
    }
}
