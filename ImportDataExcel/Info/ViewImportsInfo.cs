
using System;
using System.Text;
using System.Collections.Generic;
using ExpertLib;
namespace ExpertERP.BusinessEntities
{
    #region ViewImports
    //-----------------------------------------------------------
    //Generated By: SQLDBTools - LINHCLH (v2.1.43)
    //Class: ViewImportsInfo
    //Created Date: Wednesday, 09 Mar 2016
    //-----------------------------------------------------------

    public class ViewImportsInfo : BusinessObject
    {
        public ViewImportsInfo()
        {
        }
        #region Variables
		protected int _viewImportID;
		protected String _aAStatus = DefaultAAStatus;
		protected String _aACreatedUser = String.Empty;
		protected String _aAUpdatedUser = String.Empty;
		protected Nullable<DateTime> _aACreatedDate;
		protected Nullable<DateTime> _aAUpdatedDate;
		protected bool _aASelected = true;
		protected int _fK_BRBranchID;
		protected String _gLVoucherTypeCombo = String.Empty;
		protected String _viewImportVoucherNo = String.Empty;
		protected Nullable<DateTime> _viewImportVoucherDate;
		protected String _viewImportVoucherDesc = String.Empty;
		protected int _fK_HREmployeeID;
		protected String _viewImportOutPmtPayToName = String.Empty;
		protected int _fK_GECurrencyID;
		protected double _viewImportVoucherExcRate;
		protected int _fK_GLBankID;
		protected int _fK_GLLCID;
		protected String _viewImportVoucherItemDesc = String.Empty;
		protected int _fK_GLDebitAccountID;
		protected int _fK_GLCreditAccountID;
		protected double _viewImportVoucherItemFAmtTot;
		protected double _viewImportVoucherItemAmtTot;
		protected String _gLObjectType = String.Empty;
		protected int _fK_GLObjectID;
		protected int _fK_ARSOID;
		protected int _fK_GLCashFlowID;
		protected int _fK_GLLoanAgreementID;
		protected String _gLVoucherPmtMethodTypeCombo = String.Empty;
		protected String _gLTOF01Combo = String.Empty;
		protected String _gLTOF02Combo = String.Empty;
		protected String _gLTOF03Combo = String.Empty;
		protected String _gLTOF04Combo = String.Empty;
		protected String _gLTOF05Combo = String.Empty;
		protected String _gLTOF06Combo = String.Empty;
		protected String _gLTOF07Combo = String.Empty;
		protected String _gLTOF08Combo = String.Empty;
		protected String _gLTOF09Combo = String.Empty;
		protected String _gLTOF10Combo = String.Empty;
		protected String _viewImportVoucherItemInvSeries = String.Empty;
		protected String _viewImportVoucherItemInvNo = String.Empty;
		protected Nullable<DateTime> _viewImportVoucherItemInvDate;
		protected String _gLVoucherItemObjectTypeCombo = String.Empty;
		protected String _viewImportVoucherItemObjectName = String.Empty;
		protected String _viewImportVoucherItemObjectTxNo = String.Empty;
		protected int _fK_GLTranCfgID;
		protected int _fK_APPOID;
		protected int _fK_FAAssetConstructionID;
		protected int _fK_GEInvTypeID;
		protected double _viewImportTaxAmtTot;
		protected int _fK_TXTaxCodeID;
		protected String _viewImportQuestAddress = String.Empty;
		protected String _viewImportVoucherDocNo = String.Empty;
		protected double _viewImportTaxFAmtTot;
		protected String _viewImportMessage = String.Empty;
		protected Nullable<DateTime> _viewImportVoucherDueDate;
		protected int _fK_ICStockID;
		protected int _fK_ICProductID;
		protected int _fK_ICUOMID;
		protected int _fK_ICStkUOMID;
		protected double _viewImportItemQty;
		protected double _viewImportItemStkQty;
		protected double _viewImportFUnitPrice;
		protected double _viewImportUnitPrice;
		protected String _viewImportItemLotNo = String.Empty;
		protected int _fK_HRDepartmentID;
		protected double _viewImportVoucherItemLife;
		protected double _viewImportVoucherItemAllocateAmt;
		protected double _viewImportVoucherItemRLife;
		protected double _viewImportVoucherItemRAmt;
		protected int _fK_GLCostDistID;
		protected String _viewImportAssetNo = String.Empty;
		protected String _viewImportAssetName = String.Empty;
		protected Nullable<DateTime> _viewImportVoucherItemPurchaseDate;
		protected int _fK_FAAssetCriteriaID;
		protected Nullable<DateTime> _viewImportVoucherItemDeprDate;
		protected int _fK_GEGenerateID;
		protected int _fK_FAAssetGroupID;
		protected int _fK_FAAssetTypeID;
		protected int _fK_FAAssetSourceID;
		protected int _fK_APSupplierID;
		protected Nullable<DateTime> _viewImportUserDate;
		protected String _viewImportErrorField = String.Empty;
		protected double _viewImportCostFPeriodAmt;
		protected double _viewImportCostPeriodAmt;
		protected double _viewImportVoucherItemBeginLife;
		protected String _gLTOF11Combo = String.Empty;
		protected String _gLTOF12Combo = String.Empty;
		protected String _gLTOF13Combo = String.Empty;
		protected String _gLTOF14Combo = String.Empty;
		protected String _gLTOF15Combo = String.Empty;
		protected double _viewImportItemFPrice;
		protected double _viewImportItemPrice;
		
        #endregion

        #region Public properties
		public int ViewImportID
		{
			get { return _viewImportID; }
			set
			{
				if (value != this._viewImportID)
				{
					_viewImportID = value;
					NotifyChanged("ViewImportID");
				}
			}
		}
		public String AAStatus
		{
			get { return _aAStatus; }
			set
			{
				if (value != this._aAStatus)
				{
					_aAStatus = value;
					NotifyChanged("AAStatus");
				}
			}
		}
		public String AACreatedUser
		{
			get { return _aACreatedUser; }
			set
			{
				if (value != this._aACreatedUser)
				{
					_aACreatedUser = value;
					NotifyChanged("AACreatedUser");
				}
			}
		}
		public String AAUpdatedUser
		{
			get { return _aAUpdatedUser; }
			set
			{
				if (value != this._aAUpdatedUser)
				{
					_aAUpdatedUser = value;
					NotifyChanged("AAUpdatedUser");
				}
			}
		}
		public Nullable<DateTime> AACreatedDate
		{
			get { return _aACreatedDate; }
			set
			{
				if (value != this._aACreatedDate)
				{
					_aACreatedDate = value;
					NotifyChanged("AACreatedDate");
				}
			}
		}
		public Nullable<DateTime> AAUpdatedDate
		{
			get { return _aAUpdatedDate; }
			set
			{
				if (value != this._aAUpdatedDate)
				{
					_aAUpdatedDate = value;
					NotifyChanged("AAUpdatedDate");
				}
			}
		}
		public bool AASelected
		{
			get { return _aASelected; }
			set
			{
				if (value != this._aASelected)
				{
					_aASelected = value;
					NotifyChanged("AASelected");
				}
			}
		}
		public int FK_BRBranchID
		{
			get { return _fK_BRBranchID; }
			set
			{
				if (value != this._fK_BRBranchID)
				{
					_fK_BRBranchID = value;
					NotifyChanged("FK_BRBranchID");
				}
			}
		}
		public String GLVoucherTypeCombo
		{
			get { return _gLVoucherTypeCombo; }
			set
			{
				if (value != this._gLVoucherTypeCombo)
				{
					_gLVoucherTypeCombo = value;
					NotifyChanged("GLVoucherTypeCombo");
				}
			}
		}
		public String ViewImportVoucherNo
		{
			get { return _viewImportVoucherNo; }
			set
			{
				if (value != this._viewImportVoucherNo)
				{
					_viewImportVoucherNo = value;
					NotifyChanged("ViewImportVoucherNo");
				}
			}
		}
		public Nullable<DateTime> ViewImportVoucherDate
		{
			get { return _viewImportVoucherDate; }
			set
			{
				if (value != this._viewImportVoucherDate)
				{
					_viewImportVoucherDate = value;
					NotifyChanged("ViewImportVoucherDate");
				}
			}
		}
		public String ViewImportVoucherDesc
		{
			get { return _viewImportVoucherDesc; }
			set
			{
				if (value != this._viewImportVoucherDesc)
				{
					_viewImportVoucherDesc = value;
					NotifyChanged("ViewImportVoucherDesc");
				}
			}
		}
		public int FK_HREmployeeID
		{
			get { return _fK_HREmployeeID; }
			set
			{
				if (value != this._fK_HREmployeeID)
				{
					_fK_HREmployeeID = value;
					NotifyChanged("FK_HREmployeeID");
				}
			}
		}
		public String ViewImportOutPmtPayToName
		{
			get { return _viewImportOutPmtPayToName; }
			set
			{
				if (value != this._viewImportOutPmtPayToName)
				{
					_viewImportOutPmtPayToName = value;
					NotifyChanged("ViewImportOutPmtPayToName");
				}
			}
		}
		public int FK_GECurrencyID
		{
			get { return _fK_GECurrencyID; }
			set
			{
				if (value != this._fK_GECurrencyID)
				{
					_fK_GECurrencyID = value;
					NotifyChanged("FK_GECurrencyID");
				}
			}
		}
		public double ViewImportVoucherExcRate
		{
			get { return _viewImportVoucherExcRate; }
			set
			{
				if (value != this._viewImportVoucherExcRate)
				{
					_viewImportVoucherExcRate = value;
					NotifyChanged("ViewImportVoucherExcRate");
				}
			}
		}
		public int FK_GLBankID
		{
			get { return _fK_GLBankID; }
			set
			{
				if (value != this._fK_GLBankID)
				{
					_fK_GLBankID = value;
					NotifyChanged("FK_GLBankID");
				}
			}
		}
		public int FK_GLLCID
		{
			get { return _fK_GLLCID; }
			set
			{
				if (value != this._fK_GLLCID)
				{
					_fK_GLLCID = value;
					NotifyChanged("FK_GLLCID");
				}
			}
		}
		public String ViewImportVoucherItemDesc
		{
			get { return _viewImportVoucherItemDesc; }
			set
			{
				if (value != this._viewImportVoucherItemDesc)
				{
					_viewImportVoucherItemDesc = value;
					NotifyChanged("ViewImportVoucherItemDesc");
				}
			}
		}
		public int FK_GLDebitAccountID
		{
			get { return _fK_GLDebitAccountID; }
			set
			{
				if (value != this._fK_GLDebitAccountID)
				{
					_fK_GLDebitAccountID = value;
					NotifyChanged("FK_GLDebitAccountID");
				}
			}
		}
		public int FK_GLCreditAccountID
		{
			get { return _fK_GLCreditAccountID; }
			set
			{
				if (value != this._fK_GLCreditAccountID)
				{
					_fK_GLCreditAccountID = value;
					NotifyChanged("FK_GLCreditAccountID");
				}
			}
		}
		public double ViewImportVoucherItemFAmtTot
		{
			get { return _viewImportVoucherItemFAmtTot; }
			set
			{
				if (value != this._viewImportVoucherItemFAmtTot)
				{
					_viewImportVoucherItemFAmtTot = value;
					NotifyChanged("ViewImportVoucherItemFAmtTot");
				}
			}
		}
		public double ViewImportVoucherItemAmtTot
		{
			get { return _viewImportVoucherItemAmtTot; }
			set
			{
				if (value != this._viewImportVoucherItemAmtTot)
				{
					_viewImportVoucherItemAmtTot = value;
					NotifyChanged("ViewImportVoucherItemAmtTot");
				}
			}
		}
		public String GLObjectType
		{
			get { return _gLObjectType; }
			set
			{
				if (value != this._gLObjectType)
				{
					_gLObjectType = value;
					NotifyChanged("GLObjectType");
				}
			}
		}
		public int FK_GLObjectID
		{
			get { return _fK_GLObjectID; }
			set
			{
				if (value != this._fK_GLObjectID)
				{
					_fK_GLObjectID = value;
					NotifyChanged("FK_GLObjectID");
				}
			}
		}
		public int FK_ARSOID
		{
			get { return _fK_ARSOID; }
			set
			{
				if (value != this._fK_ARSOID)
				{
					_fK_ARSOID = value;
					NotifyChanged("FK_ARSOID");
				}
			}
		}
		public int FK_GLCashFlowID
		{
			get { return _fK_GLCashFlowID; }
			set
			{
				if (value != this._fK_GLCashFlowID)
				{
					_fK_GLCashFlowID = value;
					NotifyChanged("FK_GLCashFlowID");
				}
			}
		}
		public int FK_GLLoanAgreementID
		{
			get { return _fK_GLLoanAgreementID; }
			set
			{
				if (value != this._fK_GLLoanAgreementID)
				{
					_fK_GLLoanAgreementID = value;
					NotifyChanged("FK_GLLoanAgreementID");
				}
			}
		}
		public String GLVoucherPmtMethodTypeCombo
		{
			get { return _gLVoucherPmtMethodTypeCombo; }
			set
			{
				if (value != this._gLVoucherPmtMethodTypeCombo)
				{
					_gLVoucherPmtMethodTypeCombo = value;
					NotifyChanged("GLVoucherPmtMethodTypeCombo");
				}
			}
		}
		public String GLTOF01Combo
		{
			get { return _gLTOF01Combo; }
			set
			{
				if (value != this._gLTOF01Combo)
				{
					_gLTOF01Combo = value;
					NotifyChanged("GLTOF01Combo");
				}
			}
		}
		public String GLTOF02Combo
		{
			get { return _gLTOF02Combo; }
			set
			{
				if (value != this._gLTOF02Combo)
				{
					_gLTOF02Combo = value;
					NotifyChanged("GLTOF02Combo");
				}
			}
		}
		public String GLTOF03Combo
		{
			get { return _gLTOF03Combo; }
			set
			{
				if (value != this._gLTOF03Combo)
				{
					_gLTOF03Combo = value;
					NotifyChanged("GLTOF03Combo");
				}
			}
		}
		public String GLTOF04Combo
		{
			get { return _gLTOF04Combo; }
			set
			{
				if (value != this._gLTOF04Combo)
				{
					_gLTOF04Combo = value;
					NotifyChanged("GLTOF04Combo");
				}
			}
		}
		public String GLTOF05Combo
		{
			get { return _gLTOF05Combo; }
			set
			{
				if (value != this._gLTOF05Combo)
				{
					_gLTOF05Combo = value;
					NotifyChanged("GLTOF05Combo");
				}
			}
		}
		public String GLTOF06Combo
		{
			get { return _gLTOF06Combo; }
			set
			{
				if (value != this._gLTOF06Combo)
				{
					_gLTOF06Combo = value;
					NotifyChanged("GLTOF06Combo");
				}
			}
		}
		public String GLTOF07Combo
		{
			get { return _gLTOF07Combo; }
			set
			{
				if (value != this._gLTOF07Combo)
				{
					_gLTOF07Combo = value;
					NotifyChanged("GLTOF07Combo");
				}
			}
		}
		public String GLTOF08Combo
		{
			get { return _gLTOF08Combo; }
			set
			{
				if (value != this._gLTOF08Combo)
				{
					_gLTOF08Combo = value;
					NotifyChanged("GLTOF08Combo");
				}
			}
		}
		public String GLTOF09Combo
		{
			get { return _gLTOF09Combo; }
			set
			{
				if (value != this._gLTOF09Combo)
				{
					_gLTOF09Combo = value;
					NotifyChanged("GLTOF09Combo");
				}
			}
		}
		public String GLTOF10Combo
		{
			get { return _gLTOF10Combo; }
			set
			{
				if (value != this._gLTOF10Combo)
				{
					_gLTOF10Combo = value;
					NotifyChanged("GLTOF10Combo");
				}
			}
		}
		public String ViewImportVoucherItemInvSeries
		{
			get { return _viewImportVoucherItemInvSeries; }
			set
			{
				if (value != this._viewImportVoucherItemInvSeries)
				{
					_viewImportVoucherItemInvSeries = value;
					NotifyChanged("ViewImportVoucherItemInvSeries");
				}
			}
		}
		public String ViewImportVoucherItemInvNo
		{
			get { return _viewImportVoucherItemInvNo; }
			set
			{
				if (value != this._viewImportVoucherItemInvNo)
				{
					_viewImportVoucherItemInvNo = value;
					NotifyChanged("ViewImportVoucherItemInvNo");
				}
			}
		}
		public Nullable<DateTime> ViewImportVoucherItemInvDate
		{
			get { return _viewImportVoucherItemInvDate; }
			set
			{
				if (value != this._viewImportVoucherItemInvDate)
				{
					_viewImportVoucherItemInvDate = value;
					NotifyChanged("ViewImportVoucherItemInvDate");
				}
			}
		}
		public String GLVoucherItemObjectTypeCombo
		{
			get { return _gLVoucherItemObjectTypeCombo; }
			set
			{
				if (value != this._gLVoucherItemObjectTypeCombo)
				{
					_gLVoucherItemObjectTypeCombo = value;
					NotifyChanged("GLVoucherItemObjectTypeCombo");
				}
			}
		}
		public String ViewImportVoucherItemObjectName
		{
			get { return _viewImportVoucherItemObjectName; }
			set
			{
				if (value != this._viewImportVoucherItemObjectName)
				{
					_viewImportVoucherItemObjectName = value;
					NotifyChanged("ViewImportVoucherItemObjectName");
				}
			}
		}
		public String ViewImportVoucherItemObjectTxNo
		{
			get { return _viewImportVoucherItemObjectTxNo; }
			set
			{
				if (value != this._viewImportVoucherItemObjectTxNo)
				{
					_viewImportVoucherItemObjectTxNo = value;
					NotifyChanged("ViewImportVoucherItemObjectTxNo");
				}
			}
		}
		public int FK_GLTranCfgID
		{
			get { return _fK_GLTranCfgID; }
			set
			{
				if (value != this._fK_GLTranCfgID)
				{
					_fK_GLTranCfgID = value;
					NotifyChanged("FK_GLTranCfgID");
				}
			}
		}
		public int FK_APPOID
		{
			get { return _fK_APPOID; }
			set
			{
				if (value != this._fK_APPOID)
				{
					_fK_APPOID = value;
					NotifyChanged("FK_APPOID");
				}
			}
		}
		public int FK_FAAssetConstructionID
		{
			get { return _fK_FAAssetConstructionID; }
			set
			{
				if (value != this._fK_FAAssetConstructionID)
				{
					_fK_FAAssetConstructionID = value;
					NotifyChanged("FK_FAAssetConstructionID");
				}
			}
		}
		public int FK_GEInvTypeID
		{
			get { return _fK_GEInvTypeID; }
			set
			{
				if (value != this._fK_GEInvTypeID)
				{
					_fK_GEInvTypeID = value;
					NotifyChanged("FK_GEInvTypeID");
				}
			}
		}
		public double ViewImportTaxAmtTot
		{
			get { return _viewImportTaxAmtTot; }
			set
			{
				if (value != this._viewImportTaxAmtTot)
				{
					_viewImportTaxAmtTot = value;
					NotifyChanged("ViewImportTaxAmtTot");
				}
			}
		}
		public int FK_TXTaxCodeID
		{
			get { return _fK_TXTaxCodeID; }
			set
			{
				if (value != this._fK_TXTaxCodeID)
				{
					_fK_TXTaxCodeID = value;
					NotifyChanged("FK_TXTaxCodeID");
				}
			}
		}
		public String ViewImportQuestAddress
		{
			get { return _viewImportQuestAddress; }
			set
			{
				if (value != this._viewImportQuestAddress)
				{
					_viewImportQuestAddress = value;
					NotifyChanged("ViewImportQuestAddress");
				}
			}
		}
		public String ViewImportVoucherDocNo
		{
			get { return _viewImportVoucherDocNo; }
			set
			{
				if (value != this._viewImportVoucherDocNo)
				{
					_viewImportVoucherDocNo = value;
					NotifyChanged("ViewImportVoucherDocNo");
				}
			}
		}
		public double ViewImportTaxFAmtTot
		{
			get { return _viewImportTaxFAmtTot; }
			set
			{
				if (value != this._viewImportTaxFAmtTot)
				{
					_viewImportTaxFAmtTot = value;
					NotifyChanged("ViewImportTaxFAmtTot");
				}
			}
		}
		public String ViewImportMessage
		{
			get { return _viewImportMessage; }
			set
			{
				if (value != this._viewImportMessage)
				{
					_viewImportMessage = value;
					NotifyChanged("ViewImportMessage");
				}
			}
		}
		public Nullable<DateTime> ViewImportVoucherDueDate
		{
			get { return _viewImportVoucherDueDate; }
			set
			{
				if (value != this._viewImportVoucherDueDate)
				{
					_viewImportVoucherDueDate = value;
					NotifyChanged("ViewImportVoucherDueDate");
				}
			}
		}
		public int FK_ICStockID
		{
			get { return _fK_ICStockID; }
			set
			{
				if (value != this._fK_ICStockID)
				{
					_fK_ICStockID = value;
					NotifyChanged("FK_ICStockID");
				}
			}
		}
		public int FK_ICProductID
		{
			get { return _fK_ICProductID; }
			set
			{
				if (value != this._fK_ICProductID)
				{
					_fK_ICProductID = value;
					NotifyChanged("FK_ICProductID");
				}
			}
		}
		public int FK_ICUOMID
		{
			get { return _fK_ICUOMID; }
			set
			{
				if (value != this._fK_ICUOMID)
				{
					_fK_ICUOMID = value;
					NotifyChanged("FK_ICUOMID");
				}
			}
		}
		public int FK_ICStkUOMID
		{
			get { return _fK_ICStkUOMID; }
			set
			{
				if (value != this._fK_ICStkUOMID)
				{
					_fK_ICStkUOMID = value;
					NotifyChanged("FK_ICStkUOMID");
				}
			}
		}
		public double ViewImportItemQty
		{
			get { return _viewImportItemQty; }
			set
			{
				if (value != this._viewImportItemQty)
				{
					_viewImportItemQty = value;
					NotifyChanged("ViewImportItemQty");
				}
			}
		}
		public double ViewImportItemStkQty
		{
			get { return _viewImportItemStkQty; }
			set
			{
				if (value != this._viewImportItemStkQty)
				{
					_viewImportItemStkQty = value;
					NotifyChanged("ViewImportItemStkQty");
				}
			}
		}
		public double ViewImportFUnitPrice
		{
			get { return _viewImportFUnitPrice; }
			set
			{
				if (value != this._viewImportFUnitPrice)
				{
					_viewImportFUnitPrice = value;
					NotifyChanged("ViewImportFUnitPrice");
				}
			}
		}
		public double ViewImportUnitPrice
		{
			get { return _viewImportUnitPrice; }
			set
			{
				if (value != this._viewImportUnitPrice)
				{
					_viewImportUnitPrice = value;
					NotifyChanged("ViewImportUnitPrice");
				}
			}
		}
		public String ViewImportItemLotNo
		{
			get { return _viewImportItemLotNo; }
			set
			{
				if (value != this._viewImportItemLotNo)
				{
					_viewImportItemLotNo = value;
					NotifyChanged("ViewImportItemLotNo");
				}
			}
		}
		public int FK_HRDepartmentID
		{
			get { return _fK_HRDepartmentID; }
			set
			{
				if (value != this._fK_HRDepartmentID)
				{
					_fK_HRDepartmentID = value;
					NotifyChanged("FK_HRDepartmentID");
				}
			}
		}
		public double ViewImportVoucherItemLife
		{
			get { return _viewImportVoucherItemLife; }
			set
			{
				if (value != this._viewImportVoucherItemLife)
				{
					_viewImportVoucherItemLife = value;
					NotifyChanged("ViewImportVoucherItemLife");
				}
			}
		}
		public double ViewImportVoucherItemAllocateAmt
		{
			get { return _viewImportVoucherItemAllocateAmt; }
			set
			{
				if (value != this._viewImportVoucherItemAllocateAmt)
				{
					_viewImportVoucherItemAllocateAmt = value;
					NotifyChanged("ViewImportVoucherItemAllocateAmt");
				}
			}
		}
		public double ViewImportVoucherItemRLife
		{
			get { return _viewImportVoucherItemRLife; }
			set
			{
				if (value != this._viewImportVoucherItemRLife)
				{
					_viewImportVoucherItemRLife = value;
					NotifyChanged("ViewImportVoucherItemRLife");
				}
			}
		}
		public double ViewImportVoucherItemRAmt
		{
			get { return _viewImportVoucherItemRAmt; }
			set
			{
				if (value != this._viewImportVoucherItemRAmt)
				{
					_viewImportVoucherItemRAmt = value;
					NotifyChanged("ViewImportVoucherItemRAmt");
				}
			}
		}
		public int FK_GLCostDistID
		{
			get { return _fK_GLCostDistID; }
			set
			{
				if (value != this._fK_GLCostDistID)
				{
					_fK_GLCostDistID = value;
					NotifyChanged("FK_GLCostDistID");
				}
			}
		}
		public String ViewImportAssetNo
		{
			get { return _viewImportAssetNo; }
			set
			{
				if (value != this._viewImportAssetNo)
				{
					_viewImportAssetNo = value;
					NotifyChanged("ViewImportAssetNo");
				}
			}
		}
		public String ViewImportAssetName
		{
			get { return _viewImportAssetName; }
			set
			{
				if (value != this._viewImportAssetName)
				{
					_viewImportAssetName = value;
					NotifyChanged("ViewImportAssetName");
				}
			}
		}
		public Nullable<DateTime> ViewImportVoucherItemPurchaseDate
		{
			get { return _viewImportVoucherItemPurchaseDate; }
			set
			{
				if (value != this._viewImportVoucherItemPurchaseDate)
				{
					_viewImportVoucherItemPurchaseDate = value;
					NotifyChanged("ViewImportVoucherItemPurchaseDate");
				}
			}
		}
		public int FK_FAAssetCriteriaID
		{
			get { return _fK_FAAssetCriteriaID; }
			set
			{
				if (value != this._fK_FAAssetCriteriaID)
				{
					_fK_FAAssetCriteriaID = value;
					NotifyChanged("FK_FAAssetCriteriaID");
				}
			}
		}
		public Nullable<DateTime> ViewImportVoucherItemDeprDate
		{
			get { return _viewImportVoucherItemDeprDate; }
			set
			{
				if (value != this._viewImportVoucherItemDeprDate)
				{
					_viewImportVoucherItemDeprDate = value;
					NotifyChanged("ViewImportVoucherItemDeprDate");
				}
			}
		}
		public int FK_GEGenerateID
		{
			get { return _fK_GEGenerateID; }
			set
			{
				if (value != this._fK_GEGenerateID)
				{
					_fK_GEGenerateID = value;
					NotifyChanged("FK_GEGenerateID");
				}
			}
		}
		public int FK_FAAssetGroupID
		{
			get { return _fK_FAAssetGroupID; }
			set
			{
				if (value != this._fK_FAAssetGroupID)
				{
					_fK_FAAssetGroupID = value;
					NotifyChanged("FK_FAAssetGroupID");
				}
			}
		}
		public int FK_FAAssetTypeID
		{
			get { return _fK_FAAssetTypeID; }
			set
			{
				if (value != this._fK_FAAssetTypeID)
				{
					_fK_FAAssetTypeID = value;
					NotifyChanged("FK_FAAssetTypeID");
				}
			}
		}
		public int FK_FAAssetSourceID
		{
			get { return _fK_FAAssetSourceID; }
			set
			{
				if (value != this._fK_FAAssetSourceID)
				{
					_fK_FAAssetSourceID = value;
					NotifyChanged("FK_FAAssetSourceID");
				}
			}
		}
		public int FK_APSupplierID
		{
			get { return _fK_APSupplierID; }
			set
			{
				if (value != this._fK_APSupplierID)
				{
					_fK_APSupplierID = value;
					NotifyChanged("FK_APSupplierID");
				}
			}
		}
		public Nullable<DateTime> ViewImportUserDate
		{
			get { return _viewImportUserDate; }
			set
			{
				if (value != this._viewImportUserDate)
				{
					_viewImportUserDate = value;
					NotifyChanged("ViewImportUserDate");
				}
			}
		}
		public String ViewImportErrorField
		{
			get { return _viewImportErrorField; }
			set
			{
				if (value != this._viewImportErrorField)
				{
					_viewImportErrorField = value;
					NotifyChanged("ViewImportErrorField");
				}
			}
		}
		public double ViewImportCostFPeriodAmt
		{
			get { return _viewImportCostFPeriodAmt; }
			set
			{
				if (value != this._viewImportCostFPeriodAmt)
				{
					_viewImportCostFPeriodAmt = value;
					NotifyChanged("ViewImportCostFPeriodAmt");
				}
			}
		}
		public double ViewImportCostPeriodAmt
		{
			get { return _viewImportCostPeriodAmt; }
			set
			{
				if (value != this._viewImportCostPeriodAmt)
				{
					_viewImportCostPeriodAmt = value;
					NotifyChanged("ViewImportCostPeriodAmt");
				}
			}
		}
		public double ViewImportVoucherItemBeginLife
		{
			get { return _viewImportVoucherItemBeginLife; }
			set
			{
				if (value != this._viewImportVoucherItemBeginLife)
				{
					_viewImportVoucherItemBeginLife = value;
					NotifyChanged("ViewImportVoucherItemBeginLife");
				}
			}
		}
		public String GLTOF11Combo
		{
			get { return _gLTOF11Combo; }
			set
			{
				if (value != this._gLTOF11Combo)
				{
					_gLTOF11Combo = value;
					NotifyChanged("GLTOF11Combo");
				}
			}
		}
		public String GLTOF12Combo
		{
			get { return _gLTOF12Combo; }
			set
			{
				if (value != this._gLTOF12Combo)
				{
					_gLTOF12Combo = value;
					NotifyChanged("GLTOF12Combo");
				}
			}
		}
		public String GLTOF13Combo
		{
			get { return _gLTOF13Combo; }
			set
			{
				if (value != this._gLTOF13Combo)
				{
					_gLTOF13Combo = value;
					NotifyChanged("GLTOF13Combo");
				}
			}
		}
		public String GLTOF14Combo
		{
			get { return _gLTOF14Combo; }
			set
			{
				if (value != this._gLTOF14Combo)
				{
					_gLTOF14Combo = value;
					NotifyChanged("GLTOF14Combo");
				}
			}
		}
		public String GLTOF15Combo
		{
			get { return _gLTOF15Combo; }
			set
			{
				if (value != this._gLTOF15Combo)
				{
					_gLTOF15Combo = value;
					NotifyChanged("GLTOF15Combo");
				}
			}
		}
		public double ViewImportItemFPrice
		{
			get { return _viewImportItemFPrice; }
			set
			{
				if (value != this._viewImportItemFPrice)
				{
					_viewImportItemFPrice = value;
					NotifyChanged("ViewImportItemFPrice");
				}
			}
		}
		public double ViewImportItemPrice
		{
			get { return _viewImportItemPrice; }
			set
			{
				if (value != this._viewImportItemPrice)
				{
					_viewImportItemPrice = value;
					NotifyChanged("ViewImportItemPrice");
				}
			}
		}
		
        #endregion
    }
    #endregion
}