using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Presenters
{
    /// <summary>
    /// Presenter class for the Cage view in the BirdHouse project.
    /// Handles events and interacts with the view and repository to perform operations on Cage models.
    /// </summary>
    class CagePresenter
    {
        // Fields
        private ICageView view;
        private ICageRepository repository;
        private BindingSource cageBindingSource;
        private IEnumerable<CageModel> cageList;

        /// <summary>
        /// Initializes a new instance of the CagePresenter class with the specified view and repository.
        /// </summary>
        /// <param name="view">The Cage view interface.</param>
        /// <param name="repository">The repository for accessing and manipulating Cage data.</param>
        public CagePresenter(ICageView view, ICageRepository repository)
        {
            this.cageBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // Subscribe event handler methods to view events
            this.view.SearchEvent += SearchCage;
            this.view.AddNewEvent += AddNewCage;
            this.view.EditEvent += LoadSelectedCageToEdit;
            this.view.DeleteEvent += DeleteSelectedCage;
            this.view.SaveEvent += SaveCage;
            this.view.CancelEvent += CancelAction;

            // Set Cage binding source
            this.view.SetCageBindingSource(cageBindingSource);

            // Load Cage list view
            LoadAllCageList();

            // Show view
            this.view.Show();
        }

        /// <summary>
        /// Loads all Cage models from the repository and binds them to the view.
        /// </summary>
        private void LoadAllCageList()
        {
            cageList = repository.GetAll();
            cageBindingSource.DataSource = cageList; // Set data source
        }

        /// <summary>
        /// Event handler for the cancel action event.
        /// Cleans the view fields.
        /// </summary>
        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        /// <summary>
        /// Event handler for the save cage event.
        /// Creates a new Cage model from the view data and saves it to the repository.
        /// </summary>
        private void SaveCage(object sender, EventArgs e)
        {
            bool flag = CheckValidCageValues(view.CageLength, view.CageWidth, view.CageHeight);
            if (flag == true)
            {
                var model = new CageModel();
                model.Serial_number = Convert.ToInt32(view.CageSerialNumber);
                model.Length = Convert.ToDouble(view.CageLength);
                model.Width = Convert.ToDouble(view.CageWidth);
                model.Height = Convert.ToDouble(view.CageHeight);
                model.Material = view.CageMaterial;
                try
                {
                    new Common.ModelDataValidation().Validate(model);
                    if (view.IsEdit) // Edit model
                    {
                        repository.Edit(model);
                        view.Message = "Cage edited successfully";
                    }
                    else // Add new model
                    {
                        repository.Add(model);
                        view.Message = "Cage added successfully";
                    }
                    view.IsSuccessful = true;
                    LoadAllCageList();
                    CleanViewFields();
                }
                catch (Exception ex)
                {
                    view.IsSuccessful = false;
                    view.Message = ex.Message;
                }
            }
        }

        private bool CheckValidCageValues(string length, string width, string height)
        {
            int resultLen, resultWid, resultHei;
            int.TryParse(length, out resultLen);
            int.TryParse(width, out resultWid);
            int.TryParse(height, out resultHei);

            if (resultLen < 15 || resultWid < 15 || resultHei < 15)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Cleans the view fields by resetting them to default values.
        /// </summary>
        private void CleanViewFields()
        {
            view.CageSerialNumber = "0";
            view.CageLength = "0";
            view.CageWidth = "0";
            view.CageHeight = "0";
            view.CageMaterial = "";
        }
        /// <summary>
        /// Event handler for the delete cage event.
        /// Deletes the selected Cage model from the repository.
        /// </summary>
        private void DeleteSelectedCage(object sender, EventArgs e)
        {
            try
            {
                var cage = cageBindingSource.Current as CageModel;
                if (cage != null)
                {
                    repository.Delete(cage.Serial_number);
                    view.IsSuccessful = true;
                    view.Message = "Cage deleted successfully";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred - " + ex.Message + ", could not delete cage";
            }
        }

        /// <summary>
        /// Event handler for loading the selected Cage model to edit.
        /// Populates the view fields with the data of the selected Cage model.
        /// </summary>
        private void LoadSelectedCageToEdit(object sender, EventArgs e)
        {
            var cage = (CageModel)cageBindingSource.Current;
            view.CageSerialNumber = cage.Serial_number.ToString();
            view.CageLength = cage.Length.ToString();
            view.CageWidth = cage.Width.ToString();
            view.CageHeight = cage.Height.ToString();
            view.CageMaterial = cage.Material;
            view.IsEdit = true;
        }

        /// <summary>
        /// Event handler for adding a new Cage.
        /// Sets the view to add mode.
        /// </summary>
        private void AddNewCage(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        /// <summary>
        /// Event handler for searching Cage models.
        /// Retrieves Cage models from the repository based on the search value and binds them to the view.
        /// </summary>
        private void SearchCage(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                cageList = repository.GetByValue(this.view.SearchValue);
            else
                cageList = repository.GetAll();
            cageBindingSource.DataSource = cageList;
        }

    }
}
