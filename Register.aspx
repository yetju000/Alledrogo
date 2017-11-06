<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 72px;
            text-align: right;
        }
        .auto-style7 {
            width: 205px;
        }
        .auto-style8 {
            width: 157px;
        }
        .auto-style9 {
            width: 152px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>"></asp:SqlDataSource>
                </td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">Imie:</td>
                <td class="auto-style9">
                    <asp:TextBox ID="Name" runat="server" Width="145px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">Nazwisko:</td>
                <td class="auto-style9">
                    <asp:TextBox ID="Surname" runat="server" Width="145px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">E-mail:</td>
                <td class="auto-style9">
                    <asp:TextBox ID="Email" runat="server" Width="145px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">Hasło:</td>
                <td class="auto-style9">
                    <asp:TextBox ID="Password" runat="server" Width="145px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Button ID="RegisterButton" runat="server" Text="Rejestruj" OnClick="RegisterButton_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
