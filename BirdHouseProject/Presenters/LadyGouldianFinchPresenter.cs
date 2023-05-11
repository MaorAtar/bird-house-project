using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BirdHouseProject.Models;
using BirdHouseProject.Views;

namespace BirdHouseProject.Presenters
{
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

        private void LoadAllLadyGouldianFinchList()
        {
            ladyGouldianFinchList = repository.GetAll();
            ladyGouldianFinchBindingSource.DataSource = ladyGouldianFinchList; // Set data source
        }

        private void SearchLadyGouldianFinch(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                ladyGouldianFinchList = repository.GetByValue(this.view.SearchValue);
            else ladyGouldianFinchList = repository.GetAll();
            ladyGouldianFinchBindingSource.DataSource = ladyGouldianFinchList;
        }

        private void AddNewLadyGouldianFinch(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

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

        private void SaveLadyGouldianFinch(object sender, EventArgs e)
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
                if(view.IsEdit) // Edit model
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

        private void CleanViewFields()
        {
            view.LadyGouldianFinchSerialNumber = "0";
            view.LadyGouldianFinchSpecies = "";
            view.LadyGouldianFinchSubSpecies = "";
            view.LadyGouldianFinchHatchDate = "";
            view.LadyGouldianFinchGender = "";
            view.LadyGouldianFinchCageNumber = "";
            view.LadyGouldianFinchFSerialNumber = "0";
            view.LadyGouldianFinchMSerialNumber = "0";
            view.LadyGouldianFinchHeadColor = "";
            view.LadyGouldianFinchBreastColor = "";
            view.LadyGouldianFinchBodyColor = "";
        }

        private void DeleteSelectedLadyGouldianFinch(object sender, EventArgs e)
        {
            try
            {
                var ladyGouldianFinch = (LadyGouldianFinchModel)ladyGouldianFinchBindingSource.Current;
                repository.Delete(ladyGouldianFinch.Serial_number);
                view.IsSuccessful = true;
                view.Message = "Bird deleted successfully";
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete bird";
            }
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

    }
}
