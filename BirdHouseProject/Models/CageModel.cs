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
        [DisplayName("Cage Serial Number")]
        [Required(ErrorMessage = "Cage serial number is required!")]
        public int Serial_number { get => serial_number; set => serial_number = value; }

        // Cage Length
        [DisplayName("Cage Length")]
        [Required(ErrorMessage = "Cage length is required!")]
        public double Length { get => length; set => length = value; }

        // Cage Width
        [DisplayName("Cage Width")]
        [Required(ErrorMessage = "Cage width is required!")]
        public double Width { get => width; set => width = value; }

        // Cage Height
        [DisplayName("Cage Height")]
        [Required(ErrorMessage = "Cage height is required!")]
        public double Height { get => height; set => height = value; }

        // Cage Material
        [DisplayName("Cage Material")]
        [Required(ErrorMessage = "Cage material is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Cage material must be 4-7 digits long exactly!")]
        public string Material { get => material; set => material = value; }
    }
}
