;#DropDB SIAFEasyTest

#Application SIAF.Win
#Application SIAF.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef SIAF.Win

*Action About...
 
*Action OK

#EndIf

#IfDef SIAF.Web

*Action Choose Theme(Black Glass)

#EndIf
