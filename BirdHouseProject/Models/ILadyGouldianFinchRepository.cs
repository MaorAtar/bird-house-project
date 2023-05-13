using System.Collections.Generic;

namespace BirdHouseProject.Models
{
    public interface ILadyGouldianFinchRepository
    {
        // Adds a new Lady Gouldian Finch to the repository.
        void Add(LadyGouldianFinchModel ladyGouldianFinchModel);

        // Edits an existing Lady Gouldian Finch in the repository.
        void Edit(LadyGouldianFinchModel ladyGouldianFinchModel);

        // Deletes a Lady Gouldian Finch from the repository based on the serial number.
        void Delete(int serial_number);

        // Retrieves all Lady Gouldian Finches from the repository.
        IEnumerable<LadyGouldianFinchModel> GetAll();

        // Retrieves Lady Gouldian Finches from the repository based on a search value.
        IEnumerable<LadyGouldianFinchModel> GetByValue(string value);
    }
}
