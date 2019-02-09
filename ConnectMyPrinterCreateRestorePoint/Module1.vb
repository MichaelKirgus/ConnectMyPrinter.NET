Imports System.Globalization
Imports ConnectMyPrinterLanguageHelper

Module Module1

    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Sub Main()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(AppContext), MCultureInf, "ProcessingStr", ""))
        Dim aa As New ConnectMyPrinterSystemRestorePointLib.CreateRestorePointClass
        aa.EnsureCreationPoint()
        aa.CreatePoint(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(AppContext), MCultureInf, "RestorePointNameStr", ""), True)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(AppContext), MCultureInf, "FinishedStr", ""))
    End Sub
End Module
