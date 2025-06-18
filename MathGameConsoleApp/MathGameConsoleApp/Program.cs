using MathGameConsoleApp;

var menu = new Menu();

var date = DateTime.Now;

List<string> games = new List<string>();

var name = Helpers.GetName();

menu.ShowMenu(name, date);

