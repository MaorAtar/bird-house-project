using System;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents the view interface for managing Lady Gouldian Finch in the Bird House project.
    /// </summary>
    public interface ILadyGouldianFinchView
    {
        // Properties - Fields
        string LadyGouldianFinchSerialNumber { get; set; }
        string LadyGouldianFinchSpecies { get; set; }
        string LadyGouldianFinchSubSpecies { get; set; }
        string LadyGouldianFinchHatchDate { get; set; }
        string LadyGouldianFinchGender { get; set; }
        string LadyGouldianFinchCageNumber { get; set; }
        string LadyGouldianFinchFSerialNumber { get; set; }
        string LadyGouldianFinchMSerialNumber { get; set; }
        string LadyGouldianFinchHeadColor { get; set; }
        string LadyGouldianFinchBreastColor { get; set; }
        string LadyGouldianFinchBodyColor { get; set; }

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
        void SetLadyGouldianFinchBindingSource(BindingSource ladyGouldianFinchList);
        void Show(); // Optional
    }
}
