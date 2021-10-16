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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerProject.CategoryWorkspace
{
    /// <summary>
    /// Interaction logic for Categorylv2Row.xaml
    /// </summary>
    public partial class Categorylv2Row : UserControl
    {
        public Categorylv2Row()
        {
            InitializeComponent();
            InitData();
        }

        public void InitData()
        {
            CategoryLv2Name.Text = "Laptop";
        }
    }
}