using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLQQ
{
    public partial class frmInput : Form
    {
        public bool inputText = true;
        public bool inputCombobox = false;
        public bool multiline = false;
        AutoCompleteStringCollection autoComplete;
        public frmInput()
        {
            InitializeComponent();
            txtInput.Visible = inputText;
            cboItems.Visible = inputCombobox;
            txtInput.WordWrap = true;
            txtInput.Multiline = multiline;
            autoComplete = new AutoCompleteStringCollection();

            cboItems.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboItems.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboItems.AutoCompleteCustomSource = autoComplete;

            txtInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtInput.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtInput.AutoCompleteCustomSource = autoComplete;
        }

        
    }
}
