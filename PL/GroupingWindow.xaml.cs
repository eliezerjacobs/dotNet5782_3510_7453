﻿using System;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for GroupingWindow.xaml
    /// </summary>
    public partial class GroupingWindow : Window
    {
        public GroupingWindow(String type)
        {
            InitializeComponent();

            switch (type)
            {
                case "Station":
                    RadioButton2.Content = " Available Slots";
                    RadioButton3.Content = " Num of Available Slots";
                    break;
            }
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
