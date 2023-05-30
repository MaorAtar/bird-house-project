<br/>
<p align="center">
  <h3 align="center">Bird House Project</h3>

  <p align="center">
    C# Program for Lady Gouldian Finch birds habitat
    <br/>
    <br/>
  </p>
</p>

## About The Project

![Screen Shot](https://www.birdsville.net.au/wp-content/uploads/2011/07/Gouldian-finch1.jpg
)

C# program for a Lady Gouldian Finch birds habitat managment system,
using SQL Server as a data base.

### Installation

1. Add the files inside the "Files To Add" folder to the BirdHouseProject folder within the main project folder (BirdHouseProject-master).
Download Link: https://f2h.io/xe2l4aj7cjcy

2. Make sure you have SQL Server installed on your computer.

3. Run the project by clicking on the file "BirdHouseProject.sln".

4. Make sure you can update packages automatically:
   Tools --> NuGet Package Manager --> Package Manager Settings.

5. There are several code changes to be made:
   
Inside the Properties folder:
   - Open Settings.settings file.
   - Double-click on the Value column and change **TO-DO** to your SQL Server server name.

   Inside the Views folder:
   - Open CageDataView file.
   - Right-click and select View Code.
   - On Line 16, change **TO-DO** to your SQL Server server name.
   - Repeat the above steps for CageView, LadyGouldianFinchDataView, LadyGouldianFinchView, and MainView files.

   Inside the main project folder:
   - Open the Program file.
   - Double-click on Line 11, 31 and change **TO-DO** to your SQL Server server name.
   - Open the App.config file.
   - Double-click on Line 6 and 8, and change **TO-DO** to your SQL Server server name.

6. Click on Build, then Build Solution, and finally, run the program.

## Usage

![Screen Shot](https://i.ibb.co/fr3jPzc/BHPMain.png)
![Screen Shot](https://i.ibb.co/7tbLg77/BHPView.png)

## Authors

* **Maor Atar** - *Software Engineer Student at Shamoon College of Engenering - SCE* - [Maor Atar](https://github.com/MaorAtar/)
* **Guy Ezra** - *Software Engineer Student at Shamoon College of Engenering - SCE* - [Guy Ezra](https://github.com/GuyEzra22/)
* **Yair Eliyahu** - *Software Engineer Student at Shamoon College of Engenering - SCE* - [Yair Eliyahu](https://github.com/YairEliyahu/)
* **Liav Maman** - *Software Engineer Student at Shamoon College of Engenering - SCE* - [Liav Maman](https://github.com/liav11maman/)
