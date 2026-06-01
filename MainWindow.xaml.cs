using System;
using System.Windows;
using Microsoft.Win32;

namespace BlurayCreator
{
    public partial class MainWindow : Window
    {
        private string videoFile = "";
        private string[] audioFiles = { };
        private string[] subtitleFiles = { };

        public MainWindow()
        {
            InitializeComponent();
            UpdateStatus("Application started");
        }

        private void ImportVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Video Files|*.mp4;*.mkv;*.avi;*.mov|All Files|*.*";
            
            if (dialog.ShowDialog() == true)
            {
                videoFile = dialog.FileName;
                AddToList("Video: " + System.IO.Path.GetFileName(videoFile));
                UpdateStatus("Video loaded: " + System.IO.Path.GetFileName(videoFile));
            }
        }

        private void AddAudio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(videoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files|*.mp3;*.aac;*.wav;*.ac3|All Files|*.*";
            
            if (dialog.ShowDialog() == true)
            {
                Array.Resize(ref audioFiles, audioFiles.Length + 1);
                audioFiles[audioFiles.Length - 1] = dialog.FileName;
                AddToList("Audio: " + System.IO.Path.GetFileName(dialog.FileName));
                UpdateStatus("Audio track added");
            }
        }

        private void AddSubtitle_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(videoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Subtitle Files|*.srt;*.sub;*.ass|All Files|*.*";
            
            if (dialog.ShowDialog() == true)
            {
                Array.Resize(ref subtitleFiles, subtitleFiles.Length + 1);
                subtitleFiles[subtitleFiles.Length - 1] = dialog.FileName;
                AddToList("Subtitle: " + System.IO.Path.GetFileName(dialog.FileName));
                UpdateStatus("Subtitle added");
            }
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(videoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning");
                return;
            }

            string info = $"Video: {System.IO.Path.GetFileName(videoFile)}\n";
            info += $"Audio Tracks: {audioFiles.Length}\n";
            info += $"Subtitles: {subtitleFiles.Length}";
            MessageBox.Show(info, "Project Preview");
        }

        private void BurnDisc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(videoFile))
            {
                MessageBox.Show("Please load a video first!", "Warning");
                return;
            }

            MessageBox.Show("Burn feature coming soon!", "Burn Disc");
        }

        private void AddToList(string item)
        {
            if (FileList.Items.Count == 1 && FileList.Items[0].ToString().Contains("No files"))
            {
                FileList.Items.Clear();
            }
            FileList.Items.Add(item);
        }

        private void UpdateStatus(string message)
        {
            StatusText.Text = "✓ " + message;
        }
    }
}
