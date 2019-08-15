﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SQLTool.ViewModels
{
    public class PopupViewModel : BaseViewModel
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private bool _isText;
        public bool isText
        {
            get => _isText;
            set => SetProperty(ref _isText, value);
        }
        private string _valueReturn;
        public string valueReturn
        {
            get => _valueReturn;
            set => SetProperty(ref _valueReturn, value);
        }
        public Visibility isTextVisibility
        {
            get
            {
                if (isText)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }
        public Visibility isComboBoxVisibility
        {
            get
            {
                if (isText)
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
        }
        public ICommand okCommand { get; set; }
        public ICommand cancelCommand { get; set; }
        public PopupViewModel()
        {
            okCommand = new RelayCommand<object>((x) => { return true; }, (x) => OKCommandEvent(x));
            cancelCommand = new RelayCommand<object>((x) => { return true; }, (x) => CancelCommandEvent(x));
        }

        private void CloseFormEvent(object obj)
        {
            FrameworkElement element = GetFrameworkElement(obj as FrameworkElement);
            Window win = element as Window;
            if (win != null)
                win.Close();
        }

        private void CancelCommandEvent(object x)
        {
            
        }

        private void OKCommandEvent(object obj)
        {
            //(obj as Views.PopupWindow).set
        }

        private FrameworkElement GetFrameworkElement(FrameworkElement w)
        {
            if (w.Parent != null) w = GetFrameworkElement(w.Parent as FrameworkElement);
            return w;
        }
    }
}
