using System;
using System.Collections.Generic;
using System.Text;
using BirdHouseProject.Models;
using BirdHouseProject.Views;
using BirdHouseProject.Repositories;
using System.Windows.Forms;

namespace BirdHouseProject.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowLadyGouldianFinchView += ShowLadyGouldianFinchsView;
            this.mainView.ShowCageView += ShowCagesView;
        }

        private void ShowLadyGouldianFinchsView(object sender, EventArgs e)
        {
            ILadyGouldianFinchView view = LadyGouldianFinchView.GetInstance((MainView)mainView);
            ILadyGouldianFinchRepository repository = new LadyGouldianFinchRepository(sqlConnectionString);
            new LadyGouldianFinchPresenter(view, repository);
        }

        private void ShowCagesView(object sender, EventArgs e)
        {
            ICageView view = CageView.GetInstance((MainView)mainView);
            ICageRepository repository = new CageRepository(sqlConnectionString);
            new CagePresenter(view, repository);
        }
    }
}
