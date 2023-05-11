using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Presenters
{
    class CagePresenter
    {
        // Fields
        private ICageView view;
        private ICageRepository repository;
        private BindingSource cageBindingSource;
        private IEnumerable<CageModel> cageList;

        // Constructor
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

        private void LoadAllCageList()
        {
            cageList = repository.GetAll();
            cageBindingSource.DataSource = cageList; // Set data source
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveCage(object sender, EventArgs e)
        {
            var model = new CageModel();
            model.Serial_nubmer = Convert.ToInt32(view.CageSerialNumber);
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

        private void CleanViewFields()
        {
            view.CageSerialNumber = "0";
            view.CageLength = "0.0";
            view.CageWidth = "0.0";
            view.CageHeight = "0.0";
            view.CageMaterial = "";
        }

        private void DeleteSelectedCage(object sender, EventArgs e)
        {
            try
            {
                var cage = (CageModel)cageBindingSource.Current;
                repository.Delete(cage.Serial_nubmer);
                view.IsSuccessful = true;
                view.Message = "Cage deleted successfully";
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete cage";
            }
        }

        private void LoadSelectedCageToEdit(object sender, EventArgs e)
        {
            var cage = (CageModel)cageBindingSource.Current;
            view.CageSerialNumber = cage.Serial_nubmer.ToString();
            view.CageLength = cage.Length.ToString();
            view.CageWidth = cage.Width.ToString();
            view.CageHeight = cage.Height.ToString();
            view.CageMaterial = cage.Material;
            view.IsEdit = true;
        }

        private void AddNewCage(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void SearchCage(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                cageList = repository.GetByValue(this.view.SearchValue);
            else cageList = repository.GetAll();
            cageBindingSource.DataSource = cageList;
        }
    }
}
