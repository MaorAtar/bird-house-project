using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouseProject.Views
{
    public interface IMainView
    {
        event EventHandler ShowLadyGouldianFinchView;
        event EventHandler ShowCageView;
    }
}
