using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Repositories
{
    class CageRepository : BaseRepository, ICageRepository
    {
        // Constructor
        public CageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Methods
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

        public void Delete(int serial_number)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Cage where Serial_Number=@serial_number";
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = serial_number;
                command.ExecuteNonQuery();
            }
        }

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
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = cageModel.Serial_nubmer;
                command.ExecuteNonQuery();
            }
        }

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
                        cageModel.Serial_nubmer = (int)reader[0];
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
                var cageView = new CageDataView(cageList[0].Serial_nubmer, cageList[0].Length, cageList[0].Width, cageList[0].Height, cageList[0].Material);
                cageView.Show();
            }
            return cageList;
        }

        public IEnumerable<CageModel> GetByValue(string value)
        {
            var cageList = new List<CageModel>();
            int serial_number = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            double length = double.TryParse(value, out _) ? Convert.ToDouble(value) : 0.0;
            double width = double.TryParse(value, out _) ? Convert.ToDouble(value) : 0.0;
            double height = double.TryParse(value, out _) ? Convert.ToDouble(value) : 0.0;
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
                        cageModel.Serial_nubmer = (int)reader[0];
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
