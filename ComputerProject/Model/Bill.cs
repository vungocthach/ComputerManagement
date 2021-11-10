﻿using ComputerProject.HelperService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerProject.Model
{
    public class Bill : BaseViewModel
    {
        int _id;
        DateTime _timeCreated;
        Collection<ProductInBill> _products;
        int _moneyCustomer;
        int _pointCustomer;
        int _totalMoney;
        CUSTOMER _customer;

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime TimeCreated
        {
            get => _timeCreated;
            private set
            {
                if (value != _timeCreated)
                {
                    _timeCreated = value;
                    OnPropertyChanged();
                }
            }
        }
        public Collection<ProductInBill> Products
        {
            get => _products;
            set
            {
                if (value != _products)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MoneyCustomer
        {
            get => _moneyCustomer;
            set
            {
                if (value != _moneyCustomer)
                {
                    _moneyCustomer = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ExcessCash));
                }
            }
        }
        public int PointCustomer
        {
            get => _pointCustomer;
            set
            {
                if (value != _pointCustomer)
                {
                    _pointCustomer = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TotalMoney
        {
            get => _totalMoney;
            set
            {
                if (value != _totalMoney)
                {
                    _totalMoney = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ExcessCash));
                }
            }
        }
        public int ExcessCash => MoneyCustomer - TotalMoney;
        public CUSTOMER Customer
        {
            get => _customer;
            set
            {
                if (value!=_customer)
                {
                    _customer = value;
                    OnPropertyChanged();
                }
            }
        }

        public Bill(BILL bill)
        {
            this.Id = bill.id;
            this.PointCustomer = bill.pointCustomer;
            this.TimeCreated = bill.createTime;
            this.TotalMoney = bill.totalMoney;
            this.MoneyCustomer = bill.customerMoney;
            this.Customer = bill.CUSTOMER;
            this.Products = new ObservableCollection<ProductInBill>();

            foreach (var item in bill.ITEM_BILL_SERI)
            {
                int unitprice = bill.ITEM_BILL.Where(s => s.productId == item.productId).First().unitPrice;
                ProductInBill product = new ProductInBill(item, unitprice);

                this.Products.Add(product);
            }
        }
        public Bill(IDictionary<Model.Product, int> listproduct, CUSTOMER customer, int totalMoney = 0)
        {
            this.Id = 0;
            this.TimeCreated = DateTime.Now;
            this.TotalMoney = totalMoney;
            this.Products = new ObservableCollection<ProductInBill>();

            foreach (var dictionaryProduct in listproduct)
            {
                for (int i = 0; i < dictionaryProduct.Value; i++)
                {
                    this.Products.Add(new ProductInBill(dictionaryProduct.Key));
                }
            }
        }

        public BILL CastToModel()
        {
            BILL bill = new BILL();
            bill.id = this.Id;
            bill.createTime = this.TimeCreated;
            bill.customerMoney = this.MoneyCustomer;
            bill.pointCustomer = this.PointCustomer;
            bill.totalMoney = this.TotalMoney;
            bill.CUSTOMER = this.Customer;
            bill.customerId = 1;
            bill.ITEM_BILL = new List<ITEM_BILL>();
            bill.ITEM_BILL_SERI = new List<ITEM_BILL_SERI>();

            foreach (var product in this.Products)
            {
                bill.ITEM_BILL_SERI.Add(new ITEM_BILL_SERI() { BILL = bill, seri = product.Seri, productId = product.Id });

                var itembill = bill.ITEM_BILL.Where(i => i.productId == product.Id).FirstOrDefault();
                if (itembill != null)
                {
                    itembill.quantity++;
                }
                else
                {
                    bill.ITEM_BILL.Add(new ITEM_BILL() { BILL = bill, productId = product.Id, quantity = 1, unitPrice = product.PriceSale });
                }
            }

            return bill;
        }
    }
}