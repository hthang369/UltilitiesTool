using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Drawing2D;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using System.Data;
using DevExpress.XtraEditors.Repository;

namespace DXApplication1
{
    [ToolboxItem(true)]
    public class BaseGridControl : GridControl
    {
        public string FieldDataSource;
        public bool isAllowNew;
        public bool isAllowFilter;

        public BaseGridControl()
        {
            InitializeComponent();
        }
        protected override BaseView CreateDefaultView()
        {
            return CreateView("BaseGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new BaseGridViewInfoRegistrator());
        }
        private void InitializeComponent()
        {
            this.BeginInit();
            base.SuspendLayout();
            this.EmbeddedNavigator.Name = "";
            //base.ProcessGridKey += new KeyEventHandler(this.GMCGridControl_ProcessGridKey);
            //InitializeGridControl();
            this.EndInit();
            base.ResumeLayout(false);
        }
        public virtual void InitializeGridControl()
        {
            try
            {
                GridView view = this.InitializeGridView();
                base.ViewCollection.Clear();
                base.ViewCollection.Add(view);
                base.MainView = view;
                base.MouseDoubleClick += new MouseEventHandler(this.GridControl_MouseDoubleClick);
                view.ValidateRow += new ValidateRowEventHandler(this.GridView_ValidateRow);
                view.ValidatingEditor += new BaseContainerValidateEditorEventHandler(this.GridView_ValidatingEditor);
                view.CustomSummaryCalculate += new CustomSummaryEventHandler(this.Gridview_CustomSummaryCalculate);
                //this.InitGridControlDataSource();
                this.UseEmbeddedNavigator = true;
                this.EmbeddedNavigator.Name = "navigator_" + base.Name;
                NavigatorCustomButton[] buttons = new NavigatorCustomButton[] { new NavigatorCustomButton(8, "Customize"), new NavigatorCustomButton(9, "Save Customization"), new NavigatorCustomButton(7, "Export"), new NavigatorCustomButton(6, "Reset Customization") };
                this.EmbeddedNavigator.Buttons.CustomButtons.AddRange(buttons);
                this.EmbeddedNavigator.Buttons.Edit.Visible = false;
                this.EmbeddedNavigator.Buttons.Remove.Visible = false;
                this.EmbeddedNavigator.Buttons.Append.Visible = false;
                this.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(this.NavigatorButton_Click);
            }
            catch (Exception exception)
            {
                string name = string.Empty;
                //if (this.Screen > null)
                //{
                //    name = this.Screen.Name;
                //}
                //GMCLogging.LogNewMessage(this.ModuleName, GMCApp.CurrentUser, "InitializeGridControl ", string.Format("Screen : {0}  -   Control {1}  -  Message {2}", name, this.Field.STFieldName, exception.Message), "FAILE", true);
            }
        }

        private void GridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GridControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void GridView_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            
        }

        private void Gridview_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            
        }

        private void NavigatorButton_Click(object sender, NavigatorButtonClickEventArgs e)
        {
            
        }
        protected virtual GridView InitializeGridView()
        {
            
            GridView gridView = new BaseGridView
            {
                Name = "fld_dgv" + base.Name.Substring(7),
                OptionsBehavior = { Editable = true },
                OptionsView = {
                    ShowGroupPanel = true,
                    ColumnAutoWidth = false,
                    ShowDetailButtons = false
                },
                OptionsFilter = {
                    AllowFilterEditor = true,
                    UseNewCustomFilterDialog = true
                }
            };
            
            if (isAllowNew)
            {
                gridView.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
                gridView.OptionsView.ShowNewItemRow = true;
                gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
            else
            {
                gridView.OptionsView.ShowNewItemRow = false;
                gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }
            gridView.OptionsView.ShowAutoFilterRow = isAllowFilter;
            
            gridView.OptionsSelection.EnableAppearanceFocusedCell = true;
            gridView.OptionsCustomization.AllowFilter = true;
            gridView.OptionsSelection.MultiSelect = false;
            //gridView.KeyUp += new KeyEventHandler(this.GridView_KeyUp);
            gridView.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridView.Appearance.Row.BackColor = Color.FromArgb(0xdb, 0xe5, 0xf1);
            gridView.Appearance.OddRow.BackColor = Color.FromArgb(0xb8, 0xcc, 0xe4);
            gridView.OptionsView.EnableAppearanceOddRow = true;
            gridView.Appearance.FocusedRow.BackColor = Color.OrangeRed;
            gridView.Appearance.FocusedRow.BackColor2 = Color.SandyBrown;
            gridView.Appearance.FocusedRow.GradientMode = LinearGradientMode.ForwardDiagonal;
            gridView.OptionsNavigation.EnterMoveNextColumn = true;
            //gridView.InvalidRowException += new InvalidRowExceptionEventHandler(this.gridView_InvalidRowException);
            gridView.GridControl = this;
            if (this.FieldDataSource != null)
            {
                this.InitGridViewColumns(gridView);
            }
            //gridView.FocusedRowChanged += new FocusedRowChangedEventHandler(this.GridView_FocusedRowChanged);
            //gridView.MouseUp += new MouseEventHandler(this.GridControl_MouseUp);
            //gridView.CustomDrawCell += new RowCellCustomDrawEventHandler(this.GridView_CustomDrawCell);
            //gridView.CustomUnboundColumnData += new CustomColumnDataEventHandler(this.GridView_CustomUnboundColumnData);
            //gridView.ShownEditor += new System.EventHandler(this.GridView_ShownEditor);
            return gridView;
        }
        protected virtual void InitGridViewColumns(GridView gridView)
        {
            if (this.FieldDataSource != null)
            {
                gridView.Columns.Clear();
                //this.InitDefaultGridViewColumns(gridView);
                this.AddColumnsToGridView(this.FieldDataSource, gridView);
                //this.InitDefaultGridColumnsVisibleIndex(gridView);
            }
        }
        public void AddColumnsToGridView(string strTableName, GridView gridView)
        {
            GridControl gridControl = (GridControl)gridView.GridControl;
            DataTable allDataByGridColumnAndUserIDOrderByTabIndex = SQLAppLib.SQLDBUtil.GetTableColumns(strTableName);
            //List<STGridColumnsInfo> list = new List<STGridColumnsInfo>();
            if (allDataByGridColumnAndUserIDOrderByTabIndex.Rows.Count > 0)
            {
                foreach (DataRow row in allDataByGridColumnAndUserIDOrderByTabIndex.Rows)
                {
                    try
                    {
                        this.AddColumn(gridView, row, "");
                    }
                    catch (Exception exception)
                    {
                        //GMCLogging.LogNewMessage(this.ModuleName, GMCApp.CurrentUser, "AddColumn ", string.Format("TableName : {0}  -   Control {1}  -  Message {2}", strTableName, this.Field.STFieldName, exception.Message), "FAILE", true);
                    }
                }
            }
            else
            {
                //DataSet aAColumnAliasByTableName = controller2.GetAAColumnAliasByTableName(strTableName);
                //if (allDataByGridColumnAndUserIDOrderByTabIndex.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row2 in allDataByGridColumnAndUserIDOrderByTabIndex.Tables[0].Rows)
                //    {
                //        STGridColumnsInfo objSTGridColumnsInfo = new STGridColumnsInfo
                //        {
                //            STGridColumnTableName = row2["AATableName"].ToString(),
                //            STGridColumnName = row2["AAColumnAliasName"].ToString(),
                //            STGridColumnCaption = row2["AAColumnAliasCaption"].ToString(),
                //            STGridColumnEnable = false,
                //            STGridColumnVisible = false
                //        };
                //        this.AddColumn(gridView, objSTGridColumnsInfo, objSTGridColumnsInfo.STGridColumnCaption);
                //    }
                //}
            }
        }
        public void AddColumn(GridView gridView, DataRow objInfo, string strCaption)
        {
            int sTGridColumnTabIndex = Convert.ToInt32(objInfo["ORDINAL_POSITION"]);
            GridColumn column = new GridColumn();
            column.FieldName = Convert.ToString(objInfo["COLUMN_NAME"]);
            column.Caption = column.FieldName;
            column.Name = column.FieldName;
            column.Tag = Convert.ToString(objInfo["TABLE_NAME"]);
            column.Visible = true;
            if (column.FieldName.Contains("TableName") || column.FieldName.Contains("ColumnName"))
            {
                RepositoryItemLookUpEdit edit = this.InitColumnLookupEdit(column.FieldName);
                if (edit != null)
                {
                    column.ColumnEdit = edit;
                    //edit.CheckAllowNewButton(objSTGridColumnsInfo.STGridColumnFieldGroup);
                }
            }
            column.FilterMode = ColumnFilterMode.DisplayText;
            column.OptionsFilter.AllowAutoFilter = true;
            column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView.Columns.Add(column);
        }
        public virtual RepositoryItemLookUpEdit InitColumnLookupEdit(string strColumnName)
        {
            RepositoryItemLookUpEdit edit = new RepositoryItemLookUpEdit();
            edit.TextEditStyle = TextEditStyles.Standard;
            edit.SearchMode = SearchMode.AutoFilter;
            edit.NullText = string.Empty;
            edit.PopupSizeable = true;
            edit.BestFitMode = BestFitMode.BestFitResizePopup;
            edit.BestFit();
            DataView dvLookupTable = new DataView();
            if (strColumnName.Contains("TableName"))
            {
                DataSet dsAllTables = SQLAppLib.SQLDBUtil.GetAllTables();
                dvLookupTable = SQLAppLib.SQLDBUtil.GetDataTableByDataSet(dsAllTables).DefaultView;
                edit.ValueMember = "TableName";
                edit.DisplayMember = "TableName";
            }
            else if (strColumnName.Contains("ColumnName"))
            {
                //DataSet dsAllColumns = SQLAppLib.SQLDBUtil.GetAllTables();
                //dvLookupTable = SQLAppLib.SQLDBUtil.GetDataTableByDataSet(dsAllTables).DefaultView;
                edit.ValueMember = "COLUMN_NAME";
                edit.DisplayMember = "COLUMN_NAME";
            }
            //GMCApp.SetDummy(dvLookupTable, objSTGridColumnsInfo, sTGridColumnRepository);
            edit.DataSource = dvLookupTable;
            edit.DisplayFormat.FormatType = FormatType.Custom;
            edit.QueryPopUp += new CancelEventHandler(this.RepositoryItemLookupEdit_QueryPopup);
            //edit.EditValueChanging += new ChangingEventHandler(this.RepositoryItemLookupEdit_EditValueChanging);
            //edit.EditValueChanged += new System.EventHandler(this.RepositoryItemLookupEdit_EditValueChanged);
            if (edit.Columns.Count > 0)
            {
                edit.Columns[0].SortOrder = ColumnSortOrder.Ascending;
                edit.SortColumnIndex = 0;
            }
            //edit.Spin += new SpinEventHandler(this.AllowScrollInPopupSpinEvent);
            return edit;
        }

        protected virtual void RepositoryItemLookupEdit_QueryPopup(object sender, CancelEventArgs e)
        {
            
        }
    }

    public class BaseGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName
        {
            get
            {
                return "BaseGridView";
            }
        }

        public override BaseView CreateView(GridControl grid)
        {
            return new BaseGridView(grid);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new BaseGridViewInfo(view as BaseGridView);
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new BaseGridViewHandler(view as BaseGridView);
        }

        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new BaseGridViewPainter(view as BaseGridView);
        }
    }

    public class BaseGridView : DevExpress.XtraGrid.Views.Grid.GridView
    {
        public BaseGridView()
        {
        }

        public BaseGridView(GridControl grid) : base(grid)
        {
        }

        protected override string ViewName
        {
            get
            {
                return "BaseGridView";
            }
        }
    }

    public class BaseGridViewInfo : DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo
    {
        public BaseGridViewInfo(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }

    public class BaseGridViewHandler : DevExpress.XtraGrid.Views.Grid.Handler.GridHandler
    {
        public BaseGridViewHandler(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }

    public class BaseGridViewPainter : DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter
    {
        public BaseGridViewPainter(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }
}
