using System;
using System.Net;
using System.Windows;
using CrashReporter;

namespace MathGame
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            AppDomain currentDomain = default(AppDomain);
            currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
            System.Windows.Forms.Application.ThreadException += GlobalThreadExceptionHandler;

            var application = new App();
            application.InitializeComponent();
            application.Run();
        }

        private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = default(Exception);
            ex = (Exception)e.ExceptionObject;
            MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            NetworkCredential credential = new NetworkCredential(MathGame.Properties.Settings.Default.email, MathGame.Properties.Settings.Default.email_password);
            var mail = new Sender(credential, "sprunk97@gmail.com", "MathGame Exception", null, null);
            try
            {
                mail.SendReport(ex);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            Environment.Exit(0);
        }

        private static void GlobalThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = default(Exception);
            ex = e.Exception;
            MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            NetworkCredential credential = new NetworkCredential(MathGame.Properties.Settings.Default.email, MathGame.Properties.Settings.Default.email_password);
            var mail = new Sender(credential, "sprunk97@gmail.com", "MathGame Exception", null, null);
            try
            {
                mail.SendReport(ex);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            Environment.Exit(0);
        }
    }
}
