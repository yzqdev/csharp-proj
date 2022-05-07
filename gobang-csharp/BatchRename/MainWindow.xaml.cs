using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void OpenFolderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Multiselect = true;
            dialog.Description = "请选取一个要处理的文件夹";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            dialog.RootFolder =Environment.SpecialFolder.Desktop;
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog(this))
            {
                var selectedPaths = dialog.SelectedPaths;

                if (selectedPaths.Length == 1)
                {
                    CurDirTextBox.Text = selectedPaths[0];
                    string[] files =  GetFolderFiles(new DirectoryInfo(selectedPaths[0]) );
                   
                }
                else
                {
                    MessageBox.Show(this, $"The selected folders were:{Environment.NewLine}{string.Join(Environment.NewLine, selectedPaths)}", "Sample folder browser dialog");
                }
            }
        }
        private string[] GetFolderFiles(DirectoryInfo folder )
        {
            string[] files=new string[9];
            if (folder.Exists)
            {
                foreach (FileInfo path in folder.GetFiles())
                {
                    FileAttributes fileAttributes = path.Attributes & FileAttributes.Hidden;
                    if (fileAttributes!=FileAttributes.Hidden)
                    {
                        
                        files.Append(path.Name);
                        FileListLbox.Items.Add(path.Name);
                    }
                     
                }
                return files;
            }
            else
            {
                return Array.Empty<string>();
            }
        }
    }
}
