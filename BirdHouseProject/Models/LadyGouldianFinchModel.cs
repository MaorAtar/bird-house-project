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
        [DisplayName("Serial Number")]
        public int Serial_number { get => serial_number; set => serial_number = value; }

        // Bird Species
        [DisplayName("Species")]
        public string Species { get => species; set => species = value; }

        // Bird Sub Species
        [DisplayName("Sub Species")]
        public string Sub_species { get => sub_species; set => sub_species = value; }

        // Bird Hatch Date
        [DisplayName("Hatch Date")]
        public string Hatch_date { get => hatch_date; set => hatch_date = value; }

        // Bird Gender
        [DisplayName("Gender")]
        public string Gender { get => gender; set => gender = value; }

        // Cage Number
        [DisplayName("Cage Number")]
        public string Cage_number { get => cage_number; set => cage_number = value; }

        // Bird Father Serial Number
        [DisplayName("Father Serial Number")]
        public int F_serial_number { get => f_serial_number; set => f_serial_number = value; }

        // Bird Mother Serial Number
        [DisplayName("Mother Serial Number")]
        public int M_serial_number { get => m_serial_number; set => m_serial_number = value; }

        // Bird Head Color
        [DisplayName("Head Color")]
        public string Head_color { get => head_color; set => head_color = value; }

        // Bird Breast Color
        [DisplayName("Breast Color")]
        public string Breast_color { get => breast_color; set => breast_color = value; }

        // Bird Body Color
        [DisplayName("Body Color")]
        public string Body_color { get => body_color; set => body_color = value; }
    }
}
