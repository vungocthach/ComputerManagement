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

        int _maxBillsInPage = 10;
        Collection<BILL> _bills;
        Collection<BILL> _selectionBills;
        int _currentPage;
        int _totalPage;
        string _textSearch;
        DateTime? _timeFrom;
        DateTime? _timeTo;

        ICommand _searchBillbyStringCommand;
        ICommand _searchBillbyTimeCommand;
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

        public ICommand SearchBillbyStringCommand
        {
            get
            {
                if (null == _searchBillbyStringCommand)
                {
                    _searchBillbyStringCommand = new RelayCommand(s => SearchBill(TextSearch));
                }
                return _searchBillbyStringCommand;
            }
        }
        public ICommand SearchBillbyTimeCommand
        {
            get
            {
                if (null == _searchBillbyTimeCommand)
                {
                    _searchBillbyTimeCommand = new RelayCommand(t => SearchBill(TimeFrom, TimeTo));
                }
                return _searchBillbyTimeCommand;
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

        public HistoryBillViewModel(int maxBillsInPage = 10)
        {
            this._maxBillsInPage = maxBillsInPage;
            _repository = new BillRepository();
        }

        public void LoadInitBills()
        {
            CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage = 1);
            TotalPage = _repository.LoadNumberPages(_maxBillsInPage);
        }

        public async void ReloadBills(int pageNumber = 1)
        {
            var taskLoadTotalPage = Task.Run(() => TotalPage = _repository.LoadNumberPages(_maxBillsInPage, TextSearch, TimeFrom, TimeTo));

            await taskLoadTotalPage;
            if (TotalPage < CurrentPage)
            {
                CurrentPage = TotalPage;
            }
            Task.Run(() => CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage, TextSearch, TimeFrom, TimeTo));
        }
        public void SetNavigator(NavigationService navigator)
        {
            this._navigator = navigator;
        }

        private void SearchBill(string text)
        {
            CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage = 1, text);
            TotalPage = _repository.LoadNumberPages(_maxBillsInPage, text);
        }
        private void SearchBill(DateTime? timeFrom, DateTime? timeTo)
        {
            CurrentBills = _repository.LoadBills(_maxBillsInPage, CurrentPage = 1, TextSearch, timeFrom, timeTo);
            TotalPage = _repository.LoadNumberPages(_maxBillsInPage, timeFrom, timeTo);
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
            vm.BillDeletedEvent += (sender, id) =>
            {
                ReloadBills(CurrentPage);
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