using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace BlurayCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variables to store file paths
        private string currentVideoFile = "";
        private string[] audioTracks = { };
        private string[] subtitleTracks = { };

        public MainWindow()
        {
            InitializeComponent();
            UpdateStatus("Application started successfully");
        }

        /// <summary>
        /// Import Video File
        /// </summary>
        private void ImportVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.mkv;*.avi;*.mov;*.flv;*.wmv)|*.mp4;*.mkv;*.avi;*.mov;*.flv;*.wmv|All Files (*.*)|*.*";
            openFileDialog.Title = "Select Video File";

            if (openFileDialog.ShowDialog() == true)
            {
                currentVideoFile = openFileDialog.FileName;
                AddFileToList("Video: " + System.IO.Path.GetFileName(currentVideoFile));
                UpdateStatus($"Video loaded: {System.IO.Path.GetFileName(currentVideoFile)}");
                UpdateInfo();
            }
        }

        /// <summary>
        /// Add Audio Track
        /// </summary>
        private void AddAudio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentVideoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files (*.mp3;*.aac;*.flac;*.wav;*.ac3;*.dts)|*.mp3;*.aac;*.flac;*.wav;*.ac3;*.dts|All Files (*.*)|*.*";
            openFileDialog.Title = "Select Audio File";

            if (openFileDialog.ShowDialog() == true)
            {
                Array.Resize(ref audioTracks, audioTracks.Length + 1);
                audioTracks[audioTracks.Length - 1] = openFileDialog.FileName;
                AddFileToList("Audio: " + System.IO.Path.GetFileName(openFileDialog.FileName));
                UpdateStatus($"Audio track added: {System.IO.Path.GetFileName(openFileDialog.FileName)}");
            }
        }

        /// <summary>
        /// Add Subtitle
        /// </summary>
        private void AddSubtitle_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentVideoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Subtitle Files (*.srt;*.sub;*.ass;*.ssa;*.sup)|*.srt;*.sub;*.ass;*.ssa;*.sup|All Files (*.*)|*.*";
            openFileDialog.Title = "Select Subtitle File";

            if (openFileDialog.ShowDialog() == true)
            {
                Array.Resize(ref subtitleTracks, subtitleTracks.Length + 1);
                subtitleTracks[subtitleTracks.Length - 1] = openFileDialog.FileName;
                AddFileToList("Subtitle: " + System.IO.Path.GetFileName(openFileDialog.FileName));
                UpdateStatus($"Subtitle added: {System.IO.Path.GetFileName(openFileDialog.FileName)}");
            }
        }

        /// <summary>
        /// Preview the project
        /// </summary>
        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentVideoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Preview: {System.IO.Path.GetFileName(currentVideoFile)}\n\n" +
                            $"Audio Tracks: {audioTracks.Length}\n" +
                            $"Subtitles: {subtitleTracks.Length}",
                            "Project Preview", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateStatus("Preview generated");
        }

        /// <summary>
        /// Burn Disc
        /// </summary>
        private void BurnDisc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentVideoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Burn Disc feature coming soon!\n\nThis will:\n" +
                            "1. Create Blu-ray disc structure\n" +
                            "2. Generate ISO file\n" +
                            "3. Write to physical disc",
                            "Burn Disc", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateStatus("Burn process initialized");
        }

        /// <summary>
        /// Add file to list
        /// </summary>
        private void AddFileToList(string fileName)
        {
            if (FileList.Items.Count == 1 && FileList.Items[0].ToString().Contains("No files loaded"))
            {
                FileList.Items.Clear();
            }
            FileList.Items.Add(fileName);
        }

        /// <summary>
        /// Update status bar
        /// </summary>
        private void UpdateStatus(string message)
        {
            StatusText.Text = $"Status: {message} | {DateTime.Now:HH:mm:ss}";
        }

        /// <summary>
        /// Update project information
        /// </summary>
        private void UpdateInfo()
        {
            string videoName = string.IsNullOrEmpty(currentVideoFile) ? "Not loaded" : System.IO.Path.GetFileName(currentVideoFile);
            InfoText.Text = $"Project: Blu-ray Creator\n" +
                           $"Video: {videoName}\n" +
                           $"Duration: --:--:-- (Analysis pending)\n" +
                           $"Resolution: 1920x1080 (HD)\n" +
                           $"Frame Rate: 23.976 fps";
        }
    }
}
