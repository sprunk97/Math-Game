using System.Windows;

namespace MathGame
{
    /// <summary>
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result(double score, int right, int wrong, int minutes)
        {
            InitializeComponent();
            ResultLabel.Content = $"You got {right} right and {wrong} wrong answers.\rYour score is {right - wrong} / {minutes} min = {score:F3}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
