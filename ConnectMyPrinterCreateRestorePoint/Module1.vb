'Copyright (C) 2016-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports ConnectMyPrinterLanguageHelper

Module Module1

    Public MLangHelper As New LanguageApplyHelper
    Public MCultureInf As CultureInfo = CultureInfo.CurrentUICulture

    Sub Main()
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(Module1), MCultureInf, "ProcessingStr", ""))
        Dim aa As New ConnectMyPrinterSystemRestorePointLib.CreateRestorePointClass
        aa.EnsureCreationPoint()
        aa.CreatePoint(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(Module1), MCultureInf, "RestorePointNameStr", ""), True)
        Console.WriteLine(MLangHelper.GetCultureString("ConnectMyPrinterCreateRestorePoint.TranslatedStrings", GetType(Module1), MCultureInf, "FinishedStr", ""))
    End Sub
End Module
