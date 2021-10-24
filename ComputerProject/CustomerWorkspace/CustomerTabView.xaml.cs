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

namespace ComputerProject.CustomerWorkspace
{
    /// <summary>
    /// Interaction logic for CustomerTabView.xaml
    /// </summary>
    public partial class CustomerTabView : UserControl
    {
        public CustomerTabViewModel _vm => this.DataContext as CustomerTabViewModel;
        //private CustomerAllView mainView;

        public CustomerTabView()
        {
            InitializeComponent();

            var mainView = new CustomerAllView();
            mainView.ClickedCreate += OnClick_Create;
            mainView.ClickedDeleteItem += OnClick_DeleteItem;
            mainView.ClickedEditItem += OnClick_EditItem;


            var addView = new CustomerAdd();
            addView.Closed_NotSave += OnBackFrom_Create;
            addView.SaveOK += AddView_SavedOK;

            var detailView = new CustomerDetailView();
            detailView.ClickedBack += (s, e) => _vm.CurrentMainViewIndex = 0;

            DataContext = new CustomerTabViewModel()
            {
                ListViews = new List<Control>()
                {
                    mainView, addView, detailView
                }
            };

            _vm.CurrentMainViewIndex = 0;
        }

        private void OnClick_Create(object sender, EventArgs e)
        {
            // MessageBox.Show("hora! it's worked");
            var addView = _vm.ListViews[1] as CustomerAdd;
            addView.DataContext = new CustomerViewModel();

            _vm.CurrentMainViewIndex = 1;
        }
        private void OnBackFrom_Create(object sender, EventArgs e)
        {
            var addView = sender as CustomerAdd;
            addView.DataContext = null;

            _vm.CurrentMainViewIndex = 0;
        }

        private void AddView_SavedOK(object sender, EventArgs e)
        {
            var cus = (sender as CustomerAdd).ViewModel;
            var mainViewVM = (_vm.ListViews[0] as CustomerAllView).ViewModel;
            mainViewVM.CustomerList.Add(cus);

            CustomMessageBox.MessageBox.Show("Đã thêm khách hàng mới vào cơ sở dữ liệu thành công");
            OnBackFrom_Create(sender, e);
        }

        private void OnClick_EditItem(object sender, EventArgs e)
        {
            var rowVM = (sender as CustomerAllViewRow).ViewModel;
            var editView = _vm.ListViews[2] as CustomerAdd;
            editView.DataContext = rowVM;

            _vm.CurrentMainViewIndex = 2;
        }
        private void OnBackFrom_EditItem(object sender, EventArgs e)
        {
            var editView = sender as CustomerDetailView;
            editView.DataContext = null;

            _vm.CurrentMainViewIndex = 0;
        }

        private void OnClick_DeleteItem(object sender, EventArgs e)
        {
            MessageBox.Show("hora! it's worked");
            //_vm.CurrentMainViewIndex = 1;
        }
    }
}