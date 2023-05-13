using System.Collections.Generic;

namespace BirdHouseProject.Models
{
    // Represents a repository interface for managing bird cages.
    interface ICageRepository
    {
        // Adds a new cage to the repository.
        void Add(CageModel cageModel);

        // Edits an existing cage in the repository.
        void Edit(CageModel cageModel);

        // Deletes a cage from the repository based on the serial number.
        void Delete(int serial_number);

        // Retrieves all cages from the repository.
        IEnumerable<CageModel> GetAll();

        // Retrieves cages from the repository based on a search value.
        IEnumerable<CageModel> GetByValue(string value);
    }
}
