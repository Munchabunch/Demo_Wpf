﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Demo_Wpf
{
    /// <summary>
    /// Interaction logic for win_WpfDemo.xaml
    /// </summary>
    public partial class win_WpfDemo : Window
    {
        string Prompt;

        string m_str_OldNumericText = "";
        int m_i_OldNumericCursorPos = 0;

        ObservableCollection<DirectoryEntry> entries = new ObservableCollection<DirectoryEntry>();
        ObservableCollection<DirectoryEntry> subEntries = new ObservableCollection<DirectoryEntry>();

        public win_WpfDemo()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(win_WpfDemo_Loaded);
        }


        #region Event Procedures

        void win_WpfDemo_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "Images/dir.gif", EntryType.Dir);
                entries.Add(d);
            }
            this.list_FolderBrowser.DataContext = entries;

            text_GridDescr.Text = "The most familiar container control is the Grid control. By default, each new Window opened in the Windows Presentation Foundation (WPF) Designer for Visual Studio includes a Grid control. The Grid allows you to position controls within user-definable cells. Controls placed in cells maintain a fixed margin between two or more control edges and cell edges when the Window is resized. For more information about how to set the margins, see How to: Set Margins for a Control in the WPF Designer.\n\n"
                + "When added to a Window, a Grid control consists of a single cell. Additional vertical and horizontal rows can be added in code or in the WPF Designer. For more information, see How to: Add Rows and Columns to a Grid.";

            oListBoxItem_01.Content = "hoom\tplup";
            oListBoxItem_02.Content = "antidisestablishmentarianism\tplup";

            textbox_Scrolling.Text = "The Goops they lick their fingers,\n"
                + "   And the Goops they lick their knives;\n"
                + "They spill their broth on the tablecloth --\n"
                + "   Oh, they lead disgusting lives!\n"
                + "The Goops they talk while eating,\n"
                + "   And loud and fast they chew;\n"
                + "And that is why I’m glad that I\n"
                + "   Am not a Goop -- are you?\n";
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            textbox_Bubbling_Events.AppendText("Window_PreviewKeyDown ''" + e.Key.ToString() + "''\n");
            if (oCheckBox_Bubbling_WindowHandlingEvent.IsChecked == true)
            {
                e.Handled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            textbox_Bubbling_Events.AppendText("Window_KeyDown ''" + e.Key.ToString() + "''\n");
        }

        private void btn_CtrlTmpl_01_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks, Dave.");
        }

        private void btn_CtrlTmpl_02_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I understand, Dave.");
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btn_ImgAuction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Let the bidding begin!");
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Pause();
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Play();
        }

        private void btn_ImgA_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/cat.jpeg");
        }
        private void btn_ImgB_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/field.jpeg");
        }
        private void btn_ImgC_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/palmettos.jpeg");
        }
        private void btn_01_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Source = new Uri(@".\video\breakaway.mpeg", UriKind.Relative);
            oMediaElement_01.Play();
        }
        private void btn_02_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Source = new Uri(@".\video\ham.mp4", UriKind.Relative);
            oMediaElement_01.Play();
        }
        private void btn_03_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Source = new Uri(@".\video\parachute.avi", UriKind.Relative);
            oMediaElement_01.Play();
        }

        private void slider_Margin_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (text_PeripheryDemo != null)
            {
                text_Margin.Text = "Margin " + slider_Margin.Value;
                text_PeripheryDemo.Margin = new Thickness(slider_Margin.Value);
            }
        }

        private void slider_Border_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (text_PeripheryDemo != null)
            {
                text_Border.Text = "Border " + slider_Border.Value;
                text_PeripheryDemo.BorderThickness = new Thickness(slider_Border.Value);
            }
        }

        private void slider_Padding_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (text_PeripheryDemo != null)
            {
                text_Padding.Text = "Padding " + slider_Padding.Value;
                text_PeripheryDemo.Padding = new Thickness(slider_Padding.Value);
            }
        }

        private void list_FolderBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectoryEntry entry = (DirectoryEntry)list_FolderBrowser.Items[list_FolderBrowser.SelectedIndex];

            text_CurrentPath.Text = entry.Fullpath;

            if (entry.Type == EntryType.Dir)
            {
                subEntries.Clear();

                try
                {
                    foreach (string s in Directory.GetDirectories(entry.Fullpath))
                    {
                        DirectoryInfo dir = new DirectoryInfo(s);
                        DirectoryEntry d = new DirectoryEntry(
                            dir.Name, dir.FullName, "<Folder>", "<DIR>",
                            Directory.GetLastWriteTime(s),
                            "i/icon16-folder.ico", EntryType.Dir);
                        subEntries.Add(d);
                    }
                    foreach (string f in Directory.GetFiles(entry.Fullpath))
                    {
                        FileInfo file = new FileInfo(f);
                        DirectoryEntry d = new DirectoryEntry(
                            file.Name, file.FullName, file.Extension, file.Length.ToString(),
                            file.LastWriteTime,
                            "i/icon16-file.ico", EntryType.File);
                        subEntries.Add(d);
                    }
                }
                catch (Exception ex)
                {
                    Prompt = ex.Message;
                    MessageBox.Show(Prompt, "Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    list_Files.DataContext = subEntries;
                }
            }
        }

        private void list_Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // ↑↑ Exit if we don't have a valid selection.
            if (list_Files.SelectedIndex < 0) return;

            DirectoryEntry entry = (DirectoryEntry)list_Files.Items[list_Files.SelectedIndex];

            text_CurrentPath.Text = entry.Fullpath;

            if (entry.Type == EntryType.Dir)
            {
                subEntries.Clear();

                try
                {
                    foreach (string s in Directory.GetDirectories(entry.Fullpath))
                    {
                        DirectoryInfo dir = new DirectoryInfo(s);
                        DirectoryEntry d = new DirectoryEntry(
                            dir.Name, dir.FullName, "<Folder>", "<DIR>",
                            Directory.GetLastWriteTime(s),
                            "Images/folder.gif", EntryType.Dir);
                        subEntries.Add(d);
                    }
                    foreach (string f in Directory.GetFiles(entry.Fullpath))
                    {
                        FileInfo file = new FileInfo(f);
                        DirectoryEntry d = new DirectoryEntry(
                            file.Name, file.FullName, file.Extension, file.Length.ToString(),
                            file.LastWriteTime,
                            "Images/file.gif", EntryType.File);
                        subEntries.Add(d);
                    }
                }
                catch (Exception ex)
                {
                    Prompt = ex.Message;
                    MessageBox.Show(Prompt, "Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    list_Files.DataContext = subEntries;
                }
            }
        }

        private void list_Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Put the proper text into the "FileName" text block:
            if (list_Files.SelectedIndex < 0)
            {
                text_FileName.Text = "";
            }
            else
            {
                DirectoryEntry entry = (DirectoryEntry)list_Files.Items[list_Files.SelectedIndex];
                if (entry.Type == EntryType.Dir) text_FileName.Text = "";
                else text_FileName.Text = entry.Name;
            }
        }

        private void btn_UpOneLevel_Click(object sender, RoutedEventArgs e)
        {
            // DEBUG //
            //Prompt = "#601:\n" + GetParentPath(text_CurrentPath.Text);
            //MessageBox.Show(Prompt, "DEBUG", MessageBoxButton.OK, MessageBoxImage.Information);

            text_CurrentPath.Text = GetParentPath(text_CurrentPath.Text);
        }

        private void text_CurrentPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (list_Files != null)
            {
                subEntries.Clear();

                try
                {
                    if (Directory.Exists(text_CurrentPath.Text))
                    {
                        foreach (string s in Directory.GetDirectories(text_CurrentPath.Text))
                        {
                            DirectoryInfo dir = new DirectoryInfo(s);
                            DirectoryEntry d = new DirectoryEntry(
                                dir.Name, dir.FullName, "<Folder>", "<DIR>",
                                Directory.GetLastWriteTime(s),
                                "Images/folder.gif", EntryType.Dir);
                            subEntries.Add(d);
                        }
                        foreach (string f in Directory.GetFiles(text_CurrentPath.Text))
                        {
                            FileInfo file = new FileInfo(f);
                            DirectoryEntry d = new DirectoryEntry(
                                file.Name, file.FullName, file.Extension, file.Length.ToString(),
                                file.LastWriteTime,
                                "Images/file.gif", EntryType.File);
                            subEntries.Add(d);
                        }

                        list_Files.DataContext = subEntries;
                    }
                }
                catch (Exception ex)
                {
                    Prompt = ex.Message;
                    MessageBox.Show(Prompt, "Problem", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textbox_Numeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            // WORKING //

            bool b_Ok = IsTextNumeric(textbox_Numeric.Text);

            if (b_Ok)
            {
                if (textbox_Numeric.Text != m_str_OldNumericText)
                {
                    m_str_OldNumericText = textbox_Numeric.Text;
                    m_i_OldNumericCursorPos = textbox_Numeric.SelectionStart;
                }
            }
            else
            {
                textbox_Numeric.Text = m_str_OldNumericText;
                textbox_Numeric.SelectionStart = m_i_OldNumericCursorPos;
            }
        }

        private void btn_ImgAddRow_Click(object sender, RoutedEventArgs e)
        {
            int i_Pos = pnl_DynamicDock.Children.Count;

            var oDockPnl_New = new DockPanel();
            oDockPnl_New.LastChildFill = true;

            var myLbl = new TextBox();
            myLbl.Text = (i_Pos - 2).ToString();
            DockPanel.SetDock(myLbl, Dock.Left);
            oDockPnl_New.Children.Add(myLbl);

            var myBtn = new Button();
            myBtn.Name = "btn_Go_" + myLbl.Text;
            myBtn.Content = "Go";
            DockPanel.SetDock(myBtn, Dock.Right);
            myBtn.Click += btn_Go_Click;
            oDockPnl_New.Children.Add(myBtn);

            var myTextBox = new TextBox();
            myTextBox.Width = 60;
            myTextBox.Text = "1";
            oDockPnl_New.Children.Add(myTextBox);
            DockPanel.SetDock(myTextBox, Dock.Right);

            myTextBox = new TextBox();
            oDockPnl_New.Children.Add(myTextBox);

            DockPanel.SetDock(oDockPnl_New, Dock.Top);

            pnl_DynamicDock.Children.Insert(i_Pos - 1, oDockPnl_New);
        }

        private void btn_Go_Click(object sender, RoutedEventArgs e)
        {
            Button btn_Source = e.Source as Button;
            int i_RowNum = Convert.ToInt32(btn_Source.Name.Substring(7));
            DockPanel pnl_Selected = pnl_DynamicDock.Children[i_RowNum + 1] as DockPanel;
            TextBox textbox_Body_Selected = pnl_Selected.Children[3] as TextBox;
            TextBox textbox_RowCount_Selected = pnl_Selected.Children[2] as TextBox;
            string str_Body = textbox_Body_Selected.Text;
            int i_RowCount = Convert.ToInt32(textbox_RowCount_Selected.Text);

            Prompt = "Go #" + i_RowNum + "\n" + i_RowCount + "x: ''" + str_Body + "''";
            MessageBox.Show(Prompt, "WPF Demo", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void btn_SetFocus_01_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(textbox_Focus_01);
        }
        private void btn_SetFocus_02_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(textbox_Focus_02);
        }
        private void btn_SetFocus_03_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(textbox_Focus_03);
        }
        private void btn_SetFocus_04_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(listbox_Focus_04);
        }

        private void btn_ImgBubbling_Click(object sender, RoutedEventArgs e)
        {
            textbox_Bubbling_Events.AppendText("btn_ImgBubbling_Click\n");
        }

        private void Canvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            textbox_Bubbling_Events.AppendText("Canvas_PreviewKeyDown ''" + e.Key.ToString() + "''\n");
            if (oCheckBox_Bubbling_CanvasHandlingEvent.IsChecked == true)
            {
                e.Handled = true;
            }
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            textbox_Bubbling_Events.AppendText("Canvas_KeyDown ''" + e.Key.ToString() + "''\n");
        }

        #endregion Event Procedures


        #region Private Methods

        /// <summary>
        /// Change the source (picture) of an Image control.
        /// </summary>
        /// <param name="img_ToChange">image control</param>
        /// <param name="PathedFileName_NewImg">absolute or relative path and file name of the new image</param>
        private void ChangeImgSource(System.Windows.Controls.Image img_ToChange, string PathedFileName_NewImg)
        {
            BitmapImage oBitmapImg_01 = new BitmapImage();
            oBitmapImg_01.BeginInit();
            oBitmapImg_01.UriSource = new Uri(PathedFileName_NewImg, UriKind.RelativeOrAbsolute);
            oBitmapImg_01.EndInit();
            img_ToChange.Source = oBitmapImg_01;

            // Alternative:
            //string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            //oBitmapImg_01.UriSource = new Uri("pack://application:,,,/" + AssemblyName + ";component/" + RelPathedFileName_NewImg);
        }

        private string NormalizePath(string Path_Orig)
        {
            if (Path_Orig.EndsWith("\\"))
            {
                return Path_Orig;
            }
            else
            {
                return Path_Orig + "\\";
            }
        }

        private string GetParentPath(string Path_Orig)
        {
            string NPath_Orig = NormalizePath(Path_Orig);

            if (NPath_Orig.Length < 3)
            {
                return NPath_Orig;
            }
            else
            {
                Int32 i_Pos = NPath_Orig.LastIndexOf("\\", NPath_Orig.Length - 2);
                return NPath_Orig.Substring(0, i_Pos) + "\\";
            }
        }

        /// <summary>
        /// Determine whether a string represents a valid decimal value.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private static bool IsTextNumeric(string Text)
        {
            // ToDo: Make sure there is only one decimal point and only one minus sign.

            bool b_OK = true;

            if (Text.Length > 0)
            {
                // Test the first character:
                string c = Text[0].ToString();
                if (!"-0123456789.".Contains(c)) b_OK = false;

                for (int Pos = 1; Pos < Text.Length; Pos++)
                {
                    c = Text[Pos].ToString();
                    if (!"0123456789.".Contains(c)) b_OK = false;
                }

                if (b_OK)
                {
                    // Count the decimal points in the text.
                    int count = Text.Split('.').Length - 1;
                    if (count > 1) b_OK = false;
                }
            }
            return b_OK;
        }

        #endregion Private Methods
    }

    public enum EntryType
    {
        Dir,
        File
    }

    public class DirectoryEntry
    {
        private string _name;
        private string _fullpath;
        private string _ext;
        private string _size;
        private DateTime _date;
        private string _imagepath;
        private EntryType _type;

        public DirectoryEntry(string name, string fullname, string ext, string size, DateTime date, string imagepath, EntryType type)
        {
            _name = name;
            _fullpath = fullname;
            _ext = ext;
            _size = size;
            _date = date;
            _imagepath = imagepath;
            _type = type;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Imagepath
        {
            get { return _imagepath; }
            set { _imagepath = value; }
        }

        public EntryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Fullpath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }
    }
}
