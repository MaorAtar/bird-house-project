using System;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents the view interface for managing cages in the Bird House project.
    /// </summary>
    interface ICageView
    {
        // Properties - Fields
        string CageSerialNumber { get; set; }
        string CageLength { get; set; }
        string CageWidth { get; set; }
        string CageHeight { get; set; }
        string CageMaterial { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        // Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        // Methods
        void SetCageBindingSource(BindingSource cageList);
        void Show(); // Optional
    }
}
