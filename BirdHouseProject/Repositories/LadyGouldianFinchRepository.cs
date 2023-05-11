using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BirdHouseProject.Models;

namespace BirdHouseProject.Repositories
{
    public class LadyGouldianFinchRepository : BaseRepository, ILadyGouldianFinchRepository
    {
        // Constructor
        public LadyGouldianFinchRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Methods
        public void Add(LadyGouldianFinchModel ladyGouldianFinchModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into LadyGouldianFinch values (@species, @sub_species, @hatch_date, @gender, " +
                    "@cage_number, @f_serial_number, @m_serial_number, @head_color, @breast_color, @body_color)";
                command.Parameters.Add("@species", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Species;
                command.Parameters.Add("@sub_species", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Sub_species;
                command.Parameters.Add("@hatch_date", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Hatch_date;
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Gender;
                command.Parameters.Add("@cage_number", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Cage_number;
                command.Parameters.Add("@f_serial_number", SqlDbType.Int).Value = ladyGouldianFinchModel.F_serial_number;
                command.Parameters.Add("@m_serial_number", SqlDbType.Int).Value = ladyGouldianFinchModel.M_serial_number;
                command.Parameters.Add("@head_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Head_color;
                command.Parameters.Add("@breast_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Breast_color;
                command.Parameters.Add("@body_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Body_color;
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
                command.CommandText = "delete from LadyGouldianFinch where Serial_Number=@serial_number";
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = serial_number;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(LadyGouldianFinchModel ladyGouldianFinchModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update LadyGouldianFinch set Species=@species, Sub_Species=@sub_species, Hatch_Date=@hatch_date, Gender=@gender, " +
                    "Cage_Number=@cage_number, F_Serial_Number=@f_serial_number, M_Serial_Number=@m_serial_number, Head_Color=@head_color, " +
                    "Breast_Color=@breast_color, Body_Color=@body_color where Serial_Number=@serial_number";
                command.Parameters.Add("@species", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Species;
                command.Parameters.Add("@sub_species", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Sub_species;
                command.Parameters.Add("@hatch_date", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Hatch_date;
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Gender;
                command.Parameters.Add("@cage_number", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Cage_number;
                command.Parameters.Add("@f_serial_number", SqlDbType.Int).Value = ladyGouldianFinchModel.F_serial_number;
                command.Parameters.Add("@m_serial_number", SqlDbType.Int).Value = ladyGouldianFinchModel.M_serial_number;
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = ladyGouldianFinchModel.Serial_number;
                command.Parameters.Add("@head_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Head_color;
                command.Parameters.Add("@breast_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Breast_color;
                command.Parameters.Add("@body_color", SqlDbType.NVarChar).Value = ladyGouldianFinchModel.Body_color;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<LadyGouldianFinchModel> GetAll()
        {
            var ladyGouldianFinchList = new List<LadyGouldianFinchModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from LadyGouldianFinch order by Serial_number desc";
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var ladyGouldianFinchModel = new LadyGouldianFinchModel();
                        ladyGouldianFinchModel.Serial_number = (int)reader[0];
                        ladyGouldianFinchModel.Species = reader[1].ToString();
                        ladyGouldianFinchModel.Sub_species = reader[2].ToString();
                        ladyGouldianFinchModel.Hatch_date = reader[3].ToString();
                        ladyGouldianFinchModel.Gender = reader[4].ToString();
                        ladyGouldianFinchModel.Cage_number = reader[5].ToString();
                        ladyGouldianFinchModel.F_serial_number = (int)reader[6];
                        ladyGouldianFinchModel.M_serial_number = (int)reader[7];
                        ladyGouldianFinchModel.Head_color = reader[8].ToString();
                        ladyGouldianFinchModel.Breast_color = reader[9].ToString();
                        ladyGouldianFinchModel.Body_color = reader[10].ToString();
                        ladyGouldianFinchList.Add(ladyGouldianFinchModel);
                    }
                }
            }
            return ladyGouldianFinchList;
        }

        public IEnumerable<LadyGouldianFinchModel> GetByValue(string value)
        {
            var ladyGouldianFinchList = new List<LadyGouldianFinchModel>();
            int serial_number = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string species = value;
            string hatch_date = value;
            string gender = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from LadyGouldianFinch
                                        where Serial_Number=@serial_number
                                        or Species like @species+'%'
                                        or Hatch_Date like @hatch_date+'%'
                                        or Gender like @gender+'%'
                                        order by Serial_Number desc";
                command.Parameters.Add("@serial_number", SqlDbType.Int).Value = serial_number;
                command.Parameters.Add("@species", SqlDbType.NVarChar).Value = species;
                command.Parameters.Add("@hatch_date", SqlDbType.NVarChar).Value = hatch_date;
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ladyGouldianFinchModel = new LadyGouldianFinchModel();
                        ladyGouldianFinchModel.Serial_number = (int)reader[0];
                        ladyGouldianFinchModel.Species = reader[1].ToString();
                        ladyGouldianFinchModel.Sub_species = reader[2].ToString();
                        ladyGouldianFinchModel.Hatch_date = reader[3].ToString();
                        ladyGouldianFinchModel.Gender = reader[4].ToString();
                        ladyGouldianFinchModel.Cage_number = reader[5].ToString();
                        ladyGouldianFinchModel.F_serial_number = (int)reader[6];
                        ladyGouldianFinchModel.M_serial_number = (int)reader[7];
                        ladyGouldianFinchModel.Head_color = reader[8].ToString();
                        ladyGouldianFinchModel.Breast_color = reader[9].ToString();
                        ladyGouldianFinchModel.Body_color = reader[10].ToString();
                        ladyGouldianFinchList.Add(ladyGouldianFinchModel);
                    }
                }
            }
            return ladyGouldianFinchList;
        }
    }
}