﻿using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for UserSignInPage.xaml
    /// </summary>
    public partial class UserSignInPage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;
        private bool isUser;

        BlApi.IBL bl;

        public UserSignInPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        public UserSignInPage(BlApi.IBL bl, Customer customer, Boolean isUser) : this(bl)
        {
            windowState = State.Update;
            this.isUser = isUser;

            AddButton.Content = "Update";

            ButtonGrid.SetValue(Grid.RowProperty, 10);

            if (isUser)
            {
                User_image.Visibility = Visibility.Hidden;

                Welcome_msg.Visibility = Visibility.Visible;
                Welcome_msg.Text = "Welcome Back " + customer.Name + "!";

                LogoutButton.Visibility = Visibility.Visible;
            }

            ViewPackagesButton.Visibility = Visibility.Visible;

            ID_input.Text = customer.ID.ToString();
            ID_input.IsEnabled = false;
            ID_input.Foreground = Brushes.Gray;

            Name_input.Text = customer.Name;

            Phone_input.Text = customer.Phone;

            Longitude_input.Text = customer.Location.ToString();
            Longitude_input.IsEnabled = false;
            Longitude_input.Foreground = Brushes.Gray;

            Latitude_input.Visibility = Visibility.Hidden;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isUser)
            {
                NavigationService.GoBack();
            }
        }

        private void View_Packages_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerPackageListPage(bl, bl.GetCustomer(int.Parse(ID_input.Text))));
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            switch(windowState)
            {
                case State.Add:
                    try
                    {
                        bl.AddCustomer(int.Parse(ID_input.Text), Name_input.Text, Phone_input.Text, double.Parse(Longitude_input.Text), double.Parse(Latitude_input.Text));
                        NavigationService.GoBack();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case State.Update:
                    try
                    {
                        bl.UpdateCustomer(int.Parse(ID_input.Text), Name_input.Text, Phone_input.Text);
                        MessageBox.Show("  Customer Update Succesful!            ");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
            }
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isUser)
            {
                while (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else NavigationService.GoBack();
        }
    }
}
