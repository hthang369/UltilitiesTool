using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("EDWF-ZKV9-D793-1D2A");
                ExcelFile file = new ExcelFile();

                //DataTable dt = file.Worksheets.ActiveWorksheet.GetDataTable; 
            }
        }

        private void LoadData()
        {
            string strFunction = @"IF(AND([Loai_BT2]=AH,[SL_67]<=3.64,[Chieu_cao]<=3.2),0.435,IF(OR(AND([Loai_BT2]=AH,[SL_67]<=3.64,[Chieu_cao]>3.2,[Chieu_cao]<4.2),AND(OR([Loai_BT2]=AK,[Loai_BT2]=AD),[SL_67]<3.64,[Chieu_cao]>3.2,[Chieu_cao]<=4.2),AND(OR([Loai_BT2]=AK,[Loai_BT2]=AD),[SL_67]>3.64,[SL_67]<=5.15,[Chieu_cao]<=3.2)),0.46,IF(OR(AND([Loai_BT2]=AH,[SL_67]<=3.64,[Chieu_cao]>4.2,[Chieu_cao]<=4.6),AND(OR([Loai_BT2]=AK,[Loai_BT2]=AD),[SL_67]<3.64,[Chieu_cao]>4.2,[Chieu_cao]<=5.1)),0.49,IF(OR(AND([Loai_BT2]=AH,[SL_67]<=3.64,[Chieu_cao]>4.6,[Chieu_cao]<=5.1),AND([Loai_BT2]=AH,[SL_67]>3.64,[SL_67]<=5.15,[Chieu_cao]<=4.2)),0.495,IF(OR(AND([Loai_BT2]=AH,[SL_67]>3.64,[SL_67]<=5.15,[Chieu_cao]>4.2,[Chieu_cao]<=4.6),AND(OR([Loai_BT2]=AK,[Loai_BT2]=AD),[SL_67]>3.64,[SL_67]<=5.15,[Chieu_cao]>4.2,[Chieu_cao]<=5.1)),0.5,IF(AND([Loai_BT2]=AH,[SL_67]>3.64,[SL_67]>=5.15,[Chieu_cao]>4.6,[Chieu_cao]<=6),0.555,IF(AND([Loai_BT2]=AH,[SL_67]>5.15,[SL_67]<=8,[Chieu_cao]<=4.6),0.515,IF(AND([Loai_BT2]=AH,[SL_67]>5.15,[SL_67]<=8,[Chieu_cao]>4.6,[Chieu_cao]<=6),0.575,0))))))))
";
            FunctionClass funcClass = new FunctionClass();
            Function.GetFunctionClass(strFunction, ref funcClass);
            Function.CheckFunctionError(strFunction);
            //string strFunc = Function.GenerateFunctionByClassName(funcClass);
            string strFunc = "";
            
            //Function.ExcuteFunction(funcClass, ref strFunc);
            
        }
    }

    public class Function
    {
        string strPPPP = @"IF ( AND([TS002] = 'TTT', OR ([TS001] = 'Gia súc', [TS001] = 'Gia cầm')), 'Thức ăn gia súc + gia cầm', 
            IF(AND([TS001] = 'Con người', [TS002] = 'Thức ăn'), 'Thực phẩm', 'Sản phẩm khác'))";

        private static char charDauNganCachDieuKien = ',';

        #region string FunctionName
        private static string strFunctionIF = "IF";
        private static string strFunctionAND = "AND";
        private static string strFunctionOR = "OR";
        private static string strFunctionCaseWhen = "CASE WHEN";

        private static char strAdd = '+';
        private static char strSubtract = '-';
        private static char strMultiple = '*';
        private static char strDivide = '/';
        private static List<string> lstParameters;
        #endregion

        #region Lấy tất cả Tham số truyền vào trong Hàm
        //Trả về False nếu tham số cấu hình sai. Ngươc lại trả về danh sách tất cả Tham số có trong Hàm.
        public static Boolean GetAllParametersInFunction(string strFunctions, ref List<string> lstParameters)
        {
            lstParameters = new List<string>();
            Regex reg = new Regex("([\\[]\\w+[\\]])");
            foreach (Match item in reg.Matches(strFunctions))
            {
                lstParameters.AddItem(item.Value);
            }
            return lstParameters.Count > 0;
            #region code cũ
            //string strParam = "";
            //Boolean isParam = false;
            //for (int iIndex = 0; iIndex < strFunctions.Length; iIndex++)
            //{
            //    char charIndex = strFunctions[iIndex];
            //    if (charIndex == '[')
            //    {
            //        isParam = true;
            //    }
            //    else if (charIndex == ']')
            //    {
            //        if (!string.IsNullOrEmpty(strParam) && !lstParameters.Contains(strParam))
            //            lstParameters.Add(strParam);
            //        isParam = false;
            //        strParam = string.Empty;
            //    }
            //    else
            //    {
            //        if (isParam)
            //        {
            //            strParam += charIndex;
            //        }
            //    }
            //}
            //if (isParam)
            //    return false;

            //return true;
            #endregion
        }
        #endregion

        #region Kiểm tra Hàm có đúng định dạng không
        //Trả về True là Hàm bị lỗi, Ngược lại trả về False thì Hàm đúng
        public static Boolean CheckFunctionError(string strFunction)
        {
            if (CheckFunction(strFunction) == false) return true;

            //List<string> lstParameters = new List<string>();
            if (GetAllParametersInFunction(strFunction, ref lstParameters) == false) return true;

            FunctionClass objFunction = new FunctionClass();
            if (GetFunctionClass(strFunction, ref objFunction) == false) return true;

            return false;
        }
        private static bool CheckFunctionClassName(string strFunction, string strClassName)
        {
            return strFunction.Contains(strClassName, RegexOptions.IgnoreCase);
        }
        public static bool CheckFunctionSQLError(string strFunction)
        {
            return !CheckFunctionClassName(strFunction, strFunctionCaseWhen);
        }

        #endregion

        #region Kiểm tra Hàm và Parameter
        private static Boolean CheckFunction(string strFunction)
        {
            int iCount = 0;
            for (int iIndex = 0; iIndex < strFunction.Length; iIndex++)
            {
                char charIndex = strFunction[iIndex];
                if (charIndex == '(')
                    iCount++;
                else if (charIndex == ')')
                    iCount--;
            }
            if (iCount != 0) return false;
            return true;
        }

        private static Boolean CheckParameter(string strFunction)
        {
            int iCount = 0;
            for (int iIndex = 0; iIndex < strFunction.Length; iIndex++)
            {
                char charIndex = strFunction[iIndex];
                if (charIndex == '[')
                    iCount++;
                else if (charIndex == ']')
                    iCount--;
            }
            if (iCount != 0) return false;
            return true;
        }
        #endregion

        #region Cắt chuối Hàm ra danh sách Parameters
        private static Boolean CutParameterByStringFunction(string strFunction, ref List<string> lstParameters)
        {
            lstParameters = new List<string>();

            string strParameter = "";
            //Neu iCount = 0 va neu ki tu ke tiep la dau , thi la mot tham so
            int iCount = 0;
            for (int iIndex = 0; iIndex < strFunction.Length; iIndex++)
            {
                char charIndex = strFunction[iIndex];
                if (charIndex == '(')
                    iCount++;
                else if (charIndex == ')')
                    iCount--;

                if (iCount == 0 && charIndex == charDauNganCachDieuKien)
                {
                    lstParameters.Add(strParameter.Trim());
                    strParameter = "";
                }
                else
                {
                    strParameter += charIndex;
                }

                if (iIndex == strFunction.Length - 1)
                {
                    lstParameters.Add(strParameter.Trim());
                    strParameter = "";
                }
            }
            if (!string.IsNullOrEmpty(strParameter) || iCount < -1) return false;
            return true;
        }
        #endregion

        #region Xử lý Hàm
        public static Boolean GetFunctionClass(string strFunction, ref FunctionClass objFunction)
        {
            strFunction = strFunction.Trim();
            if (CheckFunction(strFunction) == false) return false;

            objFunction = null;
            if (strFunction.StartsWith(strFunctionIF, StringComparison.CurrentCultureIgnoreCase))
            {
                return GetFunctionIF(strFunction, ref objFunction);
            }
            else if (strFunction.StartsWith(strFunctionAND, StringComparison.CurrentCultureIgnoreCase))
            {
                return GetFunctionAND(strFunction, ref objFunction);
            }
            else if (strFunction.StartsWith(strFunctionOR, StringComparison.CurrentCultureIgnoreCase))
            {
                return GetFunctionOR(strFunction, ref objFunction);
            }
            else
            {
                return GetParameter(strFunction, ref objFunction);
            }
        }

        private static Boolean GetFunction(string strFunctionName, string strFunction, ref FunctionClass objFunction)
        {
            objFunction = new FunctionClass();
            objFunction.strFuntionName = strFunctionName;
            Boolean isFunction = true;
            try
            {
                if (strFunction.StartsWith(strFunctionName, StringComparison.CurrentCultureIgnoreCase))
                    strFunction = strFunction.Substring(strFunctionName.Length + 1, strFunction.Length - strFunctionName.Length - 2);
                strFunction = strFunction.Trim();

                if (strFunction.StartsWith("("))
                    strFunction = strFunction.Substring(1, strFunction.Length - 1);
                strFunction = strFunction.Trim();

                List<string> lstParameters = new List<string>();
                if (CutParameterByStringFunction(strFunction, ref lstParameters))
                {
                    if (lstParameters.Count >= 1 && lstParameters.Count <= 3 && strFunctionName == strFunctionIF
                     || lstParameters.Count >= 1 && strFunctionName != strFunctionIF)
                    {
                        for (int iIndex = 0; iIndex < lstParameters.Count; iIndex++)
                        {
                            FunctionClass funcParam = new FunctionClass();
                            if (GetFunctionClass(lstParameters[iIndex], ref funcParam))
                                objFunction.lstFunctions.Add(funcParam);
                            else
                                isFunction = false;
                        }
                    }
                }
            }
            catch { }

            if (isFunction == false)
            {
                objFunction = null;
                return false;
            }

            return true;
        }

        private static Boolean GetParameter(string strFunction, ref FunctionClass objFunction)
        {
            strFunction = strFunction.Trim();
            if (CheckParameter(strFunction) == false) return false;

            objFunction = null;

            string[] lstConditions = { ">=", "<=", "=", ">", "<" };

            foreach (string strCondition in lstConditions)
            {
                if (strFunction.Contains(strCondition))
                {
                    int iPos = strFunction.LastIndexOf(strCondition);
                    string strParam1 = strFunction.Substring(0, iPos);
                    string strParam2 = strFunction.Substring(iPos + strCondition.Length, strFunction.Length - (iPos + strCondition.Length));
                    if (CheckParameter(strParam1) && CheckParameter(strParam2))
                    {
                        objFunction = new FunctionClass();
                        if (strParam1.Contains("[")) objFunction.strFuntionName = strParam1;
                        if (strParam2.Contains("[")) objFunction.strFuntionName = strParam2;
                        objFunction.strCondition = strCondition;
                        objFunction.objValue1 = strParam1;
                        objFunction.objValue2 = strParam2;
                        return true;
                    }
                }
            }

            if (objFunction == null)
            {
                objFunction = new FunctionClass();
                objFunction.objValue1 = strFunction;
                return true;
            }
            return false;
        }
        #endregion

        #region Danh sách các Hàm hổ trợ
        #region IF
        private static Boolean GetFunctionIF(string strFunction, ref FunctionClass objFunction)
        {
            objFunction = new FunctionClass();
            return GetFunction(strFunctionIF, strFunction, ref objFunction);
        }
        #endregion

        #region AND
        private static Boolean GetFunctionAND(string strFunction, ref FunctionClass objFunction)
        {
            objFunction = new FunctionClass();
            return GetFunction(strFunctionAND, strFunction, ref objFunction);
        }
        #endregion

        #region OR
        private static Boolean GetFunctionOR(string strFunction, ref FunctionClass objFunction)
        {
            objFunction = new FunctionClass();
            return GetFunction(strFunctionOR, strFunction, ref objFunction);
        }
        #endregion
        #endregion

        #region Thực thi Hàm
        public static object ExcuteFunction(FunctionClass objFunction, SortedList<string, object> lstParameters)
        {
            string strFunc = string.Empty;
            return GenerateFunction(objFunction, ref strFunc);
            //if (objFunction.strFuntionName == strFunctionIF)
            //{
            //    return ExcuteFunctionIF(objFunction, lstParameters);
            //}
            //else if (objFunction.strFuntionName == strFunctionAND)
            //{
            //    return ExcuteFunctionAND(objFunction, lstParameters);
            //}
            //else if (objFunction.strFuntionName == strFunctionOR)
            //{
            //    return ExcuteFunctionOR(objFunction, lstParameters);
            //}
            //else
            //{
            //    return ExcuteFunctionParameter(objFunction, lstParameters);
            //}
        }
        
        public static object GenerateFunction(FunctionClass objFunction, ref string strFunc)
        {
            if (objFunction.strFuntionName == strFunctionIF)
            {
                return GenerateFunctionIF(objFunction, ref strFunc);
            }
            else if (objFunction.strFuntionName == strFunctionAND)
            {
                return GenerateFunctionAND(objFunction, ref strFunc);
            }
            else if (objFunction.strFuntionName == strFunctionOR)
            {
                return GenerateFunctionOR(objFunction, ref strFunc);
            }
            else
            {
                return GenerateFunctionParameter(objFunction, ref strFunc);
            }
        }
        private static object ExcuteFunctionIF(FunctionClass objFunction, SortedList<string, object> lstParameters)
        {
            try
            {
                //Kiểm tra điều kiện của hàm IF

                Object objLogical_Test = null;
                try
                {
                    if (objFunction.lstFunctions.Count > 0)
                        objLogical_Test = ExcuteFunction(objFunction.lstFunctions[0], lstParameters);
                }
                catch { }

                if (objLogical_Test != null)
                {
                    try
                    {
                        Boolean isTrue = Convert.ToBoolean(objLogical_Test);
                        //Nếu điều kiện là TRUE
                        if (isTrue)
                        {
                            return ExcuteFunction(objFunction.lstFunctions[1], lstParameters);
                        }
                        //Ngược lại
                        else if (objFunction.lstFunctions.Count == 3)
                        {
                            return ExcuteFunction(objFunction.lstFunctions[2], lstParameters);
                        }
                    }
                    catch { }
                }
            }
            catch { }

            return null;
        }
        private static object ExcuteFunctionAND(FunctionClass objFunction, SortedList<string, object> lstParameters)
        {
            Boolean isResult = true;
            Boolean isAND = true;
            try
            {
                for (int iIndex = 0; iIndex < objFunction.lstFunctions.Count; iIndex++)
                {
                    if (isAND == false) break;

                    Object objLogical_Test = ExcuteFunction(objFunction.lstFunctions[iIndex], lstParameters);
                    if (objLogical_Test != null)
                    {
                        try
                        {
                            Boolean isTrue = Convert.ToBoolean(objLogical_Test);
                            if (isTrue == false && isResult)
                            {
                                isResult = false;
                            }
                        }
                        catch { isAND = false; }
                    }
                    else isAND = false;
                }
            }
            catch { }

            if (isAND == false) return null;

            return isResult;
        }
        private static object ExcuteFunctionOR(FunctionClass objFunction, SortedList<string, object> lstParameters)
        {
            Boolean isResult = false;
            Boolean isOR = true;
            try
            {
                for (int iIndex = 0; iIndex < objFunction.lstFunctions.Count; iIndex++)
                {
                    if (isOR == false) break;

                    Object objLogical_Test = ExcuteFunction(objFunction.lstFunctions[iIndex], lstParameters);
                    if (objLogical_Test != null)
                    {
                        try
                        {
                            Boolean isTrue = Convert.ToBoolean(objLogical_Test);
                            if (isTrue)
                            {
                                isResult = true;
                            }
                        }
                        catch { isOR = false; }
                    }
                    else isOR = false;
                }
            }
            catch { }

            if (isOR == false) return null;

            return isResult;
        }
        private static object ExcuteFunctionParameter(FunctionClass objFunction, SortedList<string, object> lstParameters)
        {
            try
            {
                if (objFunction.objValue1 != null && objFunction.objValue2 == null)
                {
                    return GetObjectValueFromParameter(objFunction.objValue1, lstParameters);
                }
                else if (objFunction.objValue1 != null && objFunction.objValue2 != null)
                {
                    object objParameter1 = GetObjectValueFromParameter(objFunction.objValue1, lstParameters);
                    object objParameter2 = GetObjectValueFromParameter(objFunction.objValue2, lstParameters);
                    if (objParameter1 != null && objParameter2 != null)
                    {
                        double dbParam1 = 0, dbParam2 = 0;
                        DateTime? dtParam1 = null, dtParam2 = null;
                        int iType = 0; //Type: 0 -> string; 1 -> double; 2 -> date
                        try
                        {
                            dbParam1 = Convert.ToDouble(objParameter1);
                            dbParam2 = Convert.ToDouble(objParameter2);
                            iType = 1;
                        }
                        catch { }

                        if (iType == 0)
                        {
                            try
                            {
                                dtParam1 = Convert.ToDateTime(objParameter1);
                                dtParam2 = Convert.ToDateTime(objParameter2);
                                iType = 2;
                            }
                            catch { }
                        }

                        if (iType == 1)
                        {
                            if (objFunction.strCondition == ">=")
                            {
                                if (dbParam1 >= dbParam2) return true;
                            }
                            else if (objFunction.strCondition == "<=")
                            {
                                if (dbParam1 <= dbParam2) return true;
                            }
                            else if (objFunction.strCondition == "=")
                            {
                                if (dbParam1 == dbParam2) return true;
                            }
                            else if (objFunction.strCondition == ">")
                            {
                                if (dbParam1 > dbParam2) return true;
                            }
                            else if (objFunction.strCondition == "<")
                            {
                                if (dbParam1 < dbParam2) return true;
                            }
                        }
                        else if (iType == 2 && dtParam1 != null && dtParam2 != null)
                        {
                            if (objFunction.strCondition == ">=")
                            {
                                if (dtParam1 >= dtParam2) return true;
                            }
                            else if (objFunction.strCondition == "<=")
                            {
                                if (dtParam1 <= dtParam2) return true;
                            }
                            else if (objFunction.strCondition == "=")
                            {
                                if (dtParam1 == dtParam2) return true;
                            }
                            else if (objFunction.strCondition == ">")
                            {
                                if (dtParam1 > dtParam2) return true;
                            }
                            else if (objFunction.strCondition == "<")
                            {
                                if (dtParam1 < dtParam2) return true;
                            }
                        }
                        else
                        {
                            if (objParameter1.ToString() == objParameter2.ToString())
                                return true;
                        }

                        return false;
                    }
                }
            }
            catch { }

            return null;
        }
        private static object GenerateFunctionIF(FunctionClass objFunction, ref string strFunc)
        {
            try
            {
                if (objFunction.lstFunctions.Count > 0) strFunc += "(";
                for (int iIndex = 0; iIndex < objFunction.lstFunctions.Count; iIndex++)
                {
                    if (iIndex == 1 && objFunction.lstFunctions[iIndex].lstFunctions.Count == 0) strFunc += " ? ";
                    object objLogical_Test = GenerateFunction(objFunction.lstFunctions[iIndex], ref strFunc);
                    if (iIndex == 1 && objFunction.lstFunctions[iIndex].lstFunctions.Count == 0) strFunc += " : ";
                }
                if (objFunction.lstFunctions.Count > 0) strFunc += ")";
            }
            catch { }

            return null;
        }
        private static object GenerateFunctionAND(FunctionClass objFunction, ref string strFunc)
        {
            try
            {
                if (objFunction.lstFunctions.Count > 0) strFunc += "(";
                for (int iIndex = 0; iIndex < objFunction.lstFunctions.Count; iIndex++)
                {
                    object objLogical_Test = GenerateFunction(objFunction.lstFunctions[iIndex], ref strFunc);
                    if (objLogical_Test != null && iIndex != objFunction.lstFunctions.Count - 1)
                    {
                        strFunc += " && ";
                    }
                }
                if (objFunction.lstFunctions.Count > 0) strFunc += ")";
            }
            catch { }
            return strFunc;
        }
        private static object GenerateFunctionOR(FunctionClass objFunction, ref string strFunc)
        {
            try
            {
                if (objFunction.lstFunctions.Count > 0) strFunc += "(";
                for (int iIndex = 0; iIndex < objFunction.lstFunctions.Count; iIndex++)
                {
                    Object objLogical_Test = GenerateFunction(objFunction.lstFunctions[iIndex], ref strFunc);
                    if (objLogical_Test != null && iIndex != objFunction.lstFunctions.Count - 1)
                    {
                        strFunc += " || ";
                    }
                }
                if (objFunction.lstFunctions.Count > 0) strFunc += ")";
            }
            catch { }
            return strFunc;
        }
        private static object GenerateFunctionParameter(FunctionClass objFunction, ref string strFunc)
        {
            if (objFunction.strCondition != null)
                strFunc += string.Format("({0} {1} {2})", objFunction.objValue1, objFunction.strCondition, objFunction.objValue2);
            else
                strFunc += objFunction.objValue1;
            return strFunc;
        }
        private static object GetObjectValueFromParameter(Object objValue, SortedList<string, object> lstParameters)
        {
            if (objValue != null)
            {
                return GetValueParameter(objValue.ToString().Trim(), lstParameters);
            }
            return null;
        }
        private static Object GetValueParameter(string strParameter, SortedList<string, object> lstParameters)
        {
            strParameter = strParameter.Trim();
            Object dbResult = 0;
            try
            {
                List<string> lstParameterTemps = new List<string>();

                //Nếu Tham số là một nhóm chỉ có nhân chia không có cộng trừ
                if (CheckParameterOneGroup(strParameter))
                {
                    //Nếu Tham số là một nhóm có chứa nhân chia
                    if (CheckParameterOneGroupExistMultipleORDivide(strParameter))
                    {
                        //Chia các tham số ra từng nhóm
                        lstParameterTemps = SplitParameterOneGroupExistMultipleORDivide(strParameter);
                    }
                    else //Nếu Tham số là một nhóm mà không có nhân chia
                    {
                        Object objValue = 0;
                        //Kiểm tra tham số có giá trị
                        if (CheckParamValue(strParameter, lstParameters, ref objValue))
                        {
                            dbResult = objValue;
                        }
                        else //Nếu tham số không có giá trị, tức là một bộ tham số khác
                        {
                            //Xóa dấu "(" hay ")" của tham số rồi gọi hàm tính lại giá trị tham số
                            if (strParameter.StartsWith("(")) strParameter = strParameter.Substring(1, strParameter.Length - 1);
                            if (strParameter.EndsWith(")")) strParameter = strParameter.Substring(0, strParameter.Length - 1);
                            dbResult = GetValueParameter(strParameter, lstParameters);
                        }
                    }
                }
                else //Nếu Tham số có chứa Cộng Trừ
                {
                    //Chia các tham số ra từng nhóm
                    lstParameterTemps = SplitParameter(strParameter);
                }

                if (lstParameterTemps.Count > 0)
                {
                    double dbValue = -1;
                    double dbTemp = 0;
                    for (int iIndex = 0; iIndex < lstParameterTemps.Count; iIndex++)
                    {
                        try
                        {
                            string strParam = lstParameterTemps[iIndex];
                            if (iIndex <= lstParameterTemps.Count - 2)
                            {
                                if (iIndex == 0)
                                    dbTemp = Convert.ToDouble(GetValueParameter(lstParameterTemps[iIndex], lstParameters));
                                else
                                {
                                    dbTemp = Convert.ToDouble(GetValueParameter(lstParameterTemps[iIndex + 1], lstParameters));
                                    iIndex++;
                                }

                            }
                            else dbTemp = 0;
                            if (strParam == strAdd.ToString())
                            {
                                dbValue = dbValue + dbTemp;
                            }
                            else if (strParam == strSubtract.ToString())
                            {
                                dbValue = dbValue - dbTemp;
                            }
                            else if (strParam == strMultiple.ToString())
                            {
                                dbValue = dbValue * dbTemp;
                            }
                            else if (strParam == strDivide.ToString())
                            {
                                if (dbTemp == 0)
                                    dbValue = 0;
                                else
                                    dbValue = dbValue / dbTemp;
                            }
                            else if (dbValue == -1)
                                dbValue = dbTemp;
                        }
                        catch { }
                    }
                    dbResult = dbValue;
                }
            }
            catch { }
            return dbResult;
        }
        private static Boolean CheckParamValue(string strParameter, SortedList<string, object> lstParameters, ref Object objValue)
        {
            int iCount = 0;

            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '[') iCount++;

                //Trong tham số có ký tự tính toán
                if (charIndex == strAdd ||
                        charIndex == strSubtract ||
                        charIndex == strMultiple ||
                        charIndex == strDivide ||
                        charIndex == '(' ||
                        charIndex == ')')
                    return false;
            }

            //Có hơn 2 tham số trong strParameter
            if (iCount > 1) return false;

            if (iCount == 1)
            {
                string strParam = strParameter.ToString().Replace("[", "").Replace("]", "").Trim();
                if (lstParameters.ContainsKey(strParam))
                    objValue = lstParameters[strParam];
            }
            else
            {
                objValue = strParameter.Trim();
            }
            return true;
        }
        #region Phép tính toán
        private static void GetValueParameter(string strParameter, SortedList<string, object> lstParameters, ref Object objValue, ref string strReward)
        {
            strParameter = strParameter.Trim();

            Object dbResult = 0;

            List<string> lstParameterTemps = new List<string>();

            //Nếu Tham số là một nhóm chỉ có nhân chia không có cộng trừ
            if (CheckParameterOneGroup(strParameter))
            {
                //Nếu Tham số là một nhóm có chứa nhân chia
                if (CheckParameterOneGroupExistMultipleORDivide(strParameter))
                {
                    //Chia các tham số ra từng nhóm
                    lstParameterTemps = SplitParameterOneGroupExistMultipleORDivide(strParameter);
                }
                else //Nếu Tham số là một nhóm mà không có nhân chia
                {
                    //170323 - T.Bao thêm tham số để trả về sản phẩm trả thưởng
                    //Kiểm tra tham số có giá trị
                    if (CheckParamValue(strParameter, lstParameters, ref objValue, ref strReward))
                    {
                        dbResult = objValue;
                    }
                    else //Nếu tham số không có giá trị, tức là một bộ tham số khác
                    {
                        //Xóa dấu "(" hay ")" của tham số rồi gọi hàm tính lại giá trị tham số
                        if (strParameter.StartsWith("(")) strParameter = strParameter.Substring(1, strParameter.Length - 1);
                        if (strParameter.EndsWith(")")) strParameter = strParameter.Substring(0, strParameter.Length - 1);

                        GetValueParameter(strParameter, lstParameters, ref objValue, ref strReward);
                    }
                }
            }
            else //Nếu Tham số có chứa Cộng Trừ
            {
                //Chia các tham số ra từng nhóm
                lstParameterTemps = SplitParameter(strParameter);
            }

            if (lstParameterTemps.Count > 0)
            {
                double dbValue = -1;
                //double dbTemp = 0;
                for (int iIndex = 0; iIndex < lstParameterTemps.Count; iIndex++)
                {
                    try
                    {
                        string strParam = lstParameterTemps[iIndex];
                        if (iIndex <= lstParameterTemps.Count - 2)
                        {
                            if (iIndex == 0)
                                GetValueParameter(lstParameterTemps[iIndex], lstParameters, ref objValue, ref strReward);
                            else
                            {
                                GetValueParameter(lstParameterTemps[iIndex + 1], lstParameters, ref objValue, ref strReward);
                                iIndex++;
                            }

                        }
                        else objValue = 0;
                        if (strParam == strAdd.ToString())
                        {
                            dbValue = dbValue + Convert.ToDouble(objValue);
                        }
                        else if (strParam == strSubtract.ToString())
                        {
                            dbValue = dbValue - Convert.ToDouble(objValue);
                        }
                        else if (strParam == strMultiple.ToString())
                        {
                            dbValue = dbValue * Convert.ToDouble(objValue);
                        }
                        else if (strParam == strDivide.ToString())
                        {
                            if (Convert.ToDouble(objValue) == 0)
                                dbValue = 0;
                            else
                                dbValue = dbValue / Convert.ToDouble(objValue);
                        }
                        else if (dbValue == -1)
                            dbValue = Convert.ToDouble(objValue);
                    }
                    catch { }
                }
                objValue = dbValue;
            }
        }
        //170323 - T.Bao thêm tham số để trả về sản phẩm trả thưởng
        private static Boolean CheckParamValue(string strParameter, SortedList<string, object> lstParameters, ref Object objValue, ref string strReward)
        {
            int iCount = 0;

            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '[') iCount++;

                //Trong tham số có ký tự tính toán
                if (charIndex == strAdd ||
                        charIndex == strSubtract ||
                        charIndex == strMultiple ||
                        charIndex == strDivide ||
                        charIndex == '(' ||
                        charIndex == ')')
                    return false;
            }

            //Có hơn 2 tham số trong strParameter
            if (iCount > 1) return false;

            if (iCount == 1)
            {
                string strParam = strParameter.ToString().Replace("[", "").Replace("]", "").Trim();
                if (lstParameters.ContainsKey(strParam))
                {
                    objValue = lstParameters[strParam];
                    strReward = strParam;
                }


            }
            else
            {
                objValue = strParameter.Trim();
                strReward = "";
            }
            return true;
        }

        private static Boolean CheckParameterOneGroup(string strParameter)
        {
            int iCount = 0;
            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '(') iCount++;
                else if (charIndex == ')') iCount--;
                else if (iCount == 0)
                {
                    if (charIndex == strAdd || charIndex == strSubtract)
                        return false;
                }
            }

            return true;
        }

        private static Boolean CheckParameterOneGroupExistMultipleORDivide(string strParameter)
        {
            int iCount = 0;
            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '(') iCount++;
                else if (charIndex == ')') iCount--;
                else if (iCount == 0)
                {
                    if (charIndex == strMultiple || charIndex == strDivide)
                        return true;
                }
            }

            return false;
        }
        private static List<String> SplitParameter(string strParameter)
        {
            int iCount = 0;
            string strParam = "";
            List<string> lstParams = new List<string>();
            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '(') iCount++;
                else if (charIndex == ')') iCount--;

                if (iCount == 0 && (charIndex == strAdd || charIndex == strSubtract))
                {
                    lstParams.Add(strParam.Trim());
                    lstParams.Add(charIndex.ToString());
                    strParam = "";
                }
                else
                    strParam += charIndex;

                if (iIndex == strParameter.Length - 1 && !string.IsNullOrEmpty(strParam))
                    lstParams.Add(strParam.Trim());
            }

            return lstParams;
        }

        private static List<String> SplitParameterOneGroupExistMultipleORDivide(string strParameter)
        {
            int iCount = 0;
            string strParam = "";
            List<string> lstParams = new List<string>();
            for (int iIndex = 0; iIndex < strParameter.Length; iIndex++)
            {
                char charIndex = strParameter[iIndex];
                if (charIndex == '(') iCount++;
                else if (charIndex == ')') iCount--;

                if (iCount == 0 && (charIndex == strMultiple || charIndex == strDivide))
                {
                    lstParams.Add(strParam.Trim());
                    lstParams.Add(charIndex.ToString());
                    strParam = "";
                }
                else
                    strParam += charIndex;

                if (iIndex == strParameter.Length - 1 && !string.IsNullOrEmpty(strParam))
                    lstParams.Add(strParam.Trim());
            }

            return lstParams;
        }
        #endregion

        #endregion
    }
    public static class StringExtensions
    {
        public static string SearchRegex(this string[] name, string regex)
        {
            if (name == null) return string.Empty;
            var objStr = name.Where(x => x.Contains(regex)).Select(x => x.ToString());
            if (objStr == null || objStr.ToList().Count == 0) return string.Empty;
            return objStr.First();
        }
        public static string RegexString(this string fieldName, string strRegex = @"(\d)")
        {
            Regex myregex = new Regex(strRegex);
            return myregex.Replace(fieldName, "");
        }
        public static bool Contains(this string str, string regex, RegexOptions option)
        {
            Regex myregex = new Regex(string.Format("({0})", regex), option);
            return myregex.IsMatch(str);
        }
    }
    public class FunctionClass
    {
        public string strFuntionName { get; set; }
        public List<FunctionClass> lstFunctions { get; set; }
        public object objValue1 { get; set; }
        public object objValue2 { get; set; }

        public string strCondition { get; set; }

        public FunctionClass()
        {
            strFuntionName = string.Empty;
            lstFunctions = new List<FunctionClass>();
            objValue1 = null;
            objValue2 = null;
        }
    }
    public static class ListExtra
    {
        public static void AddItem<T>(this List<T> lst, T objValue)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(objValue)) && !lst.Contains(objValue))
                lst.Add(objValue);
            else
            {
                int idx = lst.IndexOf(objValue);
                if (idx > -1)
                    lst[idx] = objValue;
            }
        }
        
        public static void AddItem<TKey, TVal>(this Dictionary<TKey, TVal> dic, TKey objKey, TVal objVal)
        {
            if (!dic.ContainsKey(objKey))
                dic.Add(objKey, objVal);
            else
                dic[objKey] = objVal;
        }
        public static void AddItem<TKey, TVal>(this SortedList<TKey, TVal> dic, TKey objKey, TVal objValue)
        {
            if (!dic.ContainsKey(objKey)) dic.Add(objKey, objValue);
            else dic[objKey] = objValue;
        }
        
        public static object Invoke<T>(this ICollection<T> lst, string invokeName, params object[] objVals)
        {
            System.Reflection.MethodInfo mi;
            try
            {
                mi = lst.GetType().GetMethod(invokeName);
            }
            catch
            {
                mi = lst.GetType().GetMethods().Where(x => x.Name.Equals(invokeName)).FirstOrDefault();
            }
            if (mi != null)
                return mi.Invoke(lst, objVals);
            return null;
        }
        public static string GetField(this DataRow r, string strName)
        {
            object obj = r[strName];
            if (obj.GetType() == typeof(DateTime))
                return Convert.ToDateTime(obj).ToShortDateString();
            return Convert.ToString(obj);
        }
        public static object GetFieldName(this DataRow r, string strName)
        {
            if (r.Table.Columns.Contains(strName))
                return r[strName];
            return null;
        }
        public static T[] AddItem<T>(this T[] arr, T oVal)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr.SetValue(oVal, arr.Length - 1);
            return arr;
        }
    }
}
