using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MathGame.Enums;

namespace MathGame
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Game game;
        private TimeSpan gameDuration = new TimeSpan(0, 1, 0);
        private Operations operation;
        private int digitsAmount;
        private ObservableCollection<DataObject> answers = new ObservableCollection<DataObject>();

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            gameDuration -= dispatcherTimer.Interval;
            Timer.Content = $"Time left: {gameDuration}";

            if (gameDuration == new TimeSpan(0, 0, 0))
            {
                dispatcherTimer.Stop();

                game.CountScore();

                Result resultWindow = new Result(game.Score, game.RightAnswers, game.WrongAnswers, game.Minutes)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                BlurEffect objBlur = new BlurEffect
                {
                    Radius = 5
                };
                this.Effect = objBlur;

                resultWindow.ShowDialog();

                this.Effect = null;

                gbDigits.IsEnabled = true;
                gbDuration.IsEnabled = true;
                gbOperations.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameDuration = new TimeSpan(0, 0, 1);
        }

        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && dispatcherTimer.IsEnabled && AnswerBox.Text != "")
            {
                game.CheckAnswer(double.Parse(AnswerBox.Text.Replace('.', ',')));
                AnswersTable.Items.Add(new DataObject() { Answers = game.equations.Last().ToString() });

                game.Add(digitsAmount, operation);

                if (rbAddition.IsChecked == true)
                    TaskLabel.Content = $"How much is {game.equations.Last().Left} + {game.equations.Last().Right}?";
                if (rbSubstraction.IsChecked == true)
                    TaskLabel.Content = $"How much is {game.equations.Last().Left} - {game.equations.Last().Right}?";
                if (rbMultiplication.IsChecked == true)
                    TaskLabel.Content = $"How much is {game.equations.Last().Left} * {game.equations.Last().Right}?";
                if (rbDivision.IsChecked == true)
                    TaskLabel.Content = $"How much is {game.equations.Last().Left} / {game.equations.Last().Right}?";

                AnswersTable.Columns[0].Width = AnswersTable.Width - 8;

                AnswerBox.Text = "";
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (rb10min.IsChecked == true) gameDuration = new TimeSpan(0, 10, 0);
            if (rb5min.IsChecked == true) gameDuration = new TimeSpan(0, 5, 0);
            if (rb3min.IsChecked == true) gameDuration = new TimeSpan(0, 3, 0);

            if (rb2Dig.IsChecked == true) digitsAmount = 2;
            if (rb3Dig.IsChecked == true) digitsAmount = 3;
            if (rb4Dig.IsChecked == true) digitsAmount = 4;

            if (rbAddition.IsChecked == true) operation = Operations.Addition;
            if (rbSubstraction.IsChecked == true) operation = Operations.Subtraction;
            if (rbMultiplication.IsChecked == true) operation = Operations.Multiplication;
            if (rbDivision.IsChecked == true) operation = Operations.Division;

            AnswersTable.Items.Clear();

            game = new Game(gameDuration.Minutes);
            game.Add(digitsAmount, operation);

            if (rbAddition.IsChecked == true)
                TaskLabel.Content = $"How much is {game.equations.Last().Left} + {game.equations.Last().Right}?";
            if (rbSubstraction.IsChecked == true)
                TaskLabel.Content = $"How much is {game.equations.Last().Left} - {game.equations.Last().Right}?";
            if (rbMultiplication.IsChecked == true)
                TaskLabel.Content = $"How much is {game.equations.Last().Left} * {game.equations.Last().Right}?";
            if (rbDivision.IsChecked == true)
                TaskLabel.Content = $"How much is {game.equations.Last().Left} / {game.equations.Last().Right}?";

            AnswerBox.Focus();

            dispatcherTimer.Start();

            gbDigits.IsEnabled = false;
            gbDuration.IsEnabled = false;
            gbOperations.IsEnabled = false;
        }

        private void AnswersTable_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            String str = (sender as DataGrid).Items[(sender as DataGrid).Items.Count - 1].ToString();
        }

        private void AnswerBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                if (!Regex.IsMatch(AnswerBox.Text, @"^[0-9]*([,.]{1}[0-9]*)?$"))
                    AnswerBox.Text = "";
            }
        }
    }
}
