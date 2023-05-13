using System;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents the main view interface for the Bird House project.
    /// </summary>
    public interface IMainView
    {
        event EventHandler ShowLadyGouldianFinchView;
        event EventHandler ShowCageView;
    }
}
