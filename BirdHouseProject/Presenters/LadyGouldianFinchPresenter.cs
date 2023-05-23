using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Presenters
{
    /// <summary>
    /// Presenter for the Lady Gouldian Finch view.
    /// Handles the communication between the view and the repository.
    /// </summary>
    public class LadyGouldianFinchPresenter
    {
        // Fields
        private ILadyGouldianFinchView view;
        private ILadyGouldianFinchRepository repository;
        private BindingSource ladyGouldianFinchBindingSource;
        private IEnumerable<LadyGouldianFinchModel> ladyGouldianFinchList;

        // Constructor
        public LadyGouldianFinchPresenter(ILadyGouldianFinchView view, ILadyGouldianFinchRepository repository)
        {
            this.ladyGouldianFinchBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            // Subscribe event handler methods to view events
            this.view.SearchEvent += SearchLadyGouldianFinch;
            this.view.AddNewEvent += AddNewLadyGouldianFinch;
            this.view.EditEvent += LoadSelectedLadyGouldianFinchToEdit;
            this.view.DeleteEvent += DeleteSelectedLadyGouldianFinch;
            this.view.SaveEvent += SaveLadyGouldianFinch;
            this.view.CancelEvent += CancelAction;
            // Set LadyGouldianFinch binding source
            this.view.SetLadyGouldianFinchBindingSource(ladyGouldianFinchBindingSource);
            // Load LadyGouldianFinch list view
            LoadAllLadyGouldianFinchList();
            // Show view
            this.view.Show();
        }

        /// <summary>
        /// Loads all Lady Gouldian Finch models from the repository and sets the data source for the view.
        /// </summary>
        private void LoadAllLadyGouldianFinchList()
        {
            ladyGouldianFinchList = repository.GetAll();
            ladyGouldianFinchBindingSource.DataSource = ladyGouldianFinchList;
        }

        /// <summary>
        /// Event handler for searching Lady Gouldian Finch models.
        /// Retrieves Lady Gouldian Finch models from the repository based on the search value and binds them to the view.
        /// </summary>
        private void SearchLadyGouldianFinch(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                ladyGouldianFinchList = repository.GetByValue(this.view.SearchValue);
            else
                ladyGouldianFinchList = repository.GetAll();
            ladyGouldianFinchBindingSource.DataSource = ladyGouldianFinchList;
        }

        /// <summary>
        /// Event handler for adding a new Lady Gouldian Finch.
        /// Sets the view to add mode.
        /// </summary>
        private void AddNewLadyGouldianFinch(object sender, EventArgs e)
        {
            CleanViewFields();
            view.IsEdit = false;
        }

        /// <summary>
        /// Event handler for loading the selected Lady Gouldian Finch model to edit.
        /// Populates the view fields with the data of the selected Lady Gouldian Finch model.
        /// </summary>
        private void LoadSelectedLadyGouldianFinchToEdit(object sender, EventArgs e)
        {
            var ladyGouldianFinch = (LadyGouldianFinchModel)ladyGouldianFinchBindingSource.Current;
            view.LadyGouldianFinchSerialNumber = ladyGouldianFinch.Serial_number.ToString();
            view.LadyGouldianFinchSpecies = ladyGouldianFinch.Species;
            view.LadyGouldianFinchSubSpecies = ladyGouldianFinch.Sub_species;
            view.LadyGouldianFinchHatchDate = ladyGouldianFinch.Hatch_date;
            view.LadyGouldianFinchGender = ladyGouldianFinch.Gender;
            view.LadyGouldianFinchCageNumber = ladyGouldianFinch.Cage_number;
            view.LadyGouldianFinchFSerialNumber = ladyGouldianFinch.F_serial_number.ToString();
            view.LadyGouldianFinchMSerialNumber = ladyGouldianFinch.M_serial_number.ToString();
            view.LadyGouldianFinchHeadColor = ladyGouldianFinch.Head_color;
            view.LadyGouldianFinchBreastColor = ladyGouldianFinch.Breast_color;
            view.LadyGouldianFinchBodyColor = ladyGouldianFinch.Body_color;
            view.IsEdit = true;
        }

        /// <summary>
        /// Event handler for saving the Lady Gouldian Finch model.
        /// Validates the model data and either adds a new model or edits an existing one in the repository.
        /// </summary>
        private void SaveLadyGouldianFinch(object sender, EventArgs e)
        {
            bool flag = CheckValidSerialNumbers(view.LadyGouldianFinchFSerialNumber, view.LadyGouldianFinchMSerialNumber);
            if (flag == true)
            {
                var model = new LadyGouldianFinchModel();
                model.Serial_number = Convert.ToInt32(view.LadyGouldianFinchSerialNumber);
                model.Species = view.LadyGouldianFinchSpecies;
                model.Sub_species = view.LadyGouldianFinchSubSpecies;
                model.Hatch_date = view.LadyGouldianFinchHatchDate;
                model.Gender = view.LadyGouldianFinchGender;
                model.Cage_number = view.LadyGouldianFinchCageNumber;
                model.F_serial_number = Convert.ToInt32(view.LadyGouldianFinchFSerialNumber);
                model.M_serial_number = Convert.ToInt32(view.LadyGouldianFinchMSerialNumber);
                model.Head_color = view.LadyGouldianFinchHeadColor;
                model.Breast_color = view.LadyGouldianFinchBreastColor;
                model.Body_color = view.LadyGouldianFinchBodyColor;
                try
                {
                    new Common.ModelDataValidation().Validate(model);
                    if (view.IsEdit) // Edit model
                    {
                        repository.Edit(model);
                        view.Message = "Bird edited successfully";
                    }
                    else // Add new model
                    {
                        repository.Add(model);
                        view.Message = "Bird added successfully";
                    }
                    view.IsSuccessful = true;
                    LoadAllLadyGouldianFinchList();
                    CleanViewFields();
                }
                catch (Exception ex)
                {
                    view.IsSuccessful = false;
                    view.Message = ex.Message;
                }
            }
        }

        /// <summary>
        /// Checks if the father and mother serial numbers entered are valid.
        /// </summary>
        /// <param name="f_serial_number"></param>
        /// <param name="m_serial_number"></param>
        /// <returns></returns>
        private bool CheckValidSerialNumbers(string f_serial_number, string m_serial_number)
        {
            int result = 0;
            if (!(int.TryParse(f_serial_number, out result)) || !(int.TryParse(m_serial_number, out result))) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clears the view fields.
        /// </summary>
        private void CleanViewFields()
        {
            view.LadyGouldianFinchSerialNumber = "0";
            view.LadyGouldianFinchSpecies = "";
            view.LadyGouldianFinchSubSpecies = "";
            view.LadyGouldianFinchHatchDate = "";
            view.LadyGouldianFinchGender = "";
            view.LadyGouldianFinchCageNumber = "";
            view.LadyGouldianFinchFSerialNumber = "";
            view.LadyGouldianFinchMSerialNumber = "";
            view.LadyGouldianFinchHeadColor = "";
            view.LadyGouldianFinchBreastColor = "";
            view.LadyGouldianFinchBodyColor = "";
        }

        /// <summary>
        /// Event handler for deleting the selected Lady Gouldian Finch model.
        /// Deletes the model from the repository.
        /// </summary>
        private void DeleteSelectedLadyGouldianFinch(object sender, EventArgs e)
        {
            try
            {
                var ladyGouldianFinch = ladyGouldianFinchBindingSource.Current as LadyGouldianFinchModel;
                if (ladyGouldianFinch != null)
                {
                    repository.Delete(ladyGouldianFinch.Serial_number);
                    view.IsSuccessful = true;
                    view.Message = "Bird deleted successfully";
                }
                else
                {
                    view.IsSuccessful = false;
                    view.Message = "No bird selected";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occurred - " + ex.Message + ", could not delete bird";
            }

        }

        /// <summary>
        /// Event handler for canceling the current action.
        /// Clears the view fields.
        /// </summary>
        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }
    }
}