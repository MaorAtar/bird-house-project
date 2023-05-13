using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BirdHouseProject.Models
{
    public class LadyGouldianFinchModel
    {
        // Fields
        [Required]
        private int serial_number;
        private string species;
        private string sub_species;
        private string hatch_date;
        private string gender;
        private string cage_number;
        private int f_serial_number;
        private int m_serial_number;
        private string head_color;
        private string breast_color;
        private string body_color;

        // Properties - Validations

        // Bird Serial Number
        [DisplayName("Bird Serial Number")]
        [Required(ErrorMessage = "Serial number is required!")]
        public int Serial_number { get => serial_number; set => serial_number = value; }

        // Bird Species
        [DisplayName("Bird Species")]
        [Required(ErrorMessage = "Bird species is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Bird species must be 3-20 digits long exactly!")]
        public string Species { get => species; set => species = value; }

        // Bird Sub Species
        [DisplayName("Bird Sub Species")]
        [Required(ErrorMessage = "Bird sub species is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Bird sub species must be 3-20 digits long exactly!")]
        public string Sub_species { get => sub_species; set => sub_species = value; }

        // Bird Hatch Date
        [DisplayName("Bird Hatch Date")]
        [Required(ErrorMessage = "Bird hatch date is required!")]
        public string Hatch_date { get => hatch_date; set => hatch_date = value; }

        // Bird Gender
        [DisplayName("Bird Gender")]
        [Required(ErrorMessage = "Bird gender is required!")]
        public string Gender { get => gender; set => gender = value; }

        // Cage Number
        [DisplayName("Cage Number")]
        [Required(ErrorMessage = "Cage number is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Cage number must be 4 digits long exactly!")]
        public string Cage_number { get => cage_number; set => cage_number = value; }

        // Bird Father Serial Number
        [DisplayName("Bird Father Serial Number")]
        [Required(ErrorMessage = "Bird father serial number is required!")]
        public int F_serial_number { get => f_serial_number; set => f_serial_number = value; }

        // Bird Mother Serial Number
        [DisplayName("Bird Mother Serial Number")]
        [Required(ErrorMessage = "Bird mother serial number is required!")]
        public int M_serial_number { get => m_serial_number; set => m_serial_number = value; }

        // Bird Head Color
        [DisplayName("Bird Head Color")]
        [Required(ErrorMessage = "Bird head color is required!")]
        public string Head_color { get => head_color; set => head_color = value; }

        // Bird Breast Color
        [DisplayName("Bird Breast Color")]
        [Required(ErrorMessage = "Bird breast color is required!")]
        public string Breast_color { get => breast_color; set => breast_color = value; }

        // Bird Body Color
        [DisplayName("Bird Body Color")]
        [Required(ErrorMessage = "Bird body color is required!")]
        public string Body_color { get => body_color; set => body_color = value; }
    }
}
