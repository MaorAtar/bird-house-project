using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouseProject.Models
{
    interface ICageRepository
    {
        void Add(CageModel cageModel);
        void Edit(CageModel cageModel);
        void Delete(int serial_number);

        // Searches Options
        IEnumerable<CageModel> GetAll();
        IEnumerable<CageModel> GetByValue(string value);
    }
}
