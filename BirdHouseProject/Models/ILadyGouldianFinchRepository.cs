using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouseProject.Models
{
    public interface ILadyGouldianFinchRepository
    {
        void Add(LadyGouldianFinchModel ladyGouldianFinchModel);
        void Edit(LadyGouldianFinchModel ladyGouldianFinchModel);
        void Delete(int serial_number);

        // Searches Options
        IEnumerable<LadyGouldianFinchModel> GetAll();
        IEnumerable<LadyGouldianFinchModel> GetByValue(string value);
    }
}
