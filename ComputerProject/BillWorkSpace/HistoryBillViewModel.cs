﻿using ComputerProject.HelperService;
using ComputerProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerProject.BillWorkSpace
{
    public class HistoryBillViewModel : BaseViewModel
    {
        #region Fields
        NavigationService _navigator;
        BillRepository _repository;
        public BusyViewModel BusyService { get; private set; } = new BusyViewModel();

        int _maxBillsInPage = 10;
        Collection<BILL> _bills;
        Collection<BILL> _selectionBills;
        int _currentPage;
        int _totalPage;
        string _textSearch;
        DateTime? _timeFrom;
        DateTime? _timeTo;

        ICommand _searchBillCommand;
        ICommand _exportPdfCommand;
        ICommand _deleteBillCommand;
        ICommand _showDetailBillCommand;
        ICommand _changePageCommand;
        #endregion // Fields

        #region Properties
        public Collection<BILL> CurrentBills
        {
            get => _bills;
            set
            {
                if (value != _bills)
                {
                    _bills = value;
                    OnPropertyChanged();
                }
            }
        }
        public Collection<BILL> SelectionBills
        {
            get => _selectionBills;
            set
            {
                if (value != _selectionBills)
                {
                    _selectionBills = value;
                    OnPropertyChanged();
                }
            }
        }
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (value != _currentPage)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TotalPage
        {
            get => _totalPage;
            set
            {
                if (value != _totalPage)
                {
                    _totalPage = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TextSearch
        {
            get => _textSearch;
            set
            {
                if (value != _textSearch)
                {
                    _textSearch = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime? TimeFrom
        {
            get => _timeFrom;
            set
            {
                if (value != _timeFrom)
                {
                    _timeFrom = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime? TimeTo
        {
            get => _timeTo;
            set
            {
                if (value != _timeTo)
                {
                    _timeTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchBillCommand
        {
            get
            {
                if (null == _searchBillCommand)
                {
                    _searchBillCommand = new RelayCommand(s => SearchBill(TextSearch, TimeFrom, TimeTo));
                }
                return _searchBillCommand;
            }
        }
        public ICommand ExportPdfCommand
        {
            get
            {
                if (null == _exportPdfCommand)
                {
                    _exportPdfCommand = new RelayCommand(_ => ExportPdf(SelectionBills));
                }
                return _exportPdfCommand;
            }
        }
        public ICommand DeleteBillCommand
        {
            get
            {
                if (null == _deleteBillCommand)
                {
                    _deleteBillCommand = new RelayCommand(b => DeleteBill(b as BILL));
                }
                return _deleteBillCommand;
            }
        }
        public ICommand ShowDetailBillCommand
        {
            get
            {
                if (null == _showDetailBillCommand)
                {
                    _showDetailBillCommand = new RelayCommand(b => ShowDetail(b as BILL));
                }
                return _showDetailBillCommand;
            }
        }
        public ICommand ChangePageCommand
        {
            get
            {
                if (null == _changePageCommand)
                {
                    _changePageCommand = new RelayCommand(pageNumber => ChangePage((int)pageNumber));
                }
                return _changePageCommand;
            }
        }
        #endregion //Properties

        public HistoryBillViewModel(int maxBillsInPage = 8)
        {
            //BusyService = new BusyViewModel();
            this._maxBillsInPage = maxBillsInPage;
            _repository = new BillRepository();
        }

        public async void LoadBills(int pageNumber = 1, string text = null, DateTime? timeFrom = null, DateTime? timeTo = null)
        {
            TextSearch = text;
            TimeFrom = timeFrom;
            TimeTo = timeTo;

            var taskLoadTotalPage = Task.Run(() => TotalPage = _repository.LoadNumberPages(_maxBillsInPage, TextSearch, TimeFrom, TimeTo));

            await taskLoadTotalPage;
            if (TotalPage < CurrentPage)
            {
                CurrentPage = TotalPage;
            }
            else CurrentPage = pageNumber;

            CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage, TextSearch, TimeFrom, TimeTo);
        }

        public void LoadBillsAsync(int pageNumber = 1, string text = null, DateTime? timeFrom = null, DateTime? timeTo = null)
        {
            BusyService.DoBusyTask(() => LoadBills(pageNumber, text, timeFrom, timeTo));
        }
        public void SetNavigator(NavigationService navigator)
        {
            this._navigator = navigator;
        }

        private void SearchBill(string text, DateTime? timeFrom, DateTime? timeTo)
        {
            LoadBillsAsync(1, text, timeFrom, timeTo);
        }
        private void ExportPdf(Collection<BILL> bills)
        {
            throw new NotImplementedException();
        }
        private void DeleteBill(BILL bill)
        {
            if (CurrentBills.Contains(bill))
            {
                CurrentBills.Remove(bill);
                _repository.RemoveAsync(bill);

                LoadBillsAsync(CurrentPage);
            }
            else
            {
                throw new InvalidOperationException("List not containt this bill");
            }
        }
        private void ShowDetail(BILL bill)
        {
            if (_navigator == null) throw new NullReferenceException("Navigator is null");

            DetailBillViewModel vm = new DetailBillViewModel(bill);
            vm.setNavigator(_navigator);
            vm.BillDeletedEvent += (sender, id) =>
            {
                LoadBillsAsync(CurrentPage);
            };

            _navigator.NavigateTo(vm);
            _navigator.Back = () => _navigator?.NavigateTo(this);
        }
        private void ChangePage(int pageNumber)
        {
            CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage = pageNumber);
        }
    }
}
