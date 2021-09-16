using System;
using System.Data.Odbc;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Win32;

namespace Control_C__Control_V
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cut_CanEx(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txt_Text != null) && (txt_Text.SelectionLength > 0);
        }

        private void Cut_Ex(object sender, ExecutedRoutedEventArgs e)
        {
            txt_Text.Cut();
        }

        private void BtnPaste_Clicked(object sender, RoutedEventArgs e)
        {
            txt_Text.Paste();
        }

        private void BtnSelectAll_Clicked(object sender, RoutedEventArgs e)
        {
            txt_Text.Focus();
            txt_Text.SelectAll();       
        }

        private void Copy_CanEx(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txt_Text != null) && (txt_Text.SelectionLength > 0);
        }

        private void Copy_Ex(object sender, ExecutedRoutedEventArgs e)
        {
            txt_Text.Copy();

        }

        private void SelectAll_CanEx(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = txt_Text != null;
        }

        private void SelectAll_Ex(object sender, ExecutedRoutedEventArgs e)
        {
            txt_Text.SelectAll();
            txt_Text.Focus();
        }

        private void txt_Changed(object sender, TextChangedEventArgs e)
        { 
           if(AutoSave.IsChecked == true)
              File.WriteAllText(lbl_Location.Content.ToString(), txt_Text.Text);
        }

        private void AutoSave_ChekBox(object sender, RoutedEventArgs e)
        {
            var chkBox = sender as CheckBox;
            if (lbl_Location.Content == "")
            {
                MessageBox.Show("Cannot enable autosave without opening text file", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                chkBox.IsChecked = false;
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "Text file(*.txt)|*.txt";

            if(saveFile.ShowDialog() == true)
            {
               File.WriteAllText(saveFile.FileName, txt_Text.Text);
               lbl_Location.Content = saveFile.FileName;
            };

        
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if(openFile.ShowDialog() == true)
            {
                txt_Text.Text = File.ReadAllText(openFile.FileName);
                lbl_Location.Content = openFile.FileName;
            }
        }


    }
}
