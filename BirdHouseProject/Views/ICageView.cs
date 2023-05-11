using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
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
