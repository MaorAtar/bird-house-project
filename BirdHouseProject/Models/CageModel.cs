using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BirdHouseProject.Models
{
    // Represents a model for a bird cage.
    class CageModel
    {
        // Fields
        [Required]
        private int serial_number;
        private double length;
        private double width;
        private double height;
        private string material;

        // Properties - Validations

        // Cage Serial Number
        [DisplayName("Serial Number")]
        public int Serial_number { get => serial_number; set => serial_number = value; }

        // Cage Length
        [DisplayName("Length")]
        public double Length { get => length; set => length = value; }

        // Cage Width
        [DisplayName("Width")]
        public double Width { get => width; set => width = value; }

        // Cage Height
        [DisplayName("Height")]
        public double Height { get => height; set => height = value; }

        // Cage Material
        [DisplayName("Material")]
        public string Material { get => material; set => material = value; }
    }
}
