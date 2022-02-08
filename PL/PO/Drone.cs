﻿using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.PO
{
    public class Drone : INotifyPropertyChanged
    {
        private IBL bl;
        public Drone(uint ID, IBL bl) {
            this.ID = ID;
            this.bl = bl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public uint ID { init; get; }
        public string Model {
            get => bl.GetDrone(ID).Model;
            set {
                bl.UpdateDrone(ID, value);
                PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }
        public WeightCategories Weight => bl.GetDrone(ID).Weight;
        public double Battery => bl.GetDrone(ID).Battery;
        public DroneStatuses Status => bl.GetDrone(ID).Status;
        public Location Location => bl.GetDrone(ID).Location;
        public EnroutePackage Package => bl.GetDrone(ID).Package;

        public void Charge()
        {
            bl.ChargeDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
        }

        public void Release()
        {
            bl.ReleaseDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
        }
        public void Assign()
        {
            bl.AssignPackageToDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
        public void Collect()
        {
            bl.CollectPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
        public void Deliver()
        {
            bl.DeliverPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }

        public void Simulate(Func<bool> stopCheck)
        {
            bl.ActivateSimulator(ID, Reload, stopCheck);
        }
        public void Reload()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
    }
}