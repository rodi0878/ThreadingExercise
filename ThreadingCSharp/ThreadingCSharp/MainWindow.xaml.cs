using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace ThreadingCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlgorithmBase algorithm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Prepare()
        {
            algorithm = new Mandelbrot();
            algorithm.Prepare();

            algorithm.StartTimeMeasurement();
        }

        private void Finish()
        {
            algorithm.StopTimeMeasurement();

            DurationText.Text = algorithm.Duration.ToString();
        }

        private void SyncClick(object sender, RoutedEventArgs e)
        {
            Prepare();
            algorithm.Solve();
            Finish();

            ImageBox.Source = algorithm.GetWPFBitmapSource();
        }

        private void ThreadClick(object sender, RoutedEventArgs e)
        {
            // TODO: solve problem using Thread class (thread for each image row)
            // TODO: solve problem using Thread class (thread for each processor)
            // TODO: solve problem using thread pool
            // TODO: try to solve using Task.Run, async/await
            // TODO: try to solve using Parallel

            // IMPORTANT: be aware of synchronization issues and blocking of UI thread
        }

        private void Thread2Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
