IF (OBJECT_ID('[dbo].[Sp_AR_Liabilities]') IS NOT NULL)
	DROP PROCEDURE [dbo].[Sp_AR_Liabilities]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC Sp_AR_Liabilities
    (
      @FromDate DATETIME ,
      @ToDate DATETIME ,
      @CurrencyID INT ,
      @StringAccount NVARCHAR(MAX) ,
      @ObjectType VARCHAR(20) ,
      @ObjectID INT ,
      @CustomerString NVARCHAR(MAX),
	  @EmployeeString NVARCHAR(MAX)
    )
AS
    BEGIN 
--SET @CustomerString = REPLACE(@CustomerString,'''','''''')
--SET @EmployeeString = REPLACE(@EmployeeString,'''','''''')
---- Chuỗi điều kiện Ocode khách hàng
CREATE TABLE #CusList  (ARCustomerID INT)
DECLARE @SqlCusList NVARCHAR(MAX)
SET @SqlCusList = 'INSERT INTO #CusList ' +
					@CustomerString
--PRINT @SqlCusList
EXEC (@SqlCusList)
-----Chuỗi điều kiện Ocode nhân viên
CREATE TABLE #EmpList  (HREmployeeID INT)
DECLARE @SqlEmpList NVARCHAR(MAX)
SET @SqlEmpList = 'INSERT INTO #EmpList ' +
					@EmployeeString
--PRINT @SqlCusList
EXEC (@SqlEmpList)
-----

	-- Du lieu phat sinh No co cua tai khoan trong thoi gian loc theo loai tien te
        SELECT  Temp.AccountID ,
                Temp.FK_GECurrencyID ,
                Temp.FK_APSupplierID ,
                Temp.FK_ARCustomerID ,
                Temp.FK_HREmployeeID ,
                Temp.GLJournalDate ,
                Temp.GLJournalPeriod ,
                Temp.GLJournalYear ,
                SUM(Temp.GLJournalDebitAmt) GLJournalDebitAmt ,
                SUM(Temp.GLJournalFDebitAmt) GLJournalFDebitAmt ,
                SUM(Temp.GLJournalCreditAmt) GLJournalCreditAmt ,
                SUM(Temp.GLJournalFCreditAmt) GLJournalFCreditAmt
        INTO    #TemJournal
        FROM    ( SELECT    Journal.GLJournalID ,
                            Journal.FK_GLDebitAccountID AccountID ,
                            Journal.FK_GECurrencyID ,
                            Journal.FK_APSupplierID ,
                            Journal.FK_ARCustomerID ,
                            Journal.FK_HREmployeeID ,
                            Journal.GLJournalDate ,
                            GLJournalPeriod ,
                            GLJournalYear ,
                            Journal.GLJournalAmt GLJournalDebitAmt ,
                            Journal.GLJournalFAmt GLJournalFDebitAmt ,
                            0 GLJournalCreditAmt ,
                            0 GLJournalFCreditAmt
                  FROM      Fnc_JournalDetail(@FromDate, @ToDate,
                                              @StringAccount) Journal
                            JOIN dbo.GLAccounts Acc ON ( Acc.GLAccountID = Journal.FK_GLDebitAccountID
                                                         AND Acc.AAStatus = 'Alive'
                                                       )
                            JOIN ( SELECT   Account
                                   FROM     Fnc_GetListAccountDetail(@StringAccount)
                                 ) s ON ( Acc.GLAccountID = s.Account )
                  WHERE     ( GLJournalPeriod <> 0
                              AND CONVERT(DATE, GLJournalDate) BETWEEN CONVERT(DATE, @FromDate)
                                                              AND
                                                              CONVERT(DATE, @ToDate)
                            )
                            AND ( Journal.FK_GECurrencyID = @CurrencyID
                                  OR @CurrencyID = 0
                                )
                  UNION ALL
                  SELECT    Journal.GLJournalID ,
                            Journal.FK_GLCreditAccountID ,
                            Journal.FK_GECurrencyID ,
                            CASE WHEN (FK_APSupplierIDCredit = 0 AND FK_ARCustomerIDCredit = 0 AND FK_HREmployeeIDCredit = 0)
                                         THEN Journal.FK_APSupplierID
                                         ELSE Journal.FK_APSupplierIDCredit
                                    END FK_APSupplierID ,
                                    CASE WHEN (FK_APSupplierIDCredit = 0 AND FK_ARCustomerIDCredit = 0 AND FK_HREmployeeIDCredit = 0)
                                         THEN Journal.FK_ARCustomerID
                                         ELSE Journal.FK_ARCustomerIDCredit
                                    END FK_ARCustomerID ,
                                    CASE WHEN (FK_APSupplierIDCredit = 0 AND FK_ARCustomerIDCredit = 0 AND FK_HREmployeeIDCredit = 0)
                                         THEN Journal.FK_HREmployeeID
                                         ELSE Journal.FK_HREmployeeIDCredit
                                    END FK_HREmployeeID ,

                            Journal.GLJournalDate ,
                            GLJournalPeriod ,
                            GLJournalYear ,
                            0 GLJournalDebitAmt ,
                            0 GLJournalFDeditAmt ,
                            Journal.GLJournalAmt GLJournalCreditAmt ,
                            Journal.GLJournalFAmt GLJournalFCreditAmt
                  FROM      Fnc_JournalDetail(@FromDate, @ToDate,
                                              @StringAccount) Journal
                            JOIN dbo.GLAccounts Acc ON ( Acc.GLAccountID = Journal.FK_GLCreditAccountID
                                                         AND Acc.AAStatus = 'Alive'
                                                       )
                            JOIN ( SELECT   Account
                                   FROM     Fnc_GetListAccountDetail(@StringAccount)
                                 ) s ON ( Acc.GLAccountID = s.Account )
                  WHERE     ( GLJournalPeriod <> 0
                              AND CONVERT(DATE, GLJournalDate) BETWEEN CONVERT(DATE, @FromDate)
                                                              AND
                                                              CONVERT(DATE, @ToDate)
                            )
                            AND ( Journal.FK_GECurrencyID = @CurrencyID
                                  OR @CurrencyID = 0
                                )
                ) Temp
        GROUP BY Temp.AccountID ,
                Temp.FK_GECurrencyID ,
                Temp.FK_APSupplierID ,
                Temp.FK_ARCustomerID ,
                Temp.FK_HREmployeeID ,
                Temp.GLJournalDate ,
                Temp.GLJournalPeriod ,
                Temp.GLJournalYear;
----Phát sinh trong kì theo nhóm đối tượng											 
        SELECT  Temp.AccountID ,
                Temp.ObjectID ,
                Temp.GLObjectNo ,
                Temp.GLObjectName ,
                Temp.OCode1 ,
                Temp.OCode2 ,
                Temp.Ocode3 ,
                Temp.Ocode4 ,
                Temp.Ocode5 ,
                Temp.Ocode6 ,
                Temp.Ocode7 ,
                Temp.Ocode8 ,
                Temp.Ocode9 ,
                Temp.Ocode10 ,
                -- Temp.Ocode11 ,
                -- Temp.Ocode12 ,
                -- Temp.Ocode13 ,
                -- Temp.Ocode14 ,
                -- Temp.Ocode15 ,
                Temp.ObjectType ,
                Cur.GECurrencyNo ,
                SUM(Temp.GLJournalDebitAmt) ArisingDebitAmt ,
                SUM(Temp.GLJournalCreditAmt) ArisingCreditAmt ,
                SUM(Temp.GLJournalFDebitAmt) ArisingFDebitAmt ,
                SUM(Temp.GLJournalFCreditAmt) ArisingFCreditAmt
        INTO    #TempArising
        FROM    ( SELECT    journal.AccountID ,
                            journal.FK_GECurrencyID ,
                            Obj.GLObjectID ObjectID ,
                            Obj.GLObjectNo ,
                            Obj.GLObjectName ,
                            Cus.ARCustomerOOF01Combo OCode1 ,
                            Cus.ARCustomerOOF02Combo OCode2 ,
                            Cus.ARCustomerOOF03Combo Ocode3 ,
                            Cus.ARCustomerOOF04Combo Ocode4 ,
                            Cus.ARCustomerOOF05Combo Ocode5 ,
                            Cus.ARCustomerOOF06Combo Ocode6 ,
                            Cus.ARCustomerOOF07Combo Ocode7 ,
                            Cus.ARCustomerOOF08Combo Ocode8 ,
                            Cus.ARCustomerOOF09Combo Ocode9 ,
                            Cus.ARCustomerOOF10Combo Ocode10 ,
                            -- Cus.ARCustomerOOF11Combo Ocode11 ,
                            -- Cus.ARCustomerOOF12Combo Ocode12 ,
                            -- Cus.ARCustomerOOF13Combo Ocode13 ,
                            -- Cus.ARCustomerOOF14Combo Ocode14 ,
                            -- Cus.ARCustomerOOF15Combo Ocode15 ,
                            journal.GLJournalDate ,
                            journal.GLJournalPeriod ,
                            journal.GLJournalYear ,
                            GLJournalDebitAmt ,
                            GLJournalFDebitAmt ,
                            GLJournalCreditAmt ,
                            GLJournalFCreditAmt ,
                            Obj.GLObjectType AS ObjectType
                  FROM      #TemJournal journal
                            JOIN dbo.ARCustomers Cus ON ( Cus.ARCustomerID = journal.FK_ARCustomerID
                                                          AND Cus.AAStatus = 'Alive'
                                                        )
                            JOIN dbo.GLObjects Obj ON ( Cus.ARCustomerID = Obj.FK_ObjectRefID
                                                        AND Obj.AAStatus = 'Alive'
                                                        AND Obj.GLObjectType = 'ARCustomers'
                                                      )
							JOIN #CusList ON (#CusList.ARCustomerID = Cus.ARCustomerID)
                  WHERE     ( @ObjectType = 'ARCustomers'
                              OR @ObjectType = ''
                            )
                            AND ( @ObjectID = 0
                                  OR Obj.GLObjectID = @ObjectID
                                )
                  UNION ALL
                  SELECT    journal.AccountID ,
                            journal.FK_GECurrencyID ,
                            Obj.GLObjectID ObjectID ,
                            Obj.GLObjectNo ,
                            Obj.GLObjectName ,
                            Emp.HREmployeeOOF01Combo ,
                            Emp.HREmployeeOOF02Combo ,
                            Emp.HREmployeeOOF03Combo ,
                            Emp.HREmployeeOOF04Combo ,
                            Emp.HREmployeeOOF05Combo ,
                            Emp.HREmployeeOOF06Combo ,
                            Emp.HREmployeeOOF07Combo ,
                            Emp.HREmployeeOOF08Combo ,
                            Emp.HREmployeeOOF09Combo ,
                            Emp.HREmployeeOOF10Combo ,
                            -- Emp.HREmployeeOOF11Combo ,
                            -- Emp.HREmployeeOOF12Combo ,
                            -- '' ,
                            -- '' ,
                            -- '' ,
                            journal.GLJournalDate ,
                            journal.GLJournalPeriod ,
                            journal.GLJournalYear ,
                            GLJournalDebitAmt ,
                            GLJournalFDebitAmt ,
                            GLJournalCreditAmt ,
                            GLJournalFCreditAmt ,
                            Obj.GLObjectType AS ObjectType
                  FROM      #TemJournal journal
                            JOIN dbo.HREmployees Emp ON ( Emp.HREmployeeID = journal.FK_HREmployeeID
                                                          AND Emp.AAStatus = 'Alive'
                                                        )
                            JOIN dbo.GLObjects Obj ON ( Emp.HREmployeeID = Obj.FK_ObjectRefID
                                                        AND Obj.AAStatus = 'Alive'
                                                        AND Obj.GLObjectType = 'HREmployees'
                                                      )
							JOIN #EmpList ON (#EmpList.HREmployeeID = Emp.HREmployeeID)
                  WHERE     ( @ObjectType = 'HREmployees'
                              OR @ObjectType = ''
                            )
                            AND ( @ObjectID = 0
                                  OR Obj.GLObjectID = @ObjectID
                                )
                  UNION ALL
                  SELECT    journal.AccountID ,
                            journal.FK_GECurrencyID ,
                            0 ObjectID ,
                            'UnKnow' ,
                            'UnKnow' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            '' ,
                            -- '' ,
                            -- '' ,
                            -- '' ,
                            -- '' ,
                            -- '' ,
                            journal.GLJournalDate ,
                            journal.GLJournalPeriod ,
                            journal.GLJournalYear ,
                            GLJournalDebitAmt ,
                            GLJournalFDebitAmt ,
                            GLJournalCreditAmt ,
                            GLJournalFCreditAmt ,
                            'UnKnow' AS ObjectType
                  FROM      #TemJournal journal
                  WHERE     
                            journal.FK_APSupplierID = 0
                            AND journal.FK_ARCustomerID = 0
                            AND journal.FK_HREmployeeID = 0
                            AND @ObjectType = ''
                            AND @ObjectID = 0
                            AND @CustomerString = 'SELECT ARCustomerID FROM ARCustomers WHERE AAStatus = ''Alive'''
							AND @EmployeeString = 'SELECT HREmployeeID FROM HREmployees WHERE AAStatus = ''Alive'''
                ) AS Temp
                LEFT JOIN dbo.GECurrencys Cur ON ( Cur.GECurrencyID = Temp.FK_GECurrencyID
                                              AND Cur.AAStatus = 'Alive'
                                            )
        GROUP BY Temp.AccountID ,
                Temp.ObjectID ,
                Temp.GLObjectNo ,
                Temp.GLObjectName ,
                Temp.OCode1 ,
                Temp.OCode2 ,
                Temp.Ocode3 ,
                Temp.Ocode4 ,
                Temp.Ocode5 ,
                Temp.Ocode6 ,
                Temp.Ocode7 ,
                Temp.Ocode8 ,
                Temp.Ocode9 ,
                Temp.Ocode10 ,
                -- Temp.Ocode11 ,
                -- Temp.Ocode12 ,
                -- Temp.Ocode13 ,
                -- Temp.Ocode14 ,
                -- Temp.Ocode15 ,
                Temp.ObjectType ,
                Cur.GECurrencyNo;

	----- So du dau ki cua doi tuong theo tai khoan, tien te
	----- Phat sinh trong ki

        SELECT  AccountID ,
                ObjectID ,
                GLObjectNo ,
                GLObjectName ,
                OCode1 ,
                OCode2 ,
                Ocode3 ,
                Ocode4 ,
                Ocode5 ,
                Ocode6 ,
                Ocode7 ,
                Ocode8 ,
                Ocode9 ,
                Ocode10 ,
                -- Ocode11 ,
                -- Ocode12 ,
                -- Ocode13 ,
                -- Ocode14 ,
                -- Ocode15 ,
                ObjectType ,
                GeCurrencyNo ,
                BeginDebit ,
                BeginCredit ,
                BeginFDebit ,
                BeginFCredit ,
                0 ArisingDebitAmt ,
                0 ArisingCreditAmt ,
                0 ArisingFDebitAmt ,
                0 ArisingFCreditAmt
        INTO    #TemResult
        FROM    fnc_GetBeginBalanceAccount_Object_OriginalCurrency(@FromDate,
                                                              @ToDate, 0,
                                                              @CurrencyID,
                                                              @ObjectID,
                                                              @ObjectType,
                                                              @StringAccount) a
        WHERE   (@ObjectID = 0 OR a.ObjectID = @ObjectID)
			AND (@ObjectType = '' OR a.ObjectType = @ObjectType)
			AND ((a.ObjectType = 'ARCustomers' AND EXISTS (SELECT 1 FROM #CusList WHERE #CusList.ARCustomerID = a.ObjectID))
				OR (a.ObjectType = 'HREmployees' AND EXISTS (SELECT 1 FROM #EmpList WHERE #EmpList.HREmployeeID = a.ObjectID)) )
        UNION ALL
        SELECT  Temp.AccountID ,
                Temp.ObjectID ,
                Temp.GLObjectNo ,
                Temp.GLObjectName ,
                Temp.OCode1 ,
                Temp.OCode2 ,
                Temp.Ocode3 ,
                Temp.Ocode4 ,
                Temp.Ocode5 ,
                Temp.Ocode6 ,
                Temp.Ocode7 ,
                Temp.Ocode8 ,
                Temp.Ocode9 ,
                Temp.Ocode10 ,
                -- Temp.Ocode11 ,
                -- Temp.Ocode12 ,
                -- Temp.Ocode13 ,
                -- Temp.Ocode14 ,
                -- Temp.Ocode15 ,
                Temp.ObjectType ,
                Temp.GECurrencyNo ,
                0 ,
                0 ,
                0 ,
                0 ,
                Temp.ArisingDebitAmt ,
                Temp.ArisingCreditAmt ,
                Temp.ArisingFDebitAmt ,
                Temp.ArisingFCreditAmt
        FROM    #TempArising Temp

-------- Tinh so du cuoi ki cua doi tuong = Du no DK - Du co DK + Du no Trong ki - Du co trong ki
        SELECT  AccountID ,
                Acc.GLAccountNo ,
                Acc.GLAccountName ,
                ObjectID ,
                GLObjectNo ,
                GLObjectName ,
                OCode1 ,
                OCode2 ,
                Ocode3 ,
                Ocode4 ,
                Ocode5 ,
                Ocode6 ,
                Ocode7 ,
                Ocode8 ,
                Ocode9 ,
                Ocode10 ,
                -- Ocode11 ,
                -- Ocode12 ,
                -- Ocode13 ,
                -- Ocode14 ,
                -- Ocode15 ,
                ObjectType ,
                GeCurrencyNo ,
                SUM(BeginDebit) BeginDebit ,
                SUM(BeginCredit) BeginCredit ,
                SUM(BeginDebit) - SUM(BeginCredit) BeginAmt ,
                SUM(BeginFDebit) BeginFDebit ,
                SUM(BeginFCredit) BeginFCredit ,
                SUM(BeginFDebit) - SUM(BeginFCredit) BeginFAmt ,
                SUM(ArisingDebitAmt) ArisingDebitAmt ,
                SUM(ArisingCreditAmt) ArisingCreditAmt ,
                SUM(BeginDebit) - SUM(BeginCredit) + SUM(ArisingDebitAmt)
                - SUM(ArisingCreditAmt) EndAmt ,
                SUM(ArisingFDebitAmt) ArisingFDebitAmt ,
                SUM(ArisingFCreditAmt) ArisingFCreditAmt ,
                SUM(BeginFDebit) - SUM(BeginFCredit) + SUM(ArisingFDebitAmt)
                - SUM(ArisingFCreditAmt) EndFAmt
        INTO    #Result
        FROM    #TemResult s
                JOIN dbo.GLAccounts Acc ON ( s.AccountID = Acc.GLAccountID
                                             AND Acc.AAStatus = 'Alive'
                                           )
        WHERE   Acc.GLAccountTypeCombo = 'AR'
                OR Acc.GLAccountTypeCombo = 'ARVC'
        GROUP BY AccountID ,
                Acc.GLAccountNo ,
                Acc.GLAccountName ,
                ObjectID ,
                GLObjectNo ,
                GLObjectName ,
                OCode1 ,
                OCode2 ,
                Ocode3 ,
                Ocode4 ,
                Ocode5 ,
                Ocode6 ,
                Ocode7 ,
                Ocode8 ,
                Ocode9 ,
                Ocode10 ,
                -- Ocode11 ,
                -- Ocode12 ,
                -- Ocode13 ,
                -- Ocode14 ,
                -- Ocode15 ,
                ObjectType ,
                GeCurrencyNo; 

	---- Tong hop Ket qua -- Phan loai dư no ben no - co

        SELECT  GLAccountNo ,
                GLAccountName ,
                GLObjectNo ,
                GLObjectName ,
                OCode1 ,
                OCode2 ,
                Ocode3 ,
                Ocode4 ,
                Ocode5 ,
                Ocode6 ,
                Ocode7 ,
                Ocode8 ,
                Ocode9 ,
                Ocode10 ,
                -- Ocode11 ,
                -- Ocode12 ,
                -- Ocode13 ,
                -- Ocode14 ,
                -- Ocode15 ,
                ObjectType ,
                GeCurrencyNo ,
                BeginDebit ,
                BeginCredit ,
                CASE WHEN BeginAmt > 0 THEN BeginAmt
                     ELSE 0
                END BeginDebitAmt ,
                CASE WHEN BeginAmt < 0 THEN ABS(BeginAmt)
                     ELSE 0
                END BeginCreditAmt ,
                BeginFDebit BeginFDebit ,
                BeginFCredit BeginFCredit ,
                CASE WHEN BeginFAmt > 0 THEN BeginFAmt
                     ELSE 0
                END BeginDebitFAmt ,
                CASE WHEN BeginFAmt < 0 THEN ABS(BeginFAmt)
                     ELSE 0
                END BeginCreditFAmt ,
                ArisingDebitAmt ArisingDebitAmt ,
                ArisingCreditAmt ArisingCreditAmt ,
                CASE WHEN EndAmt > 0 THEN EndAmt
                     ELSE 0
                END EndDebitAmt ,
                CASE WHEN EndAmt < 0 THEN ABS(EndAmt)
                     ELSE 0
                END EndCreditAmt ,
                ArisingFDebitAmt ArisingFDebitAmt ,
                ArisingFCreditAmt ArisingFCreditAmt ,
                CASE WHEN EndFAmt > 0 THEN EndFAmt
                     ELSE 0
                END EndDebitFAmt ,
                CASE WHEN EndFAmt < 0 THEN ABS(EndFAmt)
                     ELSE 0
                END EndCreditFAmt
        FROM    #Result;

    END;

/*

 EXEC Sp_AR_Liabilities 
      @FromDate = '2016/01/01' ,
      @ToDate = '2016/06/06'  ,
      @CurrencyID = 0 ,
      @StringAccount = '47' ,
      @ObjectType = '' ,
      @ObjectID = 0,
      @CustomerString = 'SELECT ARCustomerID FROM ARCustomers WHERE AAStatus = ''Alive''',
	  @EmployeeString = 'SELECT HREmployeeID FROM HREmployees WHERE AAStatus = ''Alive'''
*/

GO