using System;
using BirdHouseProject.Models;
using BirdHouseProject.Views;
using BirdHouseProject.Repositories;

namespace BirdHouseProject.Presenters
{
    /// <summary>
    /// Presenter for the main view of the BirdHouseProject application.
    /// </summary>
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        /// <summary>
        /// Initializes a new instance of the MainPresenter class.
        /// </summary>
        /// <param name="mainView">The main view of the application.</param>
        /// <param name="sqlConnectionString">The SQL connection string for the database.</param>
        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowLadyGouldianFinchView += ShowLadyGouldianFinchsView;
            this.mainView.ShowCageView += ShowCagesView;
        }

        /// <summary>
        /// Event handler for showing the LadyGouldianFinchView.
        /// </summary>
        private void ShowLadyGouldianFinchsView(object sender, EventArgs e)
        {
            ILadyGouldianFinchView view = LadyGouldianFinchView.GetInstance((MainView)mainView);
            ILadyGouldianFinchRepository repository = new LadyGouldianFinchRepository(sqlConnectionString);
            new LadyGouldianFinchPresenter(view, repository);
        }

        /// <summary>
        /// Event handler for showing the CageView.
        /// </summary>
        private void ShowCagesView(object sender, EventArgs e)
        {
            ICageView view = CageView.GetInstance((MainView)mainView);
            ICageRepository repository = new CageRepository(sqlConnectionString);
            new CagePresenter(view, repository);
        }
    }
}
